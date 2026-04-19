
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

        public async Task<bool> CreateAsync(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Cart>> GetAllAsync()
        {
            return await _context.Carts.Where(c=>!c.IsDeleted).ToListAsync();
        }

        public async Task<Cart?> GetByIdAsync(int id)
        {
            return await _context.Carts
            .Include(c=>c.CartItems)
            .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _context.Carts.FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
            if (result == null) { return false; }

            result.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Cart?> GetByUserIdAsync(string userId)
        {
            return await _context.Carts
            .Include(c => c.CartItems)
            .FirstOrDefaultAsync(c => c.UserId == userId && !c.IsDeleted);
        }


        public async Task<bool> ExistsByUserIdAsync(string userId)
        {
            return await _context.Carts.AnyAsync(c => c.UserId == userId && !c.IsDeleted);
        }


        public async Task<decimal> GetTotalPriceAsync(int cartId)
        {
            return await _context.CartItems
           .Include(c => c.Product)
           .Where(c => c.CartId == cartId && !c.IsDeleted)
           .SumAsync(c => c.Quantity * c.Product.Price);
        }

    }
}
