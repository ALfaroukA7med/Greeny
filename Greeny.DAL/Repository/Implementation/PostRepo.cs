using Greeny.DAL.Database;
using Greeny.DAL.Repository.Interfaces;

namespace Greeny.DAL.Repository.Implementation
{
    public class PostRepo : IPostRepo
    {
        private readonly GreenyDbContext _context;

        public PostRepo(GreenyDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(Post post)
        {
                await _context.Posts.AddAsync(post);
                await _context.SaveChangesAsync();
                return true;     
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
                return await _context.Posts.ToListAsync();
        }

        public async Task<Post?> GetByIdAsync(string id)
        {
            var result = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
                return result;
        }

        public async Task<bool> UpdateAsync(Post newPost)
        {
            var result = await _context.Posts.FirstOrDefaultAsync(p => p.Id == newPost.Id);
            if (result == null)
            {
                return false;
            }
                result.Votes = newPost.Votes;
                result.Content = newPost.Content;
                result.Date = DateTime.Now;


            if (!string.IsNullOrEmpty(newPost.ImagePath))
            {
                result.ImagePath = newPost.ImagePath;
            }


            await _context.SaveChangesAsync();
                return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
                var result = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
                if (result == null) { return false; }
                _context.Posts.Remove(result);
                await _context.SaveChangesAsync();
                return true;
        }

        public async Task<IEnumerable<Post>> SearchAsync(string keyword)
        {
            return await _context.Posts
                .Where(p => p.Content.Contains(keyword))
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetRecentAsync()
        {
           var date = DateTime.Now;
            return await _context.Posts
                .Where(p => p.Date >= date)
                .OrderByDescending(p => p.Date)
                .Take(10)
                .ToListAsync();
        }


        public async Task<int> CountByUserIdAsync(string userId)
        {
            return await _context.Posts
                .CountAsync(p => p.UserId == userId);
        }

        public async Task<bool> HasPostsAsync(string userId)
        {
            return await _context.Posts
                .AnyAsync(p => p.UserId == userId);
        }

        public async Task<bool> DeleteAllByUserIdAsync(string userId)
        {
            var rows = await _context.Posts
                .Where(p => p.UserId == userId)
                .ExecuteDeleteAsync();

            return rows > 0;
        }
    }
}
