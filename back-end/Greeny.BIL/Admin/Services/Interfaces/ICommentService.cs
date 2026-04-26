using Greeny.BLL.Admin.ModelVM.Comment;


namespace Greeny.BLL.Admin.Services.Interfaces
{
    public interface ICommentService
    {
        Task<Response<IEnumerable<CommentListVM>>> GetAllAsync();
        Task<Response<CommentListVM>> GetByIdAsync(int id);
        Task<Response<bool>> CreateAsync(CommentCreateVM comment);
        Task<Response<bool>> UpdateAsync(CommentCreateVM comment);
        Task<Response<bool>> DeleteAsync(int id);
    }
}
