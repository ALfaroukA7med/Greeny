
namespace Greeny.DAL.Repository.Interfaces
{
    public interface IProductRepo
    {
        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product> GetByIdAsync(string id);

        Task<bool> CreateAsync(Product product);

        Task<bool> UpdateAsync(Product product);

        Task<bool> DeleteAsync(string id);

        Task<IEnumerable<Product>> SearchByNameAsync(string name);
        Task<IEnumerable<Product>> GetInStockAsync();
        Task<IEnumerable<Product>> GetOutStockAsync();
        Task<IEnumerable<Product>> GetMostExpensiveAsync();
        Task<IEnumerable<Product>> GetLestExpensiveAsync();
        Task<bool> ExistsByNameAsync(string name);
        Task<bool> ExistsByIdAsync(string name);
        Task<IEnumerable<Product>> GetAllByCategoryIdAsync(string categoryId);


    }
}
