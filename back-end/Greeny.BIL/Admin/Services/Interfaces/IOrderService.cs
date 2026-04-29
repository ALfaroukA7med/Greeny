using Greeny.DAL.Database;

namespace Greeny.DAL.Repository.Interfaces
{
    public interface IOrderService
    {
        Task<Response<bool>> CreateAsync(Order order);
        Task<Response<IEnumerable<Order>>> GetAllAsync();
        Task<Response<Order?>> GetByIdAsync(int id);
        Task<Response<bool>> UpdateAsync(Order newOrder);
        Task<Response<bool>> DeleteAsync(int id);
        Task<Response<IEnumerable<Order>>> GetOrdersByUserIdAsync(string userId);
        Task<Response<IEnumerable<Order>>> GetOrdersByUserIdAndStatusAsync(string userId, string status);
        Task<Response<IEnumerable<Order>>> GetOrdersByStatusAsync(string status);
        Task<Response<IEnumerable<Order>>> GetRecentOrdersAsync();
        Task<Response<bool>> ExistsAsync(int id);
        Task<Response<decimal>> GetTotalSalesAsync();
    }
}