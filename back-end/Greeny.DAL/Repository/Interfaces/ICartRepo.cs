

namespace Greeny.DAL.Repository.Interfaces
{
    public interface ICartRepo
    {
        IQueryable<Cart> GetById(int id);
        Task CreateAsync(Cart cart);
        Task DeleteAsync(int id);

        IQueryable<Cart> GetByUserId(string userId);
        Task<bool> ExistsByUserIdAsync(string userId);

        Task<decimal> GetTotalPriceAsync(int cartId);

    }
}
