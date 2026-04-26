
namespace Greeny.DAL.Repository.Interfaces
{
    public interface IPostRepo
    {
        IQueryable<Post> GetAll();

        IQueryable<Post> GetByIdAsync(int id);

        Task CreateAsync(Post post);

        //Task UpdateAsync(Post post);
         
        Task DeleteAsync(int id);

        //IQueryable<Post> SearchByKeyword(string keyword);
        IQueryable<Post> GetRecent();

        Task<int> CountByUserIdAsync(string userId);

        //Task<bool> ExistPostsAsync(string userId);

        //Task<bool> DeleteAllByUserIdAsync(string userId);

        IQueryable<Post> GetAllByUserId(string userId);


    }
}
