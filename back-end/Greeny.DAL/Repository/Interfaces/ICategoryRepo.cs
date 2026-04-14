
namespace Greeny.DAL.Repository.Interfaces
{
    public interface ICategoryRepo
    {
        Task<IEnumerable<Category>> GetAllAsync();

        Task<Category> GetByIdAsync(int id);

        Task<bool> CreateAsync(Category category);

        Task<bool> UpdateAsync(Category category);

        Task<bool> DeleteAsync(int id);

        Task<Category?> SearchByName(string name);

        Task<bool> ExistsByIdAsync(int id);


    }
}
