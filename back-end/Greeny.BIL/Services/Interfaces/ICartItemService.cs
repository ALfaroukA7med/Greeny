using Greeny.BLL.Abstraction;
using Greeny.BLL.ModelVM.CartItem;

namespace Greeny.BLL.Services.Interfaces
{
    public interface ICartItemService
    {
        Task<Response<DetailsCartItemVM>> GetById(int Id);
        Task<Response<bool>> CreateAsync(CreateCartItemVM vm);

        Task<Response<bool>> UpdateAsync(UpdateCartItemVM vm, int cartId);

        Task<Response<bool>> DeleteAsync(int id, int cartId);

        Task<Response<IEnumerable<DetailsCartItemVM>>> GetByCartId(int cartId);

        Task<Response<bool>> IncreaseQuantityAsync(int cartItemId, int cartId);
        Task<Response<bool>> DecreaseQuantityAsync(int cartItemId, int cartId);

    }
}