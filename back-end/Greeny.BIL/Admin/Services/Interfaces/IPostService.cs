using Greeny.BLL.Admin.ModelVM.Comment;
using Greeny.BLL.Admin.ModelVM.Post;


namespace Greeny.BLL.Admin.Services.Interfaces
{
    public interface IPostService
    {
        Task<Response<IEnumerable<PostCreateVM>>> GetAllAsync();
        Task<Response<PostListVM>> GetByIdAsync(int id);
        Task<Response<bool>> CreateAsync(PostCreateVM post);

        //Task<Response<string>> UpdateAsync(PostCreateVM post);
        Task<Response<bool>> DeleteAsync(int id);
    }
}
