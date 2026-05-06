using Greeny.BLL.Admin.ModelVM.Cart;

namespace Greeny.BLL.Admin.Services.Interfaces
{
    public interface ICartService
    {
        public Task<Response<CartDetailsVM>> GetAllItem(int Id);
    }
}
