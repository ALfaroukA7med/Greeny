
namespace Greeny.BLL.Admin.Services.Implementation
{
    public class CategoryService : ICategoryService
    {

        private readonly ICategoryRepo _categoryRepo;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepo categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        public async Task<Response<string>> CreateAsync(CreateCategoryVM vm)
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

                var category = _mapper.Map<Category>(vm);

                await _categoryRepo.CreateAsync(category);

                return new Response<string>
                {
                    IsSuccess = true,
                    Message = "Category Created Successfully"
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
                var category = await _categoryRepo.GetByIdAsync(id);

                if (category == null || category.IsDeleted)
                {
                    return new Response<string>
                    {
                        IsSuccess = false,
                        Message = "Category Not Found"
                    };
                }

                await _categoryRepo.DeleteAsync(id);

                return new Response<string>
                {
                    IsSuccess = true,
                    Message = "Category Deleted Successfully"
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

        public async Task<Response<bool>> ExistsByIdAsync(int id)
        {
            try
            {
                var exists = await _categoryRepo.ExistsByIdAsync(id);

                return new Response<bool>
                {
                    IsSuccess = true,
                    Data = exists,
                    Message = exists ? "Category Exists" : "Category Not Exists"
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

        public async Task<Response<IEnumerable<DetailsCategoryVM>>> GetAllAsync()
        {
            try
            {
                var categories = await _categoryRepo.GetAllAsync();

                if (categories == null || !categories.Any())
                {
                    return new Response<IEnumerable<DetailsCategoryVM>>
                    {
                        IsSuccess = false,
                        Message = "No categories Found"
                    };
                }

                var data = _mapper.Map<IEnumerable<DetailsCategoryVM>>(categories);

                return new Response<IEnumerable<DetailsCategoryVM>>
                {
                    IsSuccess = true,
                    Data = data,
                    Message = "categories Retrieved Successfully"
                };
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<DetailsCategoryVM>>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response<DetailsCategoryVM>> GetByIdAsync(int id)
        {
            try
            {
                var category = await _categoryRepo.GetByIdAsync(id);
                if (category == null)
                {
                    return new Response<DetailsCategoryVM>
                    {
                        IsSuccess = false,
                        Message = "category Not Found"
                    };
                }
                var data = _mapper.Map<DetailsCategoryVM>(category);
                return new Response<DetailsCategoryVM>
                {
                    IsSuccess = true,
                    Data = data,
                    Message = "category Retrieved Successfully"
                };

            }
            catch (Exception ex)
            {
                return new Response<DetailsCategoryVM>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response<IEnumerable<DetailsCategoryVM>>> SearchByNameAsync(string name)
        {
            try
            {
                var categories = await _categoryRepo.SearchByNameAsync(name);

                if (categories == null || !categories.Any())
                {
                    return new Response<IEnumerable<DetailsCategoryVM>>
                    {
                        IsSuccess = false,
                        Message = "No categories Found"
                    };
                }

                var data = _mapper.Map<IEnumerable<DetailsCategoryVM>>(categories);

                return new Response<IEnumerable<DetailsCategoryVM>>
                {
                    IsSuccess = true,
                    Data = data,
                    Message = "categories Retrieved Successfully"
                };
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<DetailsCategoryVM>>
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Response<string>> UpdateAsync(UpdateCategoryVM vm)
        {
            try
            {
                var category = await _categoryRepo.GetByIdAsync(vm.Id);

                if (category == null)
                {
                    return new Response<string>
                    {
                        IsSuccess = false,
                        Message = "category Not Found"
                    };
                }

                _mapper.Map(vm, category);
                await _categoryRepo.UpdateAsync(category);

                return new Response<string>
                {
                    IsSuccess = true,
                    Message = "category Updated Successfully"
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
