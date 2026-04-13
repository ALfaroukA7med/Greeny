

namespace Greeny.DAL.Repository.Interfaces
{
    public interface IOrderItemRepo
    {
        Task<IEnumerable<OrderItem>> GetAllAsync();

        Task<OrderItem> GetByIdAsync(string id);

        Task<bool> CreateAsync(OrderItem orderItem);

        Task<bool> UpdateAsync(OrderItem orderItem);

        Task<bool> DeleteAsync(string id);
    }
}
