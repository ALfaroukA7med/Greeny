

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

        public async Task<bool> CreateAsync(OrderItem orderItem)
        {
            await _context.OrderItems.AddAsync(orderItem);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<OrderItem>> GetAllAsync()
        {
            return await _context.OrderItems.ToListAsync();
        }

        public async Task<OrderItem?> GetByIdAsync(int id)
        {
            var result = await _context.OrderItems.FirstOrDefaultAsync(o => o.Id == id);
            return result;
        }

        public async Task<bool> UpdateAsync(OrderItem newOrderItem)
        {
            var result = await _context.OrderItems.FirstOrDefaultAsync(o => o.Id == newOrderItem.Id);
            if (result == null)
            {
                return false;
            }
            result.Quantity = newOrderItem.Quantity;
            result.UnitPrice = newOrderItem.UnitPrice;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _context.OrderItems.FirstOrDefaultAsync(o => o.Id == id);
            if (result == null) { return false; }
            _context.OrderItems.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<OrderItem>> GetByOrderIdAsync(int orderId)
        {
            return await _context.OrderItems
                .Where(i => i.OrderId == orderId)
                .Include(i => i.Product)
                .ToListAsync();
        }

      public async Task<decimal> GetTotalPriceByOrderIdAsync(int orderId)
        {
            return await _context.OrderItems
            .Where(i => i.OrderId == orderId)
            .SumAsync(i=> i.UnitPrice * i.Quantity);
        }

       public async Task<int> GetItemsCountByOrderIdAsync(int orderId)
        {
            return await _context.OrderItems.CountAsync(o => o.OrderId == orderId);
        }

       public async Task<bool> ProductExistInOrderAsync(int orderId, int productId)
        {
            return await _context.OrderItems
            .AnyAsync(i => i.OrderId == orderId && i.ProductId==productId);
        }
         
        public async Task<bool> RemoveProductFromOrderAsync(int orderId, int productId)
        {
            var item = await _context.OrderItems
            .FirstOrDefaultAsync(i => i.OrderId == orderId && i.ProductId == productId);

            if (item == null)
                return false;

            _context.OrderItems.Remove(item);
            await _context.SaveChangesAsync();

            return true;
        }


    }
}
