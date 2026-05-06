using Greeny.BLL.Admin.Errors;
using Greeny.BLL.Admin.ModelVM.Comment;
using Greeny.BLL.Admin.ModelVM.Post;
using Greeny.BLL.Extension;
using System.Reflection.Metadata;


namespace Greeny.BLL.Admin.Services.Implementation
{
   public class PostService : IPostService 
    {
        private readonly IPostRepo _postrepo;
        private readonly ICommentRepo _commentrepo;
        public PostService(IPostRepo postrepo, ICommentRepo commentrepo)
        {
            _postrepo = postrepo;
            _commentrepo = commentrepo;
        }
        public async Task<Response<bool>> AddAsync(PostListVM post)
        {
            var npost = new Post()
            {
                Content = post.Content,
                ImagePath = post.ImagePath
            };

            await _postrepo.CreateAsync(npost);

            return Response<bool>.Success(true);
        }

        public async Task<Response<bool>> DeleteAsync(int id)
        {
            var post = await _postrepo.GetByIdAsync(id).FirstOrDefaultAsync();

            if (post == null) return Response<bool>.Fail(PostError.NotFound);

            await _postrepo.DeleteAsync(id);

            return Response<bool>.Success(true);
        }

        public async Task<Response<IEnumerable<PostListVM>>> GetAllAsync()
        {
            var posts = await _postrepo
                .GetAll()
                .Select(p => new PostListVM{
                    ImagePath = p.ImagePath,
                    Content = p.Content
                })
                .ToListAsync();

            return Response<IEnumerable<PostListVM>>.Success(posts);
        }

        public async Task<Response<PostDetailsVM>> GetByIdAsync(int id)
        {
            var comments = await _commentrepo
                .GetAllByPostId(id)
                .Select(c => new CommentListVM
                {
                    Content = c.Content,
                    AuthorName = c.User.FirstName + ' ' + c.User.LastName,
                    TimeAgo = DateTimeExtensions.ToTimeAgo(c.Date)
                })
                .ToListAsync();
            
            var post = await _postrepo
                .GetByIdAsync(id)
                .Select(p => new PostDetailsVM
                {
                    ImagePath = p.ImagePath,
                    Content = p.Content,
                    AuthorName = p.User.FirstName + ' ' + p.User.LastName,
                    Comments = comments,
                    Votes = p.Votes
                }).FirstOrDefaultAsync();


            return Response<PostDetailsVM>.Success(post);
        }
    }
}
