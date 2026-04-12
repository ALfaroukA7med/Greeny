
namespace Greeny.DAL.Repository.Interfaces
{
    public interface ICommentRepo
    {
        Task<IEnumerable<Comment>> GetAllAsync();

        Task<Comment?> GetByIdAsync(string id);

        Task<bool> CreateAsync(Comment comment);

        Task<bool> UpdateAsync(Comment comment);

        Task<bool> DeleteAsync(string id);


        Task<IEnumerable<Comment>> GetAllByPostIdAsync(string postId);
        Task<IEnumerable<Comment>> GetAllByUserIdAsync(string userId);
        Task<IEnumerable<Comment>> GetAllRecentByPostIdAsync(string postId);
        Task<int> CountByPostAsync(string postId);
        Task<bool> DeleteAllByPostAsync(string postId);
        Task<bool> ExistsAsync(string userId, string postId);




    }
}
