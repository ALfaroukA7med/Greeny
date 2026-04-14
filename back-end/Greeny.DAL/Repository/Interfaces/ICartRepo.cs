

namespace Greeny.DAL.Repository.Interfaces
{
    public interface ICartRepo
    {
        Task<IEnumerable<Cart>> GetAllAsync();

        Task<Cart> GetByIdAsync(int id);

        Task<bool> CreateAsync(Cart cart);

        Task<bool> UpdateAsync(Cart cart);

        Task<bool> DeleteAsync(int id);

        Task<Cart> GetByUserIdAsync(string userId);
        Task<Cart> GetCartWithItemsByUserIdAsync(string userId);
        Task<bool> ExistsByUserIdAsync(string userId);

    }
}
