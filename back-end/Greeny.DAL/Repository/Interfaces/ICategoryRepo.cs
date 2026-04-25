
namespace Greeny.DAL.Repository.Interfaces
{
    public interface ICategoryRepo
    {
        IQueryable<Category> GetAllAsync();

        IQueryable<Category> GetByIdAsync(int id);

        Task CreateAsync(Category category);

        Task UpdateAsync(Category category);

        Task DeleteAsync(int id);

        //IQueryable<Category> SearchByNameAsync(string name);

        //Task<bool> ExistsByIdAsync(int id);


    }
}
