using Greeny.BLL.Abstraction;
using Greeny.BLL.ModelVM.Comment;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;


namespace Greeny.BLL.Services.Interfaces
{
    public interface ICommentService
    {
        Task<Response<IEnumerable<CommentListVM>>> GetAllByPostId(int postid);
        Task<Response<CommentListVM>> GetByIdAsync(int id);
        Task<Response<bool>> AddAsync(CommentCreateVM comment);
        //Task<Response<bool>> UpdateAsync(CommentCreateVM comment);
        Task<Response<bool>> DeleteAsync(int id);
    }
}
