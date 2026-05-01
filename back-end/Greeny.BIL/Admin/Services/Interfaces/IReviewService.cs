
namespace Greeny.BLL.Admin.Services.Interfaces
{
    public interface IReviewService
    {
        Task<Response<DetailsReviewVM>> GetByIdAsync(int id);
        Task<Response<bool>> CreateAsync(CreateReviewVM vm);
        Task<Response<bool>> UpdateAsync(UpdateReviewVM vm);
        Task<Response<bool>> DeleteAsync(int id);

        Task<Response<IEnumerable<DetailsReviewVM>>> GetAllByProductIdAsync(int productId);
        Task<Response<int>> CountByProductIdAsync(int productId);
        Task<Response<IEnumerable<DetailsReviewVM>>> GetByUserIdAsync(string userId);
        Task<Response<bool>> ExistsAsync(string userId, int productId);
        Task<Response<double>> GetAverageRatingForProductAsync(int productId);
    }
}
