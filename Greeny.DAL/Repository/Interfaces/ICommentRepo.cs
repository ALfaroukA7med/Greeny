
namespace Greeny.DAL.Repository.Interfaces
{
    public interface ICommentRepo
    {
        Task<IEnumerable<Comment>> GetAllAsync();

        Task<Comment?> GetByIdAsync(string id);

        Task<bool> CreateAsync(Comment comment);

        Task<bool> UpdateAsync(Comment comment);

        Task<bool> DeleteAsync(string id);
    }
}
