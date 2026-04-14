

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
            return await _context.Reviews.ToListAsync();
        }

        public async Task<Review?> GetByIdAsync(int id)
        {
            var result = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id);
            return result;
        }

        public async Task<bool> UpdateAsync(Review newReview)
        {
            var result = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == newReview.Id);
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
            var result = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id);
            if (result == null) { return false; }
            _context.Reviews.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }


      public async Task<IEnumerable<Review>> GetAllByProductIDAsync(int productId)
        {
            var reviews = await _context.Reviews.Where(r => r.ProductId == productId).ToListAsync();
            return reviews;
        }

        public async Task<int> CountByProductIdAsync(int productId)
        {
            var reviews = await _context.Reviews.CountAsync(r => r.ProductId == productId);
            return reviews;
        }

        public async Task<List<Review>> GetByUserIdAsync(string userId)
        {
            var reviews = await _context.Reviews.Where(r => r.UserId == userId).ToListAsync();
            return reviews;
        }

        public async Task<bool> ExistsAsync(string userId, int productId)
        {
            return await _context.Reviews.AnyAsync(r => r.UserId == userId && r.ProductId == productId);
        }


        public async Task<double> GetAverageRatingForProductAsync(int productId)
        {
            var reviews = _context.Reviews.Where(r => r.ProductId == productId);

            if (!await reviews.AnyAsync()) { return 0; }

            return await reviews.AverageAsync(r => r.Stars);
        }


    }
}
