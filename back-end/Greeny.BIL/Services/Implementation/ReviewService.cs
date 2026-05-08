using Greeny.BLL.Abstraction;
using Greeny.BLL.Errors;
using Greeny.BLL.ModelVM.Review;
using Greeny.BLL.Services.Interfaces;

namespace Greeny.BLL.Services.Implementation
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

        public async Task<Response<bool>> CreateAsync(CreateReviewVM vm)
        {
                if (vm == null)
                {
                    return Response<bool>.Fail(ReviewError.InvalidData);
                }

                var review = _mapper.Map<Review>(vm);

                await _reviewRepo.CreateAsync(review);

            return Response<bool>.Success(true);
               
        }

        public async Task<Response<bool>> DeleteAsync(int id)
        {
                var review = await _reviewRepo.GetByIdAsync(id);

                if (review == null)
                {
                return Response<bool>.Fail(ReviewError.InvalidData);
                }

                await _reviewRepo.DeleteAsync(id);

            return Response<bool>.Success(true);
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

        public async Task<Response<bool>> UpdateAsync(UpdateReviewVM vm)
        {
                var review = await _reviewRepo.GetByIdAsync(vm.Id);

                if (review == null)
                {
                    return Response<bool>.Fail(ReviewError.NotFound);
                }

                _mapper.Map(vm, review);
                await _reviewRepo.UpdateAsync(review);

            return Response<bool>.Success(true);
        }

    }
}
