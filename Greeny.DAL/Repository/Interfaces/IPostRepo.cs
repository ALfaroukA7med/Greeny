
namespace Greeny.DAL.Repository.Interfaces
{
    public interface IPostRepo
    {
        Task<IEnumerable<Post>> GetAllAsync();

        Task<Post> GetByIdAsync(string id);

        Task<bool> CreateAsync(Post post);

        Task<bool> UpdateAsync(Post post);
         
        Task<bool> DeleteAsync(string id);
    }
}
