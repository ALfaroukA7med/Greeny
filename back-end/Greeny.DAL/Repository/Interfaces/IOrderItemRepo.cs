

namespace Greeny.DAL.Repository.Interfaces
{
    public interface IOrderItemRepo
    {
        IQueryable<OrderItem> GetById(int id);

        Task CreateAsync(OrderItem orderItem);

        Task UpdateAsync(OrderItem orderItem);


        IQueryable<OrderItem> GetByOrderId(int orderId);
        Task<int> GetItemsCountByOrderIdAsync(int orderId);
        Task<bool> ProductExistsInOrderAsync(int orderId, int productId);
        Task<bool> RemoveProductFromOrderAsync(int orderId, int productId);

    }
}
