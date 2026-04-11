namespace Greeny.DAL.Repository.Interfaces
{
    public interface IReviewRepo
    {
        Task<IEnumerable<Review>> GetAllAsync();

        Task<Review> GetByIdAsync(string id);

        Task<bool> CreateAsync(Review review);

        Task<bool> UpdateAsync(Review review);

        Task<bool> DeleteAsync(string id);
    }
}
