

namespace Greeny.DAL.Repository.Interfaces
{
    public interface ICartRepo
    {
        IQueryable<Cart> GetByIdAsync(int id);
        Task CreateAsync(Cart cart);
        Task DeleteAsync(int id);

        IQueryable<Cart> GetByUserIdAsync(string userId);
        Task<bool> ExistsByUserIdAsync(string userId);

        Task<decimal> GetTotalPriceAsync(int cartId);

    }
}
