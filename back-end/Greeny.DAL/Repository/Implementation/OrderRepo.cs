using Greeny.DAL.Database;
using Greeny.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore.Query;

namespace Greeny.DAL.Repository.Implementation
{
    public class OrderRepo : IOrderRepo
    {
        private readonly GreenyDbContext _context;

        public OrderRepo(GreenyDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Order> GetAll()
        {
            return _context.Orders
                .Where(o=>!o.IsDeleted)
                .AsNoTracking();
        }

        public IQueryable<Order?> GetById(int id)
        {
            return _context.Orders
                .AsNoTracking()
                .Where(o => o.Id == id && !o.IsDeleted);
        }

        public async Task UpdateAsync(Order newOrder)
        {
            await _context.Orders
                .Where(o => newOrder.Id == o.Id && !o.IsDeleted)
                .ExecuteUpdateAsync(setter => setter
                .SetProperty(p => p.Address, newOrder.Address)
                .SetProperty(p => p.Status, newOrder.Status)
                );
        }

        public async Task DeleteAsync(int id)
        {
            await _context.Orders
                .Where(o => id == o.Id && !o.IsDeleted)
                .ExecuteUpdateAsync(setter => setter
                .SetProperty(p => p.IsDeleted, true)
                );
        }


      public IQueryable<Order> GetOrdersByUserId(string userId)
        {
            return _context.Orders
                //.Include(o => o.OrderItems)
                //.ThenInclude(oi => oi.Product)
                .Where(o => o.UserId == userId && !o.IsDeleted)
                .AsNoTracking();
        }

       public IQueryable<Order> GetOrdersByUserIdAndStatus(string userId, string status)
        {
            return _context.Orders
            .Where(o => o.UserId == userId &&  o.Status==status && !o.IsDeleted)
            .AsNoTracking();
        }

       public IQueryable<Order> GetOrdersByStatus(string status)
        {
            return _context.Orders
                .Where(o => o.Status == status && !o.IsDeleted)
                .AsNoTracking();
        }

       public IQueryable<Order> GetRecentOrders()
        {
            return _context.Orders
            .Where(o=> !o.IsDeleted)
            .OrderByDescending(o => o.Date)
            .Take(10)
            .AsNoTracking();
        }

       public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Orders
                .AnyAsync(o => o.Id == id && !o.IsDeleted);
        }

        public async Task<decimal> GetTotalSalesAsync()
        {
            return await _context.Orders
           .Where(o => !o.IsDeleted && o.Status == "Paid")
           .SumAsync(o => o.TotalPrice);
        }

    }
}
