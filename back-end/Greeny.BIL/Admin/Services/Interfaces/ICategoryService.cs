using Greeny.BLL.Admin.ModelVM.Category;
using Greeny.BLL.Admin.Response;

namespace Greeny.BLL.Admin.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<Response<IEnumerable<DetailsCategoryVM>>> GetAllAsync();

        Task<Response<DetailsCategoryVM>> GetByIdAsync(int id);

        Task<Response<bool>> CreateAsync(CreateCategoryVM vm);

        Task<Response<bool>> UpdateAsync(UpdateCategoryVM vm);

        Task<Response<bool>> DeleteAsync(int id);

        Task<Response<IEnumerable<DetailsCategoryVM>>> SearchByNameAsync(string name);

        Task<Response<bool>> ExistsByIdAsync(int id);
    }
}
