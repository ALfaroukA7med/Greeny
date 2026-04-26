using Greeny.BLL.Admin.ModelVM.Comment;
using Greeny.BLL.Admin.ModelVM.Post;


namespace Greeny.BLL.Admin.Services.Interfaces
{
    public interface IPostService
    {
        Task<Response<IEnumerable<PostListVM>>> GetAllAsync();
        Task<Response<PostDetailsVM>> GetByIdAsync(int id);
        Task<Response<bool>> AddAsync(PostListVM post);

        //Task<Response<string>> UpdateAsync(PostCreateVM post);
        Task<Response<bool>> DeleteAsync(int id);
    }
}
