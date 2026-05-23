using Greeny.BLL.Abstraction;
using Greeny.BLL.Helper;

namespace Greeny.BLL.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;
        private readonly IReviewRepo _reviewRepo;
        private readonly ICategoryRepo _categoryRepo;
        private readonly IMapper _mapper;

        public ProductService(IProductRepo productRepo, IReviewRepo reviewRepo, ICategoryRepo categoryRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _reviewRepo = reviewRepo;
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        // Get All
        public async Task<Response<MarketPlaceVM>> GetAllAsync(string? searchTerm, string? categoryName, string? sortOrder)
        {
            var query = _productRepo.GetAll();

            // SEARCH

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(p =>
                    p.Name.Contains(searchTerm));
            }

            // CATEGORY

            if (!string.IsNullOrWhiteSpace(categoryName)
                && categoryName != "All")
            {
                query = query.Where(p => p.Category.Name.ToLower() == categoryName.ToLower());
            }

            // SORT

            query = sortOrder switch
            {
                "priceAsc" => query.OrderBy(p => p.Price),

                "priceDesc" => query.OrderByDescending(p => p.Price),

                "rating" => query.OrderByDescending(p => p.Reviews.Any() ? p.Reviews.Average(r => r.Stars) : 0),

                _ => query.OrderByDescending(p => p.Id)
            };

            var products = await query
                .Select(p => new ProductListVM
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    Price = p.Price,

                    Rating = p.Reviews.Any() ? p.Reviews.Average(r => r.Stars) : 0,
                    Quantity = p.Quantity,
                    TotalReviews = p.Reviews.Count(),
                    CategoryName = p.Category.Name,
                    Image = p.Image
                })
                .ToListAsync();
            var categories = _categoryRepo.GetAll().Select(c => c.Name).ToList();

            var vm = new MarketPlaceVM
            {
                Products = products,
                Categories = categories
            };

            return Response<MarketPlaceVM>.Success(vm);
        }

        // Get By Id
        public async Task<Response<DetailsProductVM>> GetByIdAsync(int id)
        {
            var product = await _productRepo.GetByIdAsync(id);
            if (product == null)
            {
                return Response<DetailsProductVM>.Fail(ProductError.NotFound);
            }
            var data = _mapper.Map<DetailsProductVM>(product);
            data.averageRating = await _reviewRepo.GetAverageRatingForProductAsync(id);
            data.totalReviews = await _reviewRepo.CountByProductIdAsync(id);
            data.Reviews = _reviewRepo.GetAllByProductId(id).Select(r => new DetailsReviewVM
            {
                Id = r.Id,
                Content = r.Content,
                Stars = r.Stars,
                Date = r.Date,
                UserId = r.UserId,
                UserName = r.User.FirstName + " " + r.User.LastName,
                ProductId = r.ProductId,
                ProductName = r.Product.Name,
                UserImage = r.User.ProfilePicture
            }).ToList();
            return Response<DetailsProductVM>.Success(data);
        }

        // Create
        public async Task<Response<bool>> CreateAsync(CreateProductVM vm)
        {

            if (vm == null)
            {
                return Response<bool>.Fail(ProductError.InvalidData);
            }


            string? imagePath = null;

            if (vm.Image != null)
            {
                imagePath = Upload.UploadFile("Files", vm.Image);
            }

            var product = _mapper.Map<Product>(vm);

            product.Image = imagePath;

            await _productRepo.CreateAsync(product);

            return Response<bool>.Success(true);


        }

        // Update
        public async Task<Response<bool>> UpdateAsync(UpdateProductVM vm)
        {
            var product = await _productRepo.GetByIdAsync(vm.Id);

            if (product == null)
            {
                return Response<bool>.Fail(ProductError.InvalidData);
            }
            string? imagePath = null;

            if (vm.Image != null)
            {
                imagePath = Upload.UploadFile("Files", vm.Image);
            }

            _mapper.Map(vm, product);

            product.Image = imagePath;
            await _productRepo.UpdateAsync(product);

            return Response<bool>.Success(true);
        }

        // Delete (Soft Delete)
        public async Task<Response<bool>> DeleteAsync(int id)
        {
            var product = await _productRepo.GetByIdAsync(id);

            if (product == null || product.IsDeleted)
            {
                return Response<bool>.Fail(ProductError.NotFound);
            }

            await _productRepo.DeleteAsync(id);

            return Response<bool>.Success(true);
        }

        public async Task<Response<IEnumerable<DetailsProductVM>>> SearchByNameAsync(string name)
        {
            var Query = _productRepo.SearchByName(name);
            var products = await Query.ToListAsync();

            if (products == null || !products.Any())
            {
                return Response<IEnumerable<DetailsProductVM>>.Fail(ProductError.NotFound);
            }

            var data = _mapper.Map<IEnumerable<DetailsProductVM>>(products);

            return Response<IEnumerable<DetailsProductVM>>.Success(data);
        }

        public async Task<Response<IEnumerable<DetailsProductVM>>> GetInStockAsync()
        {
            var Query = _productRepo.GetInStock();
            var products = await Query.ToListAsync();

            if (products == null || !products.Any())
            {
                return Response<IEnumerable<DetailsProductVM>>.Fail(ProductError.NotFound);
            }

            var data = _mapper.Map<IEnumerable<DetailsProductVM>>(products);

            return Response<IEnumerable<DetailsProductVM>>.Success(data);
        }

        public async Task<Response<IEnumerable<DetailsProductVM>>> GetOutStockAsync()
        {
            var Query = _productRepo.GetOutStock();
            var products = await Query.ToListAsync();

            if (products == null || !products.Any())
            {
                return Response<IEnumerable<DetailsProductVM>>.Fail(ProductError.NotFound);
            }

            var data = _mapper.Map<IEnumerable<DetailsProductVM>>(products);

            return Response<IEnumerable<DetailsProductVM>>.Success(data);
        }

        public async Task<Response<IEnumerable<DetailsProductVM>>> GetMostExpensiveAsync()
        {
            var Query = _productRepo.GetMostExpensive();
            var products = await Query.ToListAsync();

            if (products == null || !products.Any())
            {
                return Response<IEnumerable<DetailsProductVM>>.Fail(ProductError.NotFound);
            }

            var data = _mapper.Map<IEnumerable<DetailsProductVM>>(products);

            return Response<IEnumerable<DetailsProductVM>>.Success(data);
        }


        public async Task<Response<IEnumerable<DetailsProductVM>>> GetLestExpensiveAsync()
        {
            var Query = _productRepo.GetLestExpensive();
            var products = await Query.ToListAsync();

            if (products == null || !products.Any())
            {
                return Response<IEnumerable<DetailsProductVM>>.Fail(ProductError.NotFound);
            }

            var data = _mapper.Map<IEnumerable<DetailsProductVM>>(products);

            return Response<IEnumerable<DetailsProductVM>>.Success(data);
        }


        public async Task<Response<IEnumerable<DetailsProductVM>>> GetAllByCategoryIdAsync(int categoryId)
        {
            var Query = _productRepo.GetAllByCategoryId(categoryId);
            var products = await Query.ToListAsync();

            if (products == null || !products.Any())
            {
                return Response<IEnumerable<DetailsProductVM>>.Fail(ProductError.NotFound);
            }

            var data = _mapper.Map<IEnumerable<DetailsProductVM>>(products);

            return Response<IEnumerable<DetailsProductVM>>.Success(data);
        }

        public async Task<Response<bool>> ExistsByNameAsync(string name)
        {
            var exists = await _productRepo.ExistsByNameAsync(name);

            return Response<bool>.Success(exists);
        }

        public async Task<Response<bool>> ExistsByIdAsync(int id)
        {
            var exists = await _productRepo.ExistsByIdAsync(id);

            return Response<bool>.Success(exists);
        }


    }
}