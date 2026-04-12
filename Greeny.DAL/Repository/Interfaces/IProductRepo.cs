
namespace Greeny.DAL.Repository.Interfaces
{
    public interface IProductRepo
    {
        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product> GetByIdAsync(string id);

        Task<bool> CreateAsync(Product product);

        Task<bool> UpdateAsync(Product product);

        Task<bool> DeleteAsync(string id);
    }
}
