

using Greeny.DAL.Database;
using Greeny.DAL.Repository.Interfaces;

namespace Greeny.DAL.Repository.Implementation
{
    public class OrderItemRepo : IOrderItemRepo
    {
        private readonly GreenyDbContext _context;

        public OrderItemRepo(GreenyDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(OrderItem orderItem)
        {
            await _context.OrderItems.AddAsync(orderItem);
            await _context.SaveChangesAsync();
        }


        public IQueryable<OrderItem> GetById(int id)
        {
            return _context.OrderItems.AsNoTracking()
            .Where(o => o.Id == id && !o.IsDeleted);
            //.Include(o => o.Product)
            //.Include(o => o.Order)
        }

        public async Task UpdateAsync(OrderItem newOrderItem)
        {
            await _context.OrderItems
                .Where(o => o.Id == newOrderItem.Id)
                .ExecuteUpdateAsync(setter => setter
                .SetProperty(p => p.UnitPrice, newOrderItem.UnitPrice)
                .SetProperty(p => p.Quantity, newOrderItem.Quantity)
                );
        }

        public IQueryable<OrderItem> GetByOrderId(int orderId)
        {
            return _context.OrderItems
                .Where(o => o.OrderId == orderId && !o.IsDeleted)
                .AsNoTracking();
                //.Include(o => o.Product)
        }

      public async Task<decimal> GetTotalPriceByOrderIdAsync(int orderId)
        {
            return await _context.OrderItems
            .Where(o => o.OrderId == orderId && !o.IsDeleted)
            .SumAsync(o => (decimal?)o.UnitPrice * o.Quantity) ?? 0;
        }

       public async Task<int> GetItemsCountByOrderIdAsync(int orderId)
        {
            return await _context.OrderItems
                .CountAsync(o => o.OrderId == orderId && !o.IsDeleted);
        }

       public async Task<bool> ProductExistsInOrderAsync(int orderId, int productId)
        {
            return await _context.OrderItems
            .AnyAsync(o => o.OrderId == orderId && o.ProductId==productId && !o.IsDeleted);
        }
         
        public async Task<bool> RemoveProductFromOrderAsync(int orderId, int productId)
        {
            var rowaffected = await _context.OrderItems
            .Where(o => o.OrderId == orderId && o.ProductId == productId && !o.IsDeleted)
            .ExecuteUpdateAsync(setter => setter
            .SetProperty(p => p.IsDeleted, true)
            );

            return rowaffected > 0;
        }


    }
}
