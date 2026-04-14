using Greeny.DAL.Repository.Interfaces;
using Greeny.DAL.Database;
namespace Greeny.DAL.Repository.Implementation
{
    public class CommentRepo : ICommentRepo
    {

        private readonly GreenyDbContext _context;

        public CommentRepo(GreenyDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(Comment comment)
        {
                await _context.Comments.AddAsync(comment);
                await _context.SaveChangesAsync();
                return true;
        }

        public async Task<IEnumerable<Comment>> GetAllAsync()
        {
                return await _context.Comments.ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id) 
        {
                var result = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
                return result;
        }

        public async Task<bool> UpdateAsync(Comment newComment) 
        {
                var result = await _context.Comments.FirstOrDefaultAsync(c => c.Id == newComment.Id);

            if (result == null) { return false; }

                result.Content = newComment.Content;
                result.Votes = newComment.Votes;
                result.Date = DateTime.Now;
                await _context.SaveChangesAsync();
                return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
                var result = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
                if (result == null) { return false; }

                _context.Comments.Remove(result);
                await _context.SaveChangesAsync();
                return true;
        }

        public async Task<IEnumerable<Comment>> GetAllByPostIdAsync(int postId)
        {
            return await _context.Comments
                .Where(c => c.PostId == postId)
                .Include(c => c.User)
                .OrderBy(c => c.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetAllByUserIdAsync(string userId)
        {
            return await _context.Comments
                .Where(c => c.UserId == userId)
                .Include(c => c.Post)
                .OrderByDescending(c => c.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetAllRecentByPostIdAsync(int postId)
        {
            return await _context.Comments
                .Where(c => c.PostId == postId)
                .OrderByDescending(c => c.Date)
                .Take(10)
                .ToListAsync();
        }

        public async Task<int> CountByPostAsync(int postId)
        {
            return await _context.Comments
                .CountAsync(c => c.PostId == postId);
        }

        public async Task<bool> DeleteAllByPostAsync(int postId)
        {
            var comments = await _context.Comments
                .Where(c => c.PostId == postId)
                .ToListAsync();

            if (!comments.Any())
                return false;

            _context.Comments.RemoveRange(comments);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsAsync(string userId, int postId)
        {
            return await _context.Comments
                .AnyAsync(c => c.UserId == userId && c.PostId == postId);
        }

    }
}
