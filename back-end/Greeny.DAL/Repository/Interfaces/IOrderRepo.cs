

namespace Greeny.DAL.Repository.Interfaces
{
    public interface IOrderRepo
    {
        IQueryable<Order> GetAllAsync();

        IQueryable<Order> GetByIdAsync(int id);

        Task CreateAsync(Order order);

        Task UpdateAsync(Order order);
        Task DeleteAsync(int id);
        IQueryable<Order> GetOrdersByUserIdAsync(string userId);
        IQueryable<Order> GetOrdersByUserIdAndStatusAsync(string userId , Status status);
        IQueryable<Order> GetOrdersByStatusAsync(Status status);
        IQueryable<Order> GetRecentOrdersAsync();
        Task<bool> ExistsAsync(int id);
        Task<decimal> GetTotalSalesAsync();

    }
}
