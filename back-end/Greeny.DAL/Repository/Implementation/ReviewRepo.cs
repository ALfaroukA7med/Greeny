

using Greeny.DAL.Database;
using Greeny.DAL.Repository.Interfaces;

namespace Greeny.DAL.Repository.Implementation
{
    public class ReviewRepo : IReviewRepo
    {
        private readonly GreenyDbContext _context;

        public ReviewRepo(GreenyDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(Review review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Review>> GetAllAsync()
        {
            return await _context.Reviews.Where(r=>!r.IsDeleted).ToListAsync();
        }

        public async Task<Review?> GetByIdAsync(int id)
        {
            return await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);
        }

        public async Task<bool> UpdateAsync(Review newReview)
        {
            var result = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == newReview.Id && !r.IsDeleted);
            if (result == null)
            {
                return false;
            }
            result.Content = newReview.Content;
            result.Stars = newReview.Stars;
            result.Date = DateTime.Now;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var result = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);
            if (result == null) { return false; }

            result.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }


      public async Task<IEnumerable<Review>> GetAllByProductIDAsync(int productId)
        {
            return await _context.Reviews.Where(r => r.ProductId == productId && !r.IsDeleted).ToListAsync();     
        }

        public async Task<int> CountByProductIdAsync(int productId)
        {
            return await _context.Reviews.CountAsync(r => r.ProductId == productId && !r.IsDeleted);     
        }

        public async Task<IEnumerable<Review>> GetByUserIdAsync(string userId)
        {
            return await _context.Reviews.Where(r => r.UserId == userId && !r.IsDeleted).ToListAsync();
        }

        public async Task<bool> ExistsAsync(string userId, int productId)
        {
            return await _context.Reviews.AnyAsync(r => r.UserId == userId && r.ProductId == productId && !r.IsDeleted);
        }


        public async Task<double> GetAverageRatingForProductAsync(int productId)
        {
            return await _context.Reviews
           .Where(r => r.ProductId == productId && !r.IsDeleted)
           .Select(r => (double?)r.Stars)
           .AverageAsync() ?? 0;
        }


    }
}
