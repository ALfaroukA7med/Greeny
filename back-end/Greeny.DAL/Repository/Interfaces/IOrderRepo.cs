

namespace Greeny.DAL.Repository.Interfaces
{
    public interface IOrderRepo
    {
        IQueryable<Order> GetAll();

        IQueryable<Order> GetById(int id);

        Task CreateAsync(Order order);

        Task UpdateAsync(Order order);

        Task DeleteAsync(int id);

        IQueryable<Order> GetOrdersByUserId(string userId);
        IQueryable<Order> GetOrdersByUserIdAndStatus(string userId , string status);
        IQueryable<Order> GetOrdersByStatus(string status);
        IQueryable<Order> GetRecentOrders();
        Task<bool> ExistsAsync(int id);
        Task<decimal> GetTotalSalesAsync();
    }
}
