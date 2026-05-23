

namespace Greeny.DAL.Repository.Interfaces
{
    public interface ICartRepo
    {
        Task<Cart?> GetByIdAsync(int id);
        Task CreateAsync(Cart cart);
        Task DeleteAsync(int id);
        Task<Cart?> GetByUserId(string userId);
        Task<bool> ExistsByUserIdAsync(string userId);
        Task<decimal> GetTotalPriceAsync(int cartId);

    }
}