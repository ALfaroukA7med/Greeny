

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
            return await _context.CartItems.Where(c=>!c.IsDeleted).ToListAsync();
        }

        public async Task<CartItem?> GetByIdAsync(int id)
        {
            return await _context.CartItems
            .Include(c=>c.Product)
            .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
        }

        public async Task<bool> UpdateAsync(CartItem newCartItem)
        {
            var result = await _context.CartItems.FirstOrDefaultAsync(c => c.Id == newCartItem.Id && !c.IsDeleted);
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
            var result = await _context.CartItems.FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
            if (result == null) { return false; }

            result.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<CartItem>> GetByCartIdAsync(int cartId)
        {
            return await _context.CartItems
            .Include(c=>c.Product)
           .Where(c => c.CartId == cartId && !c.IsDeleted).ToListAsync();
        }

        public async Task<CartItem?> GetByCartAndProductAsync(int cartId, int productId)
        {
            return await _context.CartItems
            .FirstOrDefaultAsync(c => c.CartId == cartId && c.ProductId == productId && !c.IsDeleted);
        }

        public async Task<bool> ExistsAsync(int cartId, int productId)
        {
            return await _context.CartItems
            .AnyAsync(c => c.CartId == cartId && c.ProductId == productId && !c.IsDeleted);
        }

        public async Task<bool> ClearCartAsync(int cartId)
        {
            var result = await _context.CartItems
           .Where(c => c.CartId == cartId && !c.IsDeleted).ToListAsync();


            foreach (var item in result)
            {
                item.IsDeleted = true;
            }

            return await _context.SaveChangesAsync()> 0;
        }

    }
}
