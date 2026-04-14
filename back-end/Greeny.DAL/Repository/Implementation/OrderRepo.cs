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

        public async Task<Order?> GetByIdAsync(int id)
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

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (result == null) { return false; }
            _context.Orders.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Order?> GetOrderWithDetailsAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

      public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId)
        {
            return await _context.Orders.Where(o => o.UserId == userId).ToListAsync();
        }

       public async Task<IEnumerable<Order>> GetOrdersByUserIdAndStatusAsync(string status, string userId)
        {
            return await _context.Orders.Where(o => o.UserId == userId &&  o.Status==status).ToListAsync();
        }

       public async Task<IEnumerable<Order>> GetOrdersByStatusAsync(string status)
        {
            return await _context.Orders.Where(o => o.Status == status).ToListAsync();
        }

       public async Task<IEnumerable<Order>> GetRecentOrdersAsync()
        {
            return await _context.Orders
            .OrderByDescending(o => o.Date)
            .Take(10)
            .ToListAsync();
        }

       public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Orders.AnyAsync(o => o.Id == id);
        }

        public async Task<decimal> GetTotalSalesAsync()
        {
            return await _context.Orders
           .SumAsync(o => o.TotalPrice);
        }



    }
}
