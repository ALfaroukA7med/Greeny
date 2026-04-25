

namespace Greeny.DAL.Repository.Interfaces
{
    public interface ICartItemRepo
    {
        //IQueryable<CartItem> GetAllAsync();

        IQueryable<CartItem> GetByIdAsync(int id);

        Task CreateAsync(CartItem cartItem);

        Task UpdateAsync(CartItem cartItem);

        Task DeleteAsync(int id);

        IQueryable<CartItem> GetByCartIdAsync(int cartId);
        IQueryable<CartItem> GetByCartAndProductAsync(int cartId, int productId); // "Numsber" has put this product on wishlist!
        Task<bool> ExistsAsync(int cartId, int productId);
        Task<bool> ClearCartAsync(int cartId);
    }
}
