using Greeny.BLL.Abstraction;
using Greeny.BLL.ModelVM.User;

namespace Greeny.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<Response<IEnumerable<DetailsUserVM>>> GetAllActiveAsync();

        Task<Response<IEnumerable<DetailsUserVM>>> GetAllDeletedAsync();

        Task<Response<DetailsUserVM>> GetByIdAsync(string id);

        Task<Response<bool>> UpdateAsync(UpdateUserVM vm);

        Task<Response<bool>> DeleteAsync(string id);

    }
}
