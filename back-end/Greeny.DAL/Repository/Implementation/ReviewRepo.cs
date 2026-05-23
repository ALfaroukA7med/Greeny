using Greeny.DAL.Database;
using Greeny.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Greeny.DAL.Repository.Implementation
{
    public class ReviewRepo : IReviewRepo
    {
        private readonly GreenyDbContext _context;

        public ReviewRepo(GreenyDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Review review)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Review> GetAll()
        {
            return _context.Reviews
                .Where(r => !r.IsDeleted)
                .AsNoTracking();
        }

        public async Task<Review?> GetByIdAsync(int id)
        {
            return await _context.Reviews
                .FirstOrDefaultAsync(r => r.Id == id && !r.IsDeleted);
        }

        public async Task UpdateAsync(Review newReview)
        {
            await _context.Reviews
                .Where(r => r.Id == newReview.Id && !r.IsDeleted)
                .ExecuteUpdateAsync(setter => setter
                    .SetProperty(r => r.Content, newReview.Content)
                    .SetProperty(r => r.Stars, newReview.Stars)
                    .SetProperty(r => r.Date, DateTime.Now)
                );
        }

        public async Task DeleteAsync(int id)
        {
            await _context.Reviews
                .Where(r => r.Id == id && !r.IsDeleted)
                .ExecuteUpdateAsync(setter => setter
                    .SetProperty(r => r.IsDeleted, true)
                );
        }

        public IQueryable<Review> GetAllByProductId(int productId)
        {
            return _context.Reviews
                .Include(r => r.User)
                .Include(r => r.Product)
                .Where(r => r.ProductId == productId && !r.IsDeleted)
                .AsNoTracking();
        }

        public async Task<int> CountByProductIdAsync(int productId)
        {
            return await _context.Reviews
                .CountAsync(r => r.ProductId == productId && !r.IsDeleted);
        }

        public IQueryable<Review> GetByUserId(string userId)
        {
            return _context.Reviews
                .Where(r => r.UserId == userId && !r.IsDeleted)
                .AsNoTracking();
        }

        public async Task<bool> ExistsAsync(string userId, int productId)
        {
            return await _context.Reviews
                .AnyAsync(r => r.UserId == userId &&
                               r.ProductId == productId &&
                               !r.IsDeleted);
        }

        public async Task<double> GetAverageRatingForProductAsync(int productId)
        {
            return await _context.Reviews
                .Where(r => r.ProductId == productId && !r.IsDeleted)
                .Select(r => (double?)r.Stars)
                .AverageAsync() ?? 0;
        }

        public async Task<Review?> GetByUserAndProductAsync(string userId, int productId)
        {
            return await _context.Reviews
                .FirstOrDefaultAsync(r =>
                    r.UserId == userId &&
                    r.ProductId == productId &&
                    !r.IsDeleted);
        }
    }
}