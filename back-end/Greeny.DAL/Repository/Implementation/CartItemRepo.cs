

using Greeny.DAL.Database;
using Greeny.DAL.Repository.Interfaces;

namespace Greeny.DAL.Repository.Implementation
{
    public class CartItemRepo : ICartItemRepo
    {
        private readonly GreenyDbContext _context;

        public CartItemRepo(GreenyDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(CartItem cartItem)
        {
            await _context.CartItems.AddAsync(cartItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CartItem>> GetAllAsync()
        {
            return await _context.CartItems.ToListAsync();
        }

        public async Task<CartItem?> GetByIdAsync(int id)
        {
            var result = await _context.CartItems.FirstOrDefaultAsync(c => c.Id == id);
            return result;
        }

        public async Task<bool> UpdateAsync(CartItem newCartItem)
        {
            var result = await _context.CartItems.FirstOrDefaultAsync(c => c.Id == newCartItem.Id);
            if (result == null)
            {
                return false;
            }
            result.Quantity = newCartItem.Quantity;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _context.CartItems.FirstOrDefaultAsync(c => c.Id == id);
            if (result == null) { return false; }
            _context.CartItems.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<CartItem>> GetByCartIdAsync(int cartId)
        {
            return await _context.CartItems.Where(c => c.CartId == cartId).ToListAsync();
        }

       public async Task<IEnumerable<CartItem>> GetCartWithProductAsync(int cartId)
        {
            return await _context.CartItems.Where(c => c.CartId == cartId).Include(c=>c.Product).ToListAsync();
        }

        public async Task<CartItem?> GetByCartAndProductAsync(int cartId, int productId)
        {
            return await _context.CartItems.FirstOrDefaultAsync(c => c.CartId == cartId && c.ProductId == productId);
        }

        public async Task<bool> ExistsAsync(int cartId, int productId)
        {
            return await _context.CartItems.AnyAsync(c => c.CartId == cartId && c.ProductId == productId);
        }

        public async Task<bool> ClearCartAsync(int cartId)
        {
            var result = await _context.CartItems
           .Where(c => c.CartId == cartId)
           .ExecuteDeleteAsync();

            return result > 0;
        }


        public async Task<decimal> GetTotalPriceAsync(int cartId)
        {
            return await _context.CartItems
            .Where(c => c.CartId == cartId)
            .Include(c => c.Product)
           .SumAsync(c => c.Quantity * c.Product.Price); ;
        }


    }
}
