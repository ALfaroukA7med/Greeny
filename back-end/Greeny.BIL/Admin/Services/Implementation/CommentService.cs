using Greeny.BLL.Admin.Errors;
using Greeny.BLL.Admin.ModelVM.Comment;
using Greeny.BLL.Extension;
using Microsoft.EntityFrameworkCore.Storage.Json;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;

namespace Greeny.BLL.Admin.Services.Implementation
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepo _commentrepo;
        private readonly IPostRepo _postrepo;
        public CommentService(ICommentRepo commentRepo, IPostRepo postrepo)
        {
            _commentrepo = commentRepo;
            _postrepo = postrepo;
        }

        public async Task<Response<bool>> AddAsync(CommentCreateVM comment)
        {
            var post = await _postrepo.GetByIdAsync(comment.PostId).FirstOrDefaultAsync();

            if (post == null) return Response<bool>.Fail(PostError.NotFound);
            var c = new Comment
            {
                Content = comment.Content,
                PostId = comment.PostId
            };
            await _commentrepo.CreateAsync(c);

            return Response<bool>.Success(true);
        }

        public async Task<Response<bool>> DeleteAsync(int id)
        {
            var c = await _commentrepo.GetById(id).FirstOrDefaultAsync();

            if (c == null) return Response<bool>.Fail(CommentError.NotFound);

            return Response<bool>.Success(true);
        }

        public async Task<Response<IEnumerable<CommentListVM>>> GetAllByPostId(int postid)
        {
            var post = await _postrepo.GetByIdAsync(postid).FirstOrDefaultAsync();

            if (post == null) return Response<IEnumerable<CommentListVM>>.Fail(PostError.NotFound);

            var comments = await _commentrepo
                .GetAllByPostId(postid)
                .Select(p => new CommentListVM
                {
                    Content = p.Content,
                    AuthorName = p.User.FirstName + ' ' + p.User.LastName,
                    TimeAgo = DateTimeExtensions.ToTimeAgo(p.Date)
                })
                .ToListAsync();

            return Response<IEnumerable<CommentListVM>>.Success(comments);
        }

        public async Task<Response<CommentListVM>> GetByIdAsync(int id)
        {
            var comment = await _commentrepo.GetById(id).FirstOrDefaultAsync();
            if (comment == null) return Response<CommentListVM>.Fail(CommentError.NotFound);

            var post = await _postrepo.GetByIdAsync(comment.PostId).FirstOrDefaultAsync();
            if (post == null) return Response<CommentListVM>.Fail(PostError.NotFound);

            //Content
            //AuthorName
            //TimeAgo

            var clist = new CommentListVM
            {
                Content = comment.Content,
                AuthorName = comment.User.FirstName + " " + comment.User.LastName,
                TimeAgo = DateTimeExtensions.ToTimeAgo(comment.Date)
            };

            return Response<CommentListVM>.Success(clist);
        }

    }
}
