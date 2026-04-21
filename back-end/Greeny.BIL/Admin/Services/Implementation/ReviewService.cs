
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
            try
            {
                int result = await _reviewRepo.CountByProductIdAsync(productId);

                return new Response<int>
                {
                    IsSuccess = true,
                    Data = result,
                    Message = "Count retrieved successfully"
                };
            }
            catch (Exception ex)
            {
                return new Response<int>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response<string>> CreateAsync(CreateReviewVM vm)
        {
            try
            {
                if (vm == null)
                {
                    return new Response<string>
                    {
                        IsSuccess = false,
                        Message = "Invalid Data"
                    };
                }

                var review = _mapper.Map<Review>(vm);

                await _reviewRepo.CreateAsync(review);

                return new Response<string>
                {
                    IsSuccess = true,
                    Message = "Product Created Successfully"
                };
            }
            catch (Exception ex)
            {
                return new Response<string>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response<string>> DeleteAsync(int id)
        {
            try
            {
                var review = await _reviewRepo.GetByIdAsync(id);

                if (review == null || review.IsDeleted)
                {
                    return new Response<string>
                    {
                        IsSuccess = false,
                        Message = "review Not Found"
                    };
                }

                await _reviewRepo.DeleteAsync(id);

                return new Response<string>
                {
                    IsSuccess = true,
                    Message = "review Deleted Successfully"
                };
            }
            catch (Exception ex)
            {
                return new Response<string>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response<bool>> ExistsAsync(string userId, int productId)
        {
            try
            {
                var exists = await _reviewRepo.ExistsAsync(userId, productId);

                return new Response<bool>
                {
                    IsSuccess = true,
                    Data = exists,
                    Message = exists ? "Review Exists" : "Review Not Found"
                };
            }
            catch (Exception ex)
            {
                return new Response<bool>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response<IEnumerable<DetailsReviewVM>>> GetAllByProductIdAsync(int productId)
        {
            try
            {
                var reviews = await _reviewRepo.GetAllByProductIdAsync(productId);

                if (reviews == null || !reviews.Any())
                {
                    return new Response<IEnumerable<DetailsReviewVM>>
                    {
                        IsSuccess = false,
                        Message = "No reviews Found"
                    };
                }

                var data = _mapper.Map<IEnumerable<DetailsReviewVM>>(reviews);

                return new Response<IEnumerable<DetailsReviewVM>>
                {
                    IsSuccess = true,
                    Data = data,
                    Message = "reviews Retrieved Successfully"
                };
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<DetailsReviewVM>>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response<double>> GetAverageRatingForProductAsync(int productId)
        {
            try
            {
                double result = await _reviewRepo.GetAverageRatingForProductAsync(productId);

                return new Response<double>
                {
                    IsSuccess = true,
                    Data = result,
                    Message =  "Average Rating Retrive"
                };
            }
            catch (Exception ex)
            {
                return new Response<double>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response<DetailsReviewVM>> GetByIdAsync(int id)
        {
            try
            {
                var review = await _reviewRepo.GetByIdAsync(id);
                if (review == null)
                {
                    return new Response<DetailsReviewVM>
                    {
                        IsSuccess = false,
                        Message = "Not Found"
                    };
                }
                var data = _mapper.Map<DetailsReviewVM>(review);
                return new Response<DetailsReviewVM>
                {
                    IsSuccess = true,
                    Data = data,
                    Message = "review Retrieved Successfully"
                };

            }
            catch (Exception ex)
            {
                return new Response<DetailsReviewVM>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response<IEnumerable<DetailsReviewVM>>> GetByUserIdAsync(string userId)
        {
            try
            {
                var reviews = await _reviewRepo.GetByUserIdAsync(userId);
                if (reviews == null || !reviews.Any())
                {
                    return new Response<IEnumerable<DetailsReviewVM>>
                    {
                        IsSuccess = false,
                        Message = "Not Found"
                    };
                }
                var data = _mapper.Map<IEnumerable<DetailsReviewVM>>(reviews);
                return new Response<IEnumerable<DetailsReviewVM>>
                {
                    IsSuccess = true,
                    Data = data,
                    Message = "review Retrieved Successfully"
                };

            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<DetailsReviewVM>>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response<string>> UpdateAsync(UpdateReviewVM vm)
        {
            try
            {
                var review = await _reviewRepo.GetByIdAsync(vm.Id);

                if (review == null)
                {
                    return new Response<string>
                    {
                        IsSuccess = false,
                        Message = "review Not Found"
                    };
                }

                _mapper.Map(vm, review);
                await _reviewRepo.UpdateAsync(review);

                return new Response<string>
                {
                    IsSuccess = true,
                    Message = "review Updated Successfully"
                };
            }
            catch (Exception ex)
            {
                return new Response<string>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}
