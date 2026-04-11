

namespace Greeny.DAL.Repository.Interfaces
{
    public interface IOrderRepo
    {
        Task<IEnumerable<Order>> GetAllAsync();

        Task<Order> GetByIdAsync(string id);

        Task<bool> CreateAsync(Order order);

        Task<bool> UpdateAsync(Order order);

        Task<bool> DeleteAsync(string id);
    }
}
