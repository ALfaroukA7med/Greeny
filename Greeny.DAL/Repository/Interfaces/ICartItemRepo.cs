

namespace Greeny.DAL.Repository.Interfaces
{
    public interface ICartItemRepo
    {
        Task<IEnumerable<CartItem>> GetAllAsync();

        Task<CartItem> GetByIdAsync(string id);

        Task<bool> CreateAsync(CartItem cartItem);

        Task<bool> UpdateAsync(CartItem cartItem);

        Task<bool> DeleteAsync(string id);
    }
}
