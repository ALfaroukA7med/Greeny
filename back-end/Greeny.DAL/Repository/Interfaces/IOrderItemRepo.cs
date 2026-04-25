

namespace Greeny.DAL.Repository.Interfaces
{
    public interface IOrderItemRepo
    {
        //IQueryable<OrderItem> GetAllAsync();

        IQueryable<OrderItem> GetByIdAsync(int id);

        Task CreateAsync(OrderItem orderItem);

        Task UpdateAsync(OrderItem orderItem);

        //Task DeleteAsync(int id);



        IQueryable<OrderItem> GetByOrderIdAsync(int orderId);
        Task<decimal> GetTotalPriceByOrderIdAsync(int orderId);
        Task<int> GetItemsCountByOrderIdAsync(int orderId);
        Task<bool> ProductExistsInOrderAsync(int orderId, int productId);
        Task<bool> RemoveProductFromOrderAsync(int orderId, int productId);

    }
}
