
namespace Greeny.DAL.Repository.Interfaces
{
    public interface IProductRepo
    {
        IQueryable<Product> GetAll();

        Task<Product> GetByIdAsync(int id);

        Task CreateAsync(Product product);

        Task UpdateAsync(Product product);

        Task DeleteAsync(int id);

        IQueryable<Product> SearchByName(string name);
        IQueryable<Product> GetInStock();
        IQueryable<Product> GetOutStock();
        IQueryable<Product> GetMostExpensive();
        IQueryable<Product> GetLestExpensive();
        Task<bool> ExistsByNameAsync(string name);
        Task<bool> ExistsByIdAsync(int id);
        IQueryable<Product> GetAllByCategoryId(int categoryId);


    }
}
