using Greeny.DAL.Database;
using Greeny.DAL.Repository.Interfaces;
namespace Greeny.DAL.Repository.Implementation
{
    public class CommentRepo : ICommentRepo
    {

        private readonly GreenyDbContext _context;

        public CommentRepo(GreenyDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Comment comment)
        {
                await _context.Comments.AddAsync(comment);
                await _context.SaveChangesAsync();
        }


        public IQueryable<Comment?> GetById(int id)
        {
            // GetCommentPerUser
            // GetPostsPerUser
            return _context.Comments
            .Where(c => c.Id == id && !c.IsDeleted)
            .AsNoTracking();
        }


        public async Task DeleteAsync(int id)
        {
            await _context.Comments
                .Where(c => c.Id == id)
                .ExecuteUpdateAsync(setter => setter
                .SetProperty(p => p.IsDeleted, true)
                );
        }

        public IQueryable<Comment> GetAllByPostId(int postId)
        {
            return _context.Comments
                .AsNoTracking()
                .Where(c => c.PostId == postId && !c.IsDeleted)
                .OrderByDescending(n => n.Date);
            /* .Select(c => new CommentViewModel { // 4. Only pull the columns you NEED
                Content = c.Content,
                Date = c.Date,
                AuthorName = c.User.UserName
            })
            That's optimal, we may need it.
            */

        }

        public IQueryable<Comment> GetAllByUserId(string userId)
        {
            return _context.Comments
                .AsNoTracking()
                .Where(c => c.UserId == userId && !c.IsDeleted)
                .OrderByDescending(c => c.Date);
        }

        //public async Task<IEnumerable<Comment>> GetAllRecentByPostIdAsync(int postId)
        //{
        //    return await _context.Comments
        //        .Include(c => c.User)
        //        .Where(c => c.PostId == postId && !c.IsDeleted)
        //        .OrderByDescending(c => c.Date)
        //        .Take(10)
        //        .ToListAsync();
        //}

        public async Task<int> CountByPostAsync(int postId)
        {
            return await _context.Comments
                .Where(c => c.PostId == postId && !c.IsDeleted)
                .CountAsync();
        }

        //public async Task DeleteAllByPostAsync(int postId)
        //{
        //    var comments = await _context.Comments
        //        .Where(c => c.PostId == postId && !c.IsDeleted)
        //        .ToListAsync();

        //    if (!comments.Any())
        //        return false;

        //    foreach (var item in comments)
        //    {
        //        item.IsDeleted = true;
        //    }
        //    return await _context.SaveChangesAsync() > 0;
        //}

        public async Task<bool> ExistsAsync(string userId, int postId)
        {
            return await _context.Comments
            .AnyAsync(c => c.UserId == userId && c.PostId == postId && !c.IsDeleted);
        }

    }
}
