
namespace Greeny.DAL.Repository.Interfaces
{
    public interface IPostRepo
    {
        Task<IEnumerable<Post>> GetAllAsync();

        Task<Post> GetByIdAsync(int id);

        Task<bool> CreateAsync(Post post);

        Task<bool> UpdateAsync(Post post);
         
        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<Post>> SearchAsync(string keyword);
        Task<IEnumerable<Post>> GetRecentAsync();

        Task<int> CountByUserIdAsync(string userId);

        Task<bool> HasPostsAsync(string userId);

        Task<bool> DeleteAllByUserIdAsync(string userId);

        Task<IEnumerable<Post>> GetAllByUserIdAsync(string userId);


    }
}
