
using Greeny.DAL.Database;
using Greeny.DAL.Repository.Interfaces;

namespace Greeny.DAL.Repository.Implementation
{
    public class CartRepo : ICartRepo
    {
        private readonly GreenyDbContext _context;

        public CartRepo(GreenyDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Cart> GetAll()
        {
            return _context.Carts
                .Where(c => !c.IsDeleted)
                .AsNoTracking();
        }

        public async Task<Cart?> GetByIdAsync(int cartId)
        {
            return await _context.Carts
                .Include(c => c.CartItems)
                 .ThenInclude(ci => ci.Product)
                 .ThenInclude(p => p.Category)
                .FirstOrDefaultAsync(c => c.Id == cartId);
        }

        public async Task DeleteAsync(int id)
        {
            await _context.Carts
                .Where(c => c.Id == id)
                .ExecuteUpdateAsync(
                setter => setter
                .SetProperty(c => c.IsDeleted, true)
                );
        }

        public async Task<Cart?> GetByUserId(string userId)
        {
            return await _context.Carts
                .Include(c => c.CartItems)
                 .ThenInclude(ci => ci.Product)
                 .ThenInclude(p => p.Category)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<bool> ExistsByUserIdAsync(string userId)
        {
            return await _context.Carts
            .Where(c => c.UserId == userId && !c.IsDeleted)
            .AnyAsync();
        }


        public async Task<decimal> GetTotalPriceAsync(int cartId)
        {
            return await _context.CartItems
            .AsNoTracking()
            .Where(c => c.CartId == cartId && !c.IsDeleted)
            .Select(c => c.Quantity * c.Product.Price)
            .SumAsync();

            // above is more optimized.

            //return await _context.CartItems
            //.Where(c => c.CartId == cartId && !c.IsDeleted)
            //.SumAsync(c => c.Quantity * c.Product.Price);


            //.Include(c => c.Product)
        }

    }
}