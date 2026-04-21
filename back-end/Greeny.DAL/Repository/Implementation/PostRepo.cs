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
                return await _context.Posts.Where(p => !p.IsDeleted).ToListAsync();
        }

        public async Task<Post?> GetByIdAsync(int id)
        {
            return await _context.Posts
            .Include(p=>p.Comments)
            .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        }

        public async Task<bool> UpdateAsync(Post newPost)
        {
            var result = await _context.Posts.FirstOrDefaultAsync(p => p.Id == newPost.Id && !p.IsDeleted);
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

        public async Task<bool> DeleteAsync(int id)
        {
                var result = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
                if (result == null) { return false; }

                result.IsDeleted = true;
                await _context.SaveChangesAsync();
                return true;
        }

        public async Task<IEnumerable<Post>> SearchAsync(string keyword)
        {
            return await _context.Posts
                .Where(p => EF.Functions.Like(p.Content, $"%{keyword}%") && !p.IsDeleted)
                .ToListAsync();
        }

        public async Task<IEnumerable<Post>> GetRecentAsync()
        {
            return await _context.Posts
           .Where(p => !p.IsDeleted)
           .OrderByDescending(p => p.Date)
           .Take(10)
           .ToListAsync();
        }


        public async Task<int> CountByUserIdAsync(string userId)
        {
            return await _context.Posts
                .CountAsync(p => p.UserId == userId && !p.IsDeleted);
        }

        public async Task<bool> ExistPostsAsync(string userId)
        {
            return await _context.Posts
                .AnyAsync(p => p.UserId == userId && !p.IsDeleted);
        }

        public async Task<bool> DeleteAllByUserIdAsync(string userId)
        {
            var posts = await _context.Posts
           .Where(p => p.UserId == userId && !p.IsDeleted)
           .ToListAsync();

            foreach (var post in posts)
            {
                post.IsDeleted = true;
            }

            await _context.SaveChangesAsync();
            return true;
        }


        public async Task<IEnumerable<Post>> GetAllByUserIdAsync(string userId)
        {
            return await _context.Posts.Include(p=>p.Comments)
           .Where(p => p.UserId == userId && !p.IsDeleted).ToListAsync();
        }


    }
}
