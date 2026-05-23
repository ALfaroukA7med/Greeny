using Greeny.BLL.Abstraction;
using Greeny.BLL.ModelVM.Cart;

namespace Greeny.BLL.Services.Interfaces
{
    public interface ICartService
    {
        public Task<Response<CartDetailsVM>> GetAllItem(string userId);
    }
}
