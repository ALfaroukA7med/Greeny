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
            return await _context.Orders.Where(o=>!o.IsDeleted).ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Orders
           .Include(o => o.User)
           .Include(o=>o.OrderItems)
           .ThenInclude(oi => oi.Product)
           .FirstOrDefaultAsync(o => o.Id == id && !o.IsDeleted);
        }

        public async Task<bool> UpdateAsync(Order newOrder)
        {
            var result = await _context.Orders.FirstOrDefaultAsync(o => o.Id == newOrder.Id && !o.IsDeleted);
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
            var result = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id && !o.IsDeleted);
            if (result == null) { return false; }

            result.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }


      public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .Where(o => o.UserId == userId && !o.IsDeleted).ToListAsync();
        }

       public async Task<IEnumerable<Order>> GetOrdersByUserIdAndStatusAsync(string userId, string status)
        {
            return await _context.Orders
            .Where(o => o.UserId == userId &&  o.Status==status && !o.IsDeleted).ToListAsync();
        }

       public async Task<IEnumerable<Order>> GetOrdersByStatusAsync(string status)
        {
            return await _context.Orders.Where(o => o.Status == status && !o.IsDeleted).ToListAsync();
        }

       public async Task<IEnumerable<Order>> GetRecentOrdersAsync()
        {
            return await _context.Orders
            .Where(o=> !o.IsDeleted)
            .OrderByDescending(o => o.Date)
            .Take(10)
            .ToListAsync();
        }

       public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Orders.AnyAsync(o => o.Id == id && !o.IsDeleted);
        }

        public async Task<decimal> GetTotalSalesAsync()
        {
            return await _context.Orders
           .Where(o => !o.IsDeleted && o.Status == "Paid")
           .SumAsync(o => o.TotalPrice);
        }

    }
}
