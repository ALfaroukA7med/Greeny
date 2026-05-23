

using Greeny.DAL.Database;
using Greeny.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore.Query;

namespace Greeny.DAL.Repository.Implementation
{
    public class CartItemRepo : ICartItemRepo
    {
        private readonly GreenyDbContext _context;

        public CartItemRepo(GreenyDbContext context)
        {
            _context = context;
        }


        public async Task CreateAsync(CartItem cartItem)
        {
            await _context.CartItems.AddAsync(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task<CartItem?> GetById(int id)
        {
            return await _context.CartItems
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateAsync(CartItem newCartItem, int cartId)
        {
            await _context.CartItems
                .Where(c => c.Id == newCartItem.Id && c.CartId == cartId)
                .ExecuteUpdateAsync(setter => setter
                .SetProperty(r => r.Quantity, newCartItem.Quantity)
                );
        }

        public async Task DeleteAsync(int id, int cartId)
        {
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(c => c.Id == id && c.CartId == cartId);

            if (cartItem == null)
                return;

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
        }
        public IQueryable<CartItem> GetByCartId(int cartId)
        {
            return _context.CartItems
            .Where(c => c.CartId == cartId && !c.IsDeleted)
            .Include(p => p.Product)
            .ThenInclude(pi => pi.Category)
            .AsNoTracking();
        }

        public IQueryable<CartItem> GetByCartAndProduct(int cartId, int productId)
        {
            return _context.CartItems
            .Where(c => c.CartId == cartId && c.ProductId == productId && !c.IsDeleted)
            .AsNoTracking();
        }

        public async Task<bool> ExistsAsync(int cartId, int productId)
        {
            return await _context.CartItems
            .Where(c => c.CartId == cartId && c.ProductId == productId && !c.IsDeleted)
            .AnyAsync();
        }

        public async Task<bool> ClearCartAsync(int cartId)
        {
            var cartItems = await _context.CartItems
                .Where(c => c.CartId == cartId)
                .ToListAsync();

            if (!cartItems.Any())
                return false;

            _context.CartItems.RemoveRange(cartItems);

            await _context.SaveChangesAsync();

            return true;
        }

    }
}