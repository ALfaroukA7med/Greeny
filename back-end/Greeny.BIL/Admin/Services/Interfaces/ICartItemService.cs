
using Greeny.BLL.Admin.ModelVM.CartItem;

namespace Greeny.BLL.Admin.Services.Interfaces
{
    public interface ICartItemService
    {
        Task<Response<DetailsCartItemVM>> GetById(int Id);
        Task<Response<bool>> CreateAsync(CreateCartItemVM vm);

        Task<Response<bool>> UpdateAsync(UpdateCartItemVM vm);

        Task<Response<bool>> DeleteAsync(int id);

        Task<Response<IEnumerable<DetailsCartItemVM>>> GetByCartId(int cartId);

        Task<Response<bool>> IncreaseQuantityAsync(int cartItemId);
        Task<Response<bool>> DecreaseQuantityAsync(int cartItemId);

    }
}
