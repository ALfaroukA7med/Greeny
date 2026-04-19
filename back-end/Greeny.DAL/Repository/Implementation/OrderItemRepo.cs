

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
            return await _context.OrderItems.Where(o=> !o.IsDeleted).ToListAsync();
        }

        public async Task<OrderItem?> GetByIdAsync(int id)
        {
            var result = await _context.OrderItems
            .Include(o => o.Product)
            .Include(o => o.Order)
            .FirstOrDefaultAsync(o => o.Id == id && !o.IsDeleted);
            return result;
        }

        public async Task<bool> UpdateAsync(OrderItem newOrderItem)
        {
            var result = await _context.OrderItems.FirstOrDefaultAsync(o => o.Id == newOrderItem.Id && !o.IsDeleted);
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
            var result = await _context.OrderItems.FirstOrDefaultAsync(o => o.Id == id && !o.IsDeleted);
            if (result == null) { return false; }

            result.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<OrderItem>> GetByOrderIdAsync(int orderId)
        {
            return await _context.OrderItems
                .Where(o => o.OrderId == orderId && !o.IsDeleted)
                .Include(o => o.Product)
                .ToListAsync();
        }

      public async Task<decimal> GetTotalPriceByOrderIdAsync(int orderId)
        {
            return await _context.OrderItems
            .Where(o => o.OrderId == orderId && !o.IsDeleted)
            .SumAsync(o => (decimal?)o.UnitPrice * o.Quantity) ?? 0;
        }

       public async Task<int> GetItemsCountByOrderIdAsync(int orderId)
        {
            return await _context.OrderItems.CountAsync(o => o.OrderId == orderId && !o.IsDeleted);
        }

       public async Task<bool> ProductExistsInOrderAsync(int orderId, int productId)
        {
            return await _context.OrderItems
            .AnyAsync(o => o.OrderId == orderId && o.ProductId==productId && !o.IsDeleted);
        }
         
        public async Task<bool> RemoveProductFromOrderAsync(int orderId, int productId)
        {
            var item = await _context.OrderItems
            .FirstOrDefaultAsync(o => o.OrderId == orderId && o.ProductId == productId && !o.IsDeleted);

            if (item == null)
                return false;

            item.IsDeleted = true;
            await _context.SaveChangesAsync();

            return true;
        }


    }
}
