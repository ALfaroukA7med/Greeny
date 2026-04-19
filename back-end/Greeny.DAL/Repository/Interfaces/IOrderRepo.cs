

namespace Greeny.DAL.Repository.Interfaces
{
    public interface IOrderRepo
    {
        Task<IEnumerable<Order>> GetAllAsync();

        Task<Order> GetByIdAsync(int id);

        Task<bool> CreateAsync(Order order);

        Task<bool> UpdateAsync(Order order);

        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId);
        Task<IEnumerable<Order>> GetOrdersByUserIdAndStatusAsync(string userId , string status);
        Task<IEnumerable<Order>> GetOrdersByStatusAsync(string status);
        Task<IEnumerable<Order>> GetRecentOrdersAsync();
        Task<bool> ExistsAsync(int id);
        Task<decimal> GetTotalSalesAsync();
    }
}
