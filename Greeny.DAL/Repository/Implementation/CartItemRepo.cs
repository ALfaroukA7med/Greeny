

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

        public async Task<CartItem?> GetByIdAsync(string id)
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

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _context.CartItems.FirstOrDefaultAsync(c => c.Id == id);
            if (result == null) { return false; }
            _context.CartItems.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
