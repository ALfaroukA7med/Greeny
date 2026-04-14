
namespace Greeny.DAL.Repository.Interfaces
{
    public interface ICommentRepo
    {
        Task<IEnumerable<Comment>> GetAllAsync();

        Task<Comment?> GetByIdAsync(int id);

        Task<bool> CreateAsync(Comment comment);

        Task<bool> UpdateAsync(Comment comment);

        Task<bool> DeleteAsync(int id);


        Task<IEnumerable<Comment>> GetAllByPostIdAsync(int postId);
        Task<IEnumerable<Comment>> GetAllByUserIdAsync(string userId);
        Task<IEnumerable<Comment>> GetAllRecentByPostIdAsync(int postId);
        Task<int> CountByPostAsync(int postId);
        Task<bool> DeleteAllByPostAsync(int postId);
        Task<bool> ExistsAsync(string userId, int postId);




    }
}
