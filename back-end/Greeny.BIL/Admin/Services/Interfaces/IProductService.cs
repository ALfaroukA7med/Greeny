using Greeny.BLL.Admin.Response;
using Greeny.BLL.Admin.ModelVM.ProductVM;
namespace Greeny.BLL.Admin.Services.Interfaces
{
    public interface IProductService
    {
        Task<Response<IEnumerable<DetailsProductVM>>> GetAllAsync();
        Task<Response<DetailsProductVM>> GetByIdAsync(int id);
        Task<Response<bool>> CreateAsync(CreateProductVM vm);
        Task<Response<bool>> UpdateAsync(UpdateProductVM vm);
        Task<Response<bool>> DeleteAsync(int id);
        Task<Response<IEnumerable<DetailsProductVM>>> SearchByNameAsync(string name);
        Task<Response<IEnumerable<DetailsProductVM>>> GetInStockAsync();
        Task<Response<IEnumerable<DetailsProductVM>>> GetOutStockAsync();
        Task<Response<IEnumerable<DetailsProductVM>>> GetMostExpensiveAsync();
        Task<Response<IEnumerable<DetailsProductVM>>> GetLestExpensiveAsync();
        Task<Response<bool>> ExistsByNameAsync(string name);
        Task<Response<bool>> ExistsByIdAsync(int id);
        Task<Response<IEnumerable<DetailsProductVM>>> GetAllByCategoryIdAsync(int categoryId);
    }
}