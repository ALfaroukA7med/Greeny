namespace Greeny.DAL.Repository.Interfaces
{
    public interface IReviewRepo
    {
        IQueryable<Review> GetAll();

        Task<Review> GetByIdAsync(int id);

        Task CreateAsync(Review review);

        Task UpdateAsync(Review review);

        Task DeleteAsync(int id);

        IQueryable<Review> GetAllByProductId(int productId);
        Task<int> CountByProductIdAsync(int productId);
        IQueryable<Review> GetByUserId(string userId);
        Task<bool> ExistsAsync(string userId, int productId);
        Task<double> GetAverageRatingForProductAsync(int productId);
        Task<Review?> GetByUserAndProductAsync(string userId, int productId);
    }
}
