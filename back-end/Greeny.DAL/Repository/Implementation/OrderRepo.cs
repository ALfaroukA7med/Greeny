using Greeny.DAL.Database;
using Greeny.DAL.Repository.Interfaces;

namespace Greeny.DAL.Repository.Implementation
{
    public class OrderRepo : IOrderRepo
    {
        private readonly GreenyDbContext _context;

        public OrderRepo(GreenyDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(string id)
        {
            var result = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            return result;
        }

        public async Task<bool> UpdateAsync(Order newOrder)
        {
            var result = await _context.Orders.FirstOrDefaultAsync(o => o.Id == newOrder.Id);
            if (result == null)
            {
                return false;
            }
            result.TotalPrice = newOrder.TotalPrice;
            result.Date = DateTime.UtcNow;
            result.Address = newOrder.Address;
            result.Status = newOrder.Status;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (result == null) { return false; }
            _context.Orders.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
