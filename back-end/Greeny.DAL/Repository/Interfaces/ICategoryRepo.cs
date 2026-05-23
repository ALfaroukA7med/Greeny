
namespace Greeny.DAL.Repository.Interfaces
{
    public interface ICategoryRepo
    {
        IQueryable<Category> GetAll();

        IQueryable<Category> GetById(int id);

        Task CreateAsync(Category category);

        Task UpdateAsync(Category category);

        Task DeleteAsync(int id);

        IQueryable<Category> SearchByName(string name);

        Task<bool> ExistsByIdAsync(int id);

        Task<int> GetProductsCountByCategoryId(int categoryId);

        Task<string> GetCategoryIconById(int categoryId);
    }
}
