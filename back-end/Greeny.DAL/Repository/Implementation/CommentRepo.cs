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
                return await _context.Comments
                .Where(c=>!c.IsDeleted).ToListAsync();
        }

        public async Task<Comment?> GetByIdAsync(int id) 
        {
                return await _context.Comments
                .Include(c => c.User)
                .Include(c=>c.Post)
                .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
        }

        public async Task<bool> UpdateAsync(Comment newComment) 
        {
                var result = await _context.Comments.FirstOrDefaultAsync(c => c.Id == newComment.Id && !c.IsDeleted);

            if (result == null) { return false; }

                result.Content = newComment.Content;
                result.Votes = newComment.Votes;
                result.Date = DateTime.Now;
                await _context.SaveChangesAsync();
                return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
                var result = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);
                if (result == null) { return false; }

                result.IsDeleted = true;
                await _context.SaveChangesAsync();
                return true;
        }

        public async Task<IEnumerable<Comment>> GetAllByPostIdAsync(int postId)
        {
            return await _context.Comments
                .Include(c => c.User)
                .Where(c => c.PostId == postId && !c.IsDeleted)
                .OrderByDescending(n => n.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetAllByUserIdAsync(string userId)
        {
            return await _context.Comments
                .Where(c => c.UserId == userId && !c.IsDeleted)
                .OrderByDescending(c => c.Date)
                .ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetAllRecentByPostIdAsync(int postId)
        {
            return await _context.Comments
                .Include(c => c.User)
                .Where(c => c.PostId == postId && !c.IsDeleted)
                .OrderByDescending(c => c.Date)
                .Take(10)
                .ToListAsync();
        }

        public async Task<int> CountByPostAsync(int postId)
        {
            return await _context.Comments
            .CountAsync(c => c.PostId == postId && !c.IsDeleted);
        }

        public async Task<bool> DeleteAllByPostAsync(int postId)
        {
            var comments = await _context.Comments
                .Where(c => c.PostId == postId && !c.IsDeleted)
                .ToListAsync();

            if (!comments.Any())
                return false;

            foreach (var item in comments)
            {
                item.IsDeleted = true;
            }
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> ExistsAsync(string userId, int postId)
        {
            return await _context.Comments
            .AnyAsync(c => c.UserId == userId && c.PostId == postId && !c.IsDeleted);
        }

    }
}
