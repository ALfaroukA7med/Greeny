using Greeny.BLL.Admin.ModelVM.Comment;
using Greeny.BLL.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

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
