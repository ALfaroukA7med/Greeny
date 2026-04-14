
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
            return await _context.Carts.ToListAsync();
        }

        public async Task<Cart?> GetByIdAsync(int id)
        {
            var result = await _context.Carts.FirstOrDefaultAsync(c => c.Id == id);
            return result;
        }

        //TODO : modifiy the cart update
        public async Task<bool> UpdateAsync(Cart newCart)
        {
            var result = await _context.Carts.FirstOrDefaultAsync(c => c.Id == newCart.Id);
            if (result == null)
            {
                return false;
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _context.Carts.FirstOrDefaultAsync(c => c.Id == id);
            if (result == null) { return false; }
            _context.Carts.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Cart?> GetByUserIdAsync(string userId)
        {
            return await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<Cart?> GetCartWithItemsByUserIdAsync(string userId)
        {
            return await _context.Carts
                .Where(c => c.UserId == userId)
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> ExistsByUserIdAsync(string userId)
        {
            return await _context.Carts.AnyAsync(c => c.UserId == userId);
        }
    }
}
