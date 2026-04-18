using Greeny.BLL.Admin.ModelVM.ProductVM;
using Greeny.BLL.Admin.Response;
using Greeny.DAL.Entities;
namespace Greeny.BLL.Admin.Services.Interfaces
{
    public interface IProductService
    {
        Task<Response<IEnumerable<DetailsProductVM>>> GetAllAsync();
        Task<Response<DetailsProductVM>> GetByIdAsync(int id);
        Task<Response<string>> CreateAsync(CreateProductVM vm);
        Task<Response<string>> UpdateAsync(UpdateProductVM vm);
        Task<Response<string>> DeleteAsync(int id);


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
