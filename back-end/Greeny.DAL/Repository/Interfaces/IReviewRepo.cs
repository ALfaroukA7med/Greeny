namespace Greeny.DAL.Repository.Interfaces
{
    public interface IReviewRepo
    {
        Task<IEnumerable<Review>> GetAllAsync();

        Task<Review> GetByIdAsync(int id);

        Task<bool> CreateAsync(Review review);

        Task<bool> UpdateAsync(Review review);

        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<Review>> GetAllByProductIdAsync(int productId);
        Task<int> CountByProductIdAsync(int productId);
        Task<IEnumerable<Review>> GetByUserIdAsync(string userId);
        Task<bool> ExistsAsync(string userId, int productId);
        Task<double> GetAverageRatingForProductAsync(int productId);
    }
}
