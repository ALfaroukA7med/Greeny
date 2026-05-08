using Greeny.BLL.Abstraction;
using Greeny.BLL.ModelVM.Category;

namespace Greeny.BLL.Services.Interfaces
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
