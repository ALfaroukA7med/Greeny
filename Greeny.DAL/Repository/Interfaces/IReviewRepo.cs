namespace Greeny.DAL.Repository.Interfaces
{
    public interface IReviewRepo
    {
        Task<IEnumerable<Review>> GetAllAsync();

        Task<Review> GetByIdAsync(string id);

        Task<bool> CreateAsync(Review review);

        Task<bool> UpdateAsync(Review review);

        Task<bool> DeleteAsync(string id);

        Task<IEnumerable<Review>> GetAllByProductIDAsync(string productId);
        Task<int> CountByProductIdAsync(string productId);
        Task<List<Review>> GetByUserIdAsync(string userId);
        Task<bool> ExistsAsync(string userId, string productId);
        Task<double> GetAverageRatingForProductAsync(string productId);
    }
}
