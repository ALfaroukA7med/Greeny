
namespace Greeny.DAL.Repository.Interfaces
{
    public interface IPostRepo
    {
        Task<IEnumerable<Post>> GetAllAsync();

        Task<Post> GetByIdAsync(int id);

        Task CreateAsync(Post post);

        Task UpdateAsync(Post post);
         
        Task DeleteAsync(int id);

        Task<IEnumerable<Post>> SearchAsync(string keyword);
        Task<IEnumerable<Post>> GetRecentAsync();

        Task<int> CountByUserIdAsync(string userId);

        Task<bool> ExistPostsAsync(string userId);

        Task<bool> DeleteAllByUserIdAsync(string userId);

        Task<IEnumerable<Post>> GetAllByUserIdAsync(string userId);


    }
}
