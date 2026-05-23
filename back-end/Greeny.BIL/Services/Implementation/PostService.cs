using Greeny.BLL.Abstraction;
using Greeny.BLL.ModelVM.Comment;
using Greeny.BLL.Errors;
using Greeny.BLL.Extension;
using Greeny.BLL.ModelVM.Post;
using Greeny.BLL.Services.Interfaces;
using System.Reflection.Metadata;
using Org.BouncyCastle.Bcpg.Sig;


namespace Greeny.BLL.Services.Implementation
{
    public class PostService : IPostService
    {
        private readonly IPostRepo _postrepo;
        private readonly ICommentRepo _commentrepo;
        private readonly IUserRepo _userrepo;
        public PostService(IPostRepo postrepo, ICommentRepo commentrepo, IUserRepo userrepo)
        {
            _postrepo = postrepo;
            _commentrepo = commentrepo;
            _userrepo = userrepo;
        }
        public async Task<Result> AddAsync(PostCreateVM post)
        {
            var npost = new Post()
            {
                UserId = post.UserId,
                Content = post.Content,
                ImagePath = post.ImagePath
            };

            await _postrepo.CreateAsync(npost);

            return Result.Success();
        }

        public async Task<Result> DeleteAsync(string userId, int id)
        {
            var post = await _postrepo.GetByIdAsync(id).FirstOrDefaultAsync();

            if (post == null) return Result.Failure(PostError.NotFound);
            var currentUser = await _userrepo.GetByIdAsync(userId);

            if (currentUser == null)
            {
                return Result.Failure(UserError.NotFound); // Or a generic unauthorized error
            }
            if (currentUser.Id != userId)
            {
                return Result.Failure(UserError.Unauthorized);
            }

            // implement user admin can delete too..
            await _postrepo.DeleteAsync(id);

            return Result.Success();
        }

        public Task<Result<IEnumerable<PostListVM>>> Feed()
        {
            throw new NotImplementedException();
        }
        public async Task<Result<IEnumerable<PostListVM>>> GetAllAsync()
        {
            var posts = await _postrepo
                .GetAll().OrderByDescending(p => p.Date)
                .Select(p => new PostListVM {
                    Id = p.Id,
                    UserId = p.UserId,
                    Votes = p.Votes,
                    FormattedDate = DateTimeExtensions.ToTimeAgo(p.Date),
                    AuthorName = $"{p.User.FirstName} {p.User.LastName}",
                    ImagePath = p.ImagePath,
                    Content = p.Content
                })
                .ToListAsync();

            return Result.Success<IEnumerable<PostListVM>>(posts);
        }

        public async Task<Result<PostDetailsVM>> GetByIdAsync(int id)
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


            return Result.Success(post);
        }
    }
}
