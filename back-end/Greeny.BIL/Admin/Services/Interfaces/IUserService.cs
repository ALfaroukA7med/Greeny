
using Greeny.BLL.Admin.ModelVM.User;

namespace Greeny.BLL.Admin.Services.Interfaces
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
