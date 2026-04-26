
namespace Greeny.DAL.Repository.Interfaces
{
    public interface ICommentRepo
    {
        IQueryable<Comment?> GetById(int id);

        Task CreateAsync(Comment comment);

        //Task UpdateAsync(Comment comment);

        Task DeleteAsync(int id);
        IQueryable<Comment> GetAllByPostId(int postId);
        IQueryable<Comment> GetAllByUserId(string userId);
        //IQueryable<Comment> GetAllRecentByPostIdAsync(int postId);
        Task<int> CountByPostAsync(int postId);
        //Task<bool> DeleteAllByPostAsync(int postId);
        Task<bool> ExistsAsync(string userId, int postId);




    }
}
