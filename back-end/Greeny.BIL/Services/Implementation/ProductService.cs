using Greeny.BLL.Abstraction;
using Greeny.BLL.Errors;
using Greeny.BLL.ModelVM.ProductVM;
using Greeny.BLL.Services.Interfaces;

namespace Greeny.BLL.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;
        private readonly IMapper _mapper;

        public ProductService(IProductRepo productRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }




        // Get All
        public async Task<Response<IEnumerable<DetailsProductVM>>> GetAllAsync()
        {
            var query =  _productRepo.GetAll();
            var products = await query.ToListAsync();
            if (!products.Any())
            {
                return Response<IEnumerable<DetailsProductVM>>.Fail(ProductError.NotFound);
            }

            var data = _mapper.Map<IEnumerable<DetailsProductVM>>(products);

            return Response<IEnumerable<DetailsProductVM>>.Success(data);
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
                return Response<DetailsProductVM>.Success(data);
        }

        // Create
        public async Task<Response<bool>> CreateAsync(CreateProductVM vm)
        {
                if (vm == null)
                {
                    return Response<bool>.Fail(ProductError.InvalidData);
                }

                var product = _mapper.Map<Product>(vm);

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

                _mapper.Map(vm, product);
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