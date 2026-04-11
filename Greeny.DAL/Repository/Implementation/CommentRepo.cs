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

        public async Task<Comment?> GetByIdAsync(string id) 
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

        public async Task<bool> DeleteAsync(string id)
        {
                var result = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
                if (result == null) { return false; }

                _context.Comments.Remove(result);
                await _context.SaveChangesAsync();
                return true;
        }

    }
}
