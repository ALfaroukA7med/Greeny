using Greeny.BLL.Abstraction;
using Greeny.BLL.Errors;
using Greeny.BLL.ModelVM.Review;
using Greeny.BLL.Services.Interfaces;
using Greeny.DAL.Repository.Implementation;

namespace Greeny.BLL.Services.Implementation
{
    public class ReviewService : IReviewService
    {
        private readonly IProductRepo _productRepo;
        private readonly IReviewRepo _reviewRepo;
        private readonly IUserRepo _userRepo;
        private readonly IMapper _mapper;

        public ReviewService(IReviewRepo reviewRepo,IProductRepo productRepo, IUserRepo userRepo, IMapper mapper)
        {
            _reviewRepo = reviewRepo;
            _productRepo = productRepo;
            _userRepo = userRepo;
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

            if (vm.Stars < 1 || vm.Stars > 5)
            {
                return Response<bool>.Fail(ReviewError.InvalidData);
            }

            var product = await _productRepo.GetByIdAsync(vm.ProductId);

            if (product == null)
            {
                return Response<bool>.Fail(ReviewError.NotFound);
            }

            var user = await _userRepo.GetByIdAsync(vm.UserId);

            if (user == null)
            {
                return Response<bool>.Fail(ReviewError.UserNotFound);
            }

            var existingReview = await _reviewRepo.GetByUserAndProductAsync(vm.UserId, vm.ProductId);

            if (existingReview != null)
            {
                // Update Existing Review

                existingReview.Stars = vm.Stars;
                existingReview.Content = vm.Content;
                existingReview.Date = DateTime.UtcNow;

                await _reviewRepo.UpdateAsync(existingReview);
            }
            else
            {
                // Create New Review

                var review = new Review
                {
                    ProductId = vm.ProductId,
                    UserId = vm.UserId,
                    Stars = vm.Stars,
                    Content = vm.Content,
                    Date = DateTime.UtcNow
                };

                await _reviewRepo.CreateAsync(review);
            }

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
