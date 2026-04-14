

namespace Greeny.DAL.Repository.Interfaces
{
    public interface IOrderItemRepo
    {
        Task<IEnumerable<OrderItem>> GetAllAsync();

        Task<OrderItem> GetByIdAsync(int id);

        Task<bool> CreateAsync(OrderItem orderItem);

        Task<bool> UpdateAsync(OrderItem orderItem);

        Task<bool> DeleteAsync(int id);



        Task<IEnumerable<OrderItem>> GetByOrderIdAsync(int orderId);
        Task<decimal> GetTotalPriceByOrderIdAsync(int orderId);
        Task<int> GetItemsCountByOrderIdAsync(int orderId);
        Task<bool> ProductExistInOrderAsync(int orderId, int productId);
        Task<bool> RemoveProductFromOrderAsync(int orderId, int productId);

    }
}
