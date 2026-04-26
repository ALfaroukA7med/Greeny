
using Greeny.BLL.Admin.Errors;

namespace Greeny.BLL.Admin.Services.Implementation
{
    public class ReviewService : IReviewService
    {

        private readonly IReviewRepo _reviewRepo;
        private readonly IMapper _mapper;

        public ReviewService(IReviewRepo reviewRepo, IMapper mapper)
        {
            _reviewRepo = reviewRepo;
            _mapper = mapper;
        }

        public async Task<Response<int>> CountByProductIdAsync(int productId)
        {
                int result = await _reviewRepo.CountByProductIdAsync(productId);

                return Response<int>.Success(result);
        }

        public async Task<Response<string>> CreateAsync(CreateReviewVM vm)
        {
                if (vm == null)
                {
                    return Response<string>.Fail(ReviewError.InvalidData);
                }

                var review = _mapper.Map<Review>(vm);

                await _reviewRepo.CreateAsync(review);

            return Response<string>.Success("Create Successfully");
               
        }

        public async Task<Response<string>> DeleteAsync(int id)
        {
                var review = await _reviewRepo.GetByIdAsync(id);

                if (review == null)
                {
                return Response<string>.Fail(ReviewError.InvalidData);
                }

                await _reviewRepo.DeleteAsync(id);

            return Response<string>.Success("Deleted Successfully");
        }

        public async Task<Response<bool>> ExistsAsync(string userId, int productId)
        {
                var exists = await _reviewRepo.ExistsAsync(userId, productId);

                return Response<bool>.Success(exists);
        }

        public async Task<Response<IEnumerable<DetailsReviewVM>>> GetAllByProductIdAsync(int productId)
        {
            var Query = _reviewRepo.GetAllByProductId(productId);
                var reviews = await Query.ToArrayAsync();

                if (reviews == null || !reviews.Any())
                {
                return Response<IEnumerable<DetailsReviewVM>>.Fail(ReviewError.NotFound);
                }

                var data = _mapper.Map<IEnumerable<DetailsReviewVM>>(reviews);

                return Response<IEnumerable<DetailsReviewVM>>.Success(data);
        }

        public async Task<Response<double>> GetAverageRatingForProductAsync(int productId)
        {
               double result = await _reviewRepo.GetAverageRatingForProductAsync(productId);

                return Response<double>.Success(result);
        }

        public async Task<Response<DetailsReviewVM>> GetByIdAsync(int id)
        {
                var review = await _reviewRepo.GetByIdAsync(id);
                if (review == null)
                {
                    return Response<DetailsReviewVM>.Fail(ReviewError.NotFound);
                }
                var data = _mapper.Map<DetailsReviewVM>(review);
                return Response<DetailsReviewVM>.Success(data);
        }

        public async Task<Response<IEnumerable<DetailsReviewVM>>> GetByUserIdAsync(string userId)
        {
                var Query = _reviewRepo.GetByUserId(userId);
            var reviews = await Query.ToArrayAsync();
                if (reviews == null || !reviews.Any())
                {
                    return Response<IEnumerable<DetailsReviewVM>>.Fail(ReviewError.NotFound);
                }
                var data = _mapper.Map<IEnumerable<DetailsReviewVM>>(reviews);
                return Response<IEnumerable<DetailsReviewVM>>.Success(data);
        }

        public async Task<Response<string>> UpdateAsync(UpdateReviewVM vm)
        {
                var review = await _reviewRepo.GetByIdAsync(vm.Id);

                if (review == null)
                {
                    return Response<string>.Fail(ReviewError.NotFound);
                }

                _mapper.Map(vm, review);
                await _reviewRepo.UpdateAsync(review);

            return Response<string>.Success("Updated Successfully");
        }

    }
}
