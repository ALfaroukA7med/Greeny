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

        public async Task CreateAsync(Post post)
        {
                await _context.Posts.AddAsync(post);
                await _context.SaveChangesAsync();
        }

        public IQueryable<Post> GetAll()
        {
                return _context.Posts.Where(p => !p.IsDeleted).AsNoTracking();
        }

        public IQueryable<Post> GetByIdAsync(int id)
        {
            return _context.Posts
            .Where(p => p.Id == id && !p.IsDeleted);
        }

        public async Task UpdateAsync(Post newPost)
        {
            await _context.Posts.Where(p => p.Id == newPost.Id && !p.IsDeleted)
               .ExecuteUpdateAsync(setter => setter
               .SetProperty(p => p.Votes, newPost.Votes)
               .SetProperty(p => p.Content, newPost.Content)
               .SetProperty(p => p.Date, DateTime.Now)
               .SetProperty(p => p.ImagePath, newPost.ImagePath));
        }

        public async Task DeleteAsync(int id)
        {
            await _context.Posts.Where(p => p.Id == id && !p.IsDeleted)
          .ExecuteUpdateAsync(setter => setter
          .SetProperty(p => p.IsDeleted, true));
        }

        //public  IQueryable<Post> SearchByKeyword(string keyword)
        //{
        //    return  _context.Posts
        //        .Where(p => EF.Functions.Like(p.Content, $"%{keyword}%") && !p.IsDeleted);
        //}

        public IQueryable<Post> GetRecent()
        {
            return _context.Posts
           .Where(p => !p.IsDeleted)
           .OrderByDescending(p => p.Date)
           .Take(10)
           .AsNoTracking();
        }


        public async Task<int> CountByUserIdAsync(string userId)
        {
            return await _context.Posts
                .CountAsync(p => p.UserId == userId && !p.IsDeleted);
        }

        //public async Task<bool> ExistPostsAsync(string userId)
        //{
        //    return await _context.Posts
        //        .AnyAsync(p => p.UserId == userId && !p.IsDeleted);
        //}

        //public async Task DeleteAllByUserIdAsync(string userId)
        //{
        //    var posts = await _context.Posts
        //   .Where(p => p.UserId == userId && !p.IsDeleted)
        //   .ToListAsync();

        //    foreach (var post in posts)
        //    {
        //        post.IsDeleted = true;
        //    }

        //    await _context.SaveChangesAsync();
        //}


        public IQueryable<Post> GetAllByUserId(string userId)
        {
            return _context.Posts
           .Where(p => p.UserId == userId && !p.IsDeleted).AsNoTracking();
        }


    }
}
