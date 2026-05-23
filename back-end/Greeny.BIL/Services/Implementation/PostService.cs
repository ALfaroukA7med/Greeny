using Greeny.BLL.Abstraction;
using Greeny.BLL.Errors;
using Greeny.BLL.Extension;
using Greeny.BLL.Helper;
using Greeny.BLL.ModelVM.Comment;
using Greeny.BLL.ModelVM.Post;
using Greeny.BLL.Services.Interfaces;
using Org.BouncyCastle.Bcpg.Sig;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;


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
            string dbPath = "";
            if (post.ImageFile != null)
            {

                string userId = post.UserId; // e.g., "0a770f42-b253-..."
                string postId = Guid.NewGuid().ToString(); // e.g., "8616000a-..."

                // 1. Define the relative inner folder path inside wwwroot
                string relativeFolder = "Users/" + userId + "/Posts/" + postId;

                // 2. CRITICAL FIX: Match exactly what your Upload helper expects!
                // Since the helper appends Directory.GetCurrentDirectory() + "/wwwroot/" by itself,
                // we must create the directory on the exact same full path structure.
                string absolutePhysicalPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativeFolder);

                // Create the full folder structure physically so the Stream doesn't crash
                if (!Directory.Exists(absolutePhysicalPath))
                {
                    Directory.CreateDirectory(absolutePhysicalPath);
                }

                string savedFileName = Upload.UploadFile(relativeFolder, post.ImageFile);
                dbPath = (relativeFolder + "/" + savedFileName).Replace("\\", "/");
            }

            var npost = new Post()
            {
                UserId = post.UserId,
                Content = post.Content,
                ImagePath = dbPath
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
                    Id = c.Id,
                    UserId = c.UserId,
                    Content = c.Content,
                    AuthorName = c.User.FirstName + ' ' + c.User.LastName,
                    TimeAgo = DateTimeExtensions.ToTimeAgo(c.Date)
                })
                .ToListAsync();
            
            var post = await _postrepo
                .GetByIdAsync(id)
                .Select(p => new PostDetailsVM
                {
                    Id = p.Id,
                    ImagePath = p.ImagePath,
                    Content = p.Content,
                    AuthorName = p.User.FirstName + ' ' + p.User.LastName,
                    Comments = comments,
                    Votes = p.Votes,
                    FormattedDate = DateTimeExtensions.ToTimeAgo(p.Date),
                }).FirstOrDefaultAsync();


            return Result.Success(post);
        }
        public async Task<int> GetVotes()
        {
            return await _postrepo.GetVotes();
        }
    }
}
