using AutoMapper;
using Greeny.BLL.Admin.ModelVM.ProductVM;
using Greeny.BLL.Admin.Response;
using Greeny.BLL.Admin.Services.Interfaces;
using Greeny.DAL.Entities;
using Greeny.DAL.Repository.Interfaces;

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
        try
        {
            var products = await _productRepo.GetAllAsync();

            if (products == null || !products.Any())
            {
                return new Response<IEnumerable<DetailsProductVM>>
                {
                    IsSuccess = false,
                    Message = "No Products Found"
                };
            }

            var data = _mapper.Map<IEnumerable<DetailsProductVM>>(products);

            return new Response<IEnumerable<DetailsProductVM>>
            {
                IsSuccess = true,
                Data = data,
                Message = "Products Retrieved Successfully"
            };
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<DetailsProductVM>>
            {
                IsSuccess = false,
                Message = "Something went wrong, please try again later"
            };
        }
    }

    // Get By Id
    public async Task<Response<DetailsProductVM>> GetByIdAsync(int id)
    {
        try { 
         var product = await _productRepo.GetByIdAsync(id);
        if(product == null)
        {
            return new Response<DetailsProductVM>
            {
                IsSuccess = false,
                Message = "Not Found"
            };
        }
            var data = _mapper.Map<DetailsProductVM>(product);
            return new Response<DetailsProductVM>
            {
                IsSuccess = true,
                Data = data,
                Message = "Product Retrieved Successfully"
            };
         
        }catch(Exception ex)
        {
            return new Response<DetailsProductVM>
            {
                IsSuccess = false,
                Message = "Something went wrong, please try again later"
            };
        }
    }

    // Create
    public async Task<Response<string>> CreateAsync(CreateProductVM vm)
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

            var product = _mapper.Map<Product>(vm);

            await _productRepo.CreateAsync(product);

            return new Response<string>
            {
                IsSuccess = true,
                Message = "Product Created Successfully"
            };
        }
        catch (Exception)
        {
            return new Response<string>
            {
                IsSuccess = false,
                Message = "Something went wrong, please try again later"
            };
        }
    }

    // Update
    public async Task<Response<string>> UpdateAsync(UpdateProductVM vm)
    {
        try
        {
            var product = await _productRepo.GetByIdAsync(vm.Id);

            if (product == null)
            {
                return new Response<string>
                {
                    IsSuccess = false,
                    Message = "Product Not Found"
                };
            }

            _mapper.Map(vm, product);
            await _productRepo.UpdateAsync(product);

            return new Response<string>
            {
                IsSuccess = true,
                Message = "Product Updated Successfully"
            };
        }       
        catch (Exception)
        {
            return new Response<string>
            {
                IsSuccess = false,
                Message = "Something went wrong, please try again later"
            };
        }
    }

    // Delete (Soft Delete)
    public async Task<Response<string>> DeleteAsync(int id)
    {
        try
        {
            var product = await _productRepo.GetByIdAsync(id);

            if (product == null || product.IsDeleted)
            {
                return new Response<string>
                {
                    IsSuccess = false,
                    Message = "Product Not Found"
                };
            }

            await _productRepo.DeleteAsync(id);

            return new Response<string>
            {
                IsSuccess = true,
                Message = "Product Deleted Successfully"
            };
        }
        catch (Exception)
        {
            return new Response<string>
            {
                IsSuccess = false,
                Message = "Something went wrong, please try again later"
            };
        }
    }

    public async Task<Response<IEnumerable<DetailsProductVM>>> SearchByNameAsync(string name)
    {
        try
        {
            var products = await _productRepo.SearchByNameAsync(name);

            if (products == null || !products.Any())
            {
                return new Response<IEnumerable<DetailsProductVM>>
                {
                    IsSuccess = false,
                    Message = "No Products Found"
                };
            }

            var data = _mapper.Map<IEnumerable<DetailsProductVM>>(products);

            return new Response<IEnumerable<DetailsProductVM>>
            {
                IsSuccess = true,
                Data = data,
                Message = "Products Retrieved Successfully"
            };
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<DetailsProductVM>>
            {
                IsSuccess = false,
                Message = "Something went wrong, please try again later"
            };
        }
    }

    public async Task<Response<IEnumerable<DetailsProductVM>>> GetInStockAsync()
    {
        try
        {
            var products = await _productRepo.GetInStockAsync();

            if (products == null || !products.Any())
            {
                return new Response<IEnumerable<DetailsProductVM>>
                {
                    IsSuccess = false,
                    Message = "No Products Found"
                };
            }

            var data = _mapper.Map<IEnumerable<DetailsProductVM>>(products);

            return new Response<IEnumerable<DetailsProductVM>>
            {
                IsSuccess = true,
                Data = data,
                Message = "Products Retrieved Successfully"
            };
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<DetailsProductVM>>
            {
                IsSuccess = false,
                Message = "Something went wrong, please try again later"
            };
        }
    }

   public async Task<Response<IEnumerable<DetailsProductVM>>> GetOutStockAsync()
    {
        try
        {
            var products = await _productRepo.GetOutStockAsync();

            if (products == null || !products.Any())
            {
                return new Response<IEnumerable<DetailsProductVM>>
                {
                    IsSuccess = false,
                    Message = "No Products Found"
                };
            }

            var data = _mapper.Map<IEnumerable<DetailsProductVM>>(products);

            return new Response<IEnumerable<DetailsProductVM>>
            {
                IsSuccess = true,
                Data = data,
                Message = "Products Retrieved Successfully"
            };
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<DetailsProductVM>>
            {
                IsSuccess = false,
                Message = "Something went wrong, please try again later"
            };
        }
    }

    public async Task<Response<IEnumerable<DetailsProductVM>>> GetMostExpensiveAsync()
    {
        try
        {
            var products = await _productRepo.GetMostExpensiveAsync();

            if (products == null || !products.Any())
            {
                return new Response<IEnumerable<DetailsProductVM>>
                {
                    IsSuccess = false,
                    Message = "No Products Found"
                };
            }

            var data = _mapper.Map<IEnumerable<DetailsProductVM>>(products);

            return new Response<IEnumerable<DetailsProductVM>>
            {
                IsSuccess = true,
                Data = data,
                Message = "Products Retrieved Successfully"
            };
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<DetailsProductVM>>
            {
                IsSuccess = false,
                Message = "Something went wrong, please try again later"
            };
        }
    }


    public async Task<Response<IEnumerable<DetailsProductVM>>> GetLestExpensiveAsync()
    {
        try
        {
            var products = await _productRepo.GetLestExpensiveAsync();

            if (products == null || !products.Any())
            {
                return new Response<IEnumerable<DetailsProductVM>>
                {
                    IsSuccess = false,
                    Message = "No Products Found"
                };
            }

            var data = _mapper.Map<IEnumerable<DetailsProductVM>>(products);

            return new Response<IEnumerable<DetailsProductVM>>
            {
                IsSuccess = true,
                Data = data,
                Message = "Products Retrieved Successfully"
            };
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<DetailsProductVM>>
            {
                IsSuccess = false,
                Message = "Something went wrong, please try again later"
            };
        }
    }


    public async Task<Response<IEnumerable<DetailsProductVM>>> GetAllByCategoryIdAsync(int categoryId)
    {
        try
        {
            var products = await _productRepo.GetAllByCategoryIdAsync(categoryId);

            if (products == null || !products.Any())
            {
                return new Response<IEnumerable<DetailsProductVM>>
                {
                    IsSuccess = false,
                    Message = "No Products Found"
                };
            }

            var data = _mapper.Map<IEnumerable<DetailsProductVM>>(products);

            return new Response<IEnumerable<DetailsProductVM>>
            {
                IsSuccess = true,
                Data = data,
                Message = "Products Retrieved Successfully"
            };
        }
        catch (Exception ex)
        {
            return new Response<IEnumerable<DetailsProductVM>>
            {
                IsSuccess = false,
                Message = "Something went wrong, please try again later"
            };
        }
    }

    public async Task<Response<bool>> ExistsByNameAsync(string name)
    {
        try
        {
            var exists = await _productRepo.ExistsByNameAsync(name);

            return new Response<bool>
            {
                IsSuccess = true,
                Data = exists,
                Message = exists ? "Product Exists" : "Product Not Found"
            };
        }
        catch (Exception)
        {
            return new Response<bool>
            {
                IsSuccess = false,
                Message = "Something went wrong, please try again later"
            };
        }
    }

    public async Task<Response<bool>> ExistsByIdAsync(int id)
    {
        try
        {
            var exists = await _productRepo.ExistsByIdAsync(id);

            return new Response<bool>
            {
                IsSuccess = true,
                Data = exists,
                Message = exists ? "Product Exists" : "Product Not Found"
            };
        }
        catch (Exception)
        {
            return new Response<bool>
            {
                IsSuccess = false,
                Message = "Something went wrong, please try again later"
            };
        }
    }

}