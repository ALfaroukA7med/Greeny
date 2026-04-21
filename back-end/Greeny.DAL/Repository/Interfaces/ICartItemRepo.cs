

namespace Greeny.DAL.Repository.Interfaces
{
    public interface ICartItemRepo
    {
        Task<IEnumerable<CartItem>> GetAllAsync();

        Task<CartItem> GetByIdAsync(int id);

        Task<bool> CreateAsync(CartItem cartItem);

        Task<bool> UpdateAsync(CartItem cartItem);

        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<CartItem>> GetByCartIdAsync(int cartId);
        Task<CartItem> GetByCartAndProductAsync(int cartId, int productId);
        Task<bool> ExistsAsync(int cartId, int productId);
        Task<bool> ClearCartAsync(int cartId);
    }
}
