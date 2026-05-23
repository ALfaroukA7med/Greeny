using Greeny.BLL.Abstraction;
using Greeny.BLL.ModelVM.Comment;
using Greeny.BLL.ModelVM.Post;


namespace Greeny.BLL.Services.Interfaces
{
    public interface IPostService
    {
        Task<int> GetVotes();
        Task<Result<IEnumerable<PostListVM>>> GetAllAsync();
        Task<Result<PostDetailsVM>> GetByIdAsync(int id);
        Task<Result> AddAsync(PostCreateVM post);

        //Task<Result<string>> UpdateAsync(PostCreateVM post);
        Task<Result> DeleteAsync(string userId, int id);
    }
}
