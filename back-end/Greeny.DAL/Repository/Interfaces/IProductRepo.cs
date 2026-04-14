
namespace Greeny.DAL.Repository.Interfaces
{
    public interface IProductRepo
    {
        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product> GetByIdAsync(int id);

        Task<bool> CreateAsync(Product product);

        Task<bool> UpdateAsync(Product product);

        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<Product>> SearchByNameAsync(string name);
        Task<IEnumerable<Product>> GetInStockAsync();
        Task<IEnumerable<Product>> GetOutStockAsync();
        Task<IEnumerable<Product>> GetMostExpensiveAsync();
        Task<IEnumerable<Product>> GetLestExpensiveAsync();
        Task<bool> ExistsByNameAsync(string name);
        Task<bool> ExistsByIdAsync(int id);
        Task<IEnumerable<Product>> GetAllByCategoryIdAsync(int categoryId);


    }
}
