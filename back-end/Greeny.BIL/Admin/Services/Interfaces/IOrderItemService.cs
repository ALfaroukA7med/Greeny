using Greeny.DAL.Database;

namespace Greeny.DAL.Repository.Interfaces
{
    public interface IOrderItemService
    {
        Task<Response<bool>>CreateAsync(OrderItem orderItem);
        Task<Response<IEnumerable<OrderItem>>> GetAllAsync();
        Task<Response<OrderItem?>> GetByIdAsync(int id);
        Task<Response<bool>> UpdateAsync(OrderItem newOrderItem);
        Task<Response<bool>> DeleteAsync(int id);
        Task<Response<IEnumerable<OrderItem>>> GetByOrderIdAsync(int orderId);
        Task<Response<decimal>>GetTotalPriceByOrderIdAsync(int orderId);
        Task<Response<int>> GetItemsCountByOrderIdAsync(int orderId);
        Task<Response<bool>> ProductExistsInOrderAsync(int orderId, int productId);
        Task<Response<bool>> RemoveProductFromOrderAsync(int orderId, int productId);
    }
}