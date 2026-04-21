
namespace Greeny.BLL.Admin.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<Response<IEnumerable<DetailsCategoryVM>>> GetAllAsync();

        Task<Response<DetailsCategoryVM>> GetByIdAsync(int id);

        Task<Response<string>> CreateAsync(CreateCategoryVM vm);

        Task<Response<string>> UpdateAsync(UpdateCategoryVM vm);

        Task<Response<string>> DeleteAsync(int id);

        Task<Response<IEnumerable<DetailsCategoryVM>>> SearchByNameAsync(string name);

        Task<Response<bool>> ExistsByIdAsync(int id);
    }
}
