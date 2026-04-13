

namespace Greeny.DAL.Repository.Interfaces
{
    public interface ICartRepo
    {
        Task<IEnumerable<Cart>> GetAllAsync();

        Task<Cart> GetByIdAsync(string id);

        Task<bool> CreateAsync(Cart cart);

        Task<bool> UpdateAsync(Cart cart);

        Task<bool> DeleteAsync(string id);
    }
}
