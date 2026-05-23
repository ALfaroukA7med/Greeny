using Greeny.BLL.Abstraction;
using Greeny.BLL.Errors;
using Greeny.BLL.Helper;
using Greeny.BLL.ModelVM.Category;
using Greeny.BLL.Services.Interfaces;

namespace Greeny.BLL.Services.Implementation
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

        public async Task<Response<bool>> CreateAsync(CreateCategoryVM vm)
        {
            if (vm == null)
            {
                return Response<bool>.Fail(CategoryError.InvalidData);
            }
            string? IconPath = null;

            if (vm.Icon != null)
            {
                IconPath = Upload.UploadFile("Files", vm.Icon);
            }

            var category = _mapper.Map<Category>(vm);
            category.Icon = IconPath;

            await _categoryRepo.CreateAsync(category);

            return Response<bool>.Success(true);
        }

        public async Task<Response<bool>> DeleteAsync(int id)
        {
            var Query = _categoryRepo.GetById(id);
            var category = await Query.FirstOrDefaultAsync();

            if (category == null || category.IsDeleted)
            {
                return Response<bool>.Fail(CategoryError.NotFound);
            }

            await _categoryRepo.DeleteAsync(id);

            return Response<bool>.Success(true);
        }

        public async Task<Response<bool>> ExistsByIdAsync(int id)
        {
            var exists = await _categoryRepo.ExistsByIdAsync(id);

            return Response<bool>.Success(exists);
        }

        public async Task<Response<IEnumerable<DetailsCategoryVM>>> GetAllAsync()
        {
            var categories = await _categoryRepo
                .GetAll()
                .ToListAsync();

            if (!categories.Any())
            {
                return Response<IEnumerable<DetailsCategoryVM>>
                    .Fail(CategoryError.NotFound);
            }

            var data = _mapper.Map<List<DetailsCategoryVM>>(categories);

            foreach (var item in data)
            {
                item.NumberOfProducts =
                    await _categoryRepo.GetProductsCountByCategoryId(item.Id);
                item.Icon = await _categoryRepo.GetCategoryIconById(item.Id);
            }

            return Response<IEnumerable<DetailsCategoryVM>>
                .Success(data);
        }

        public async Task<Response<DetailsCategoryVM>> GetByIdAsync(int id)
        {
            var Query = _categoryRepo.GetById(id);
            var category = await Query.FirstOrDefaultAsync();
            if (category == null)
            {
                return Response<DetailsCategoryVM>.Fail(CategoryError.NotFound);
            }
            var data = _mapper.Map<DetailsCategoryVM>(category);
            return Response<DetailsCategoryVM>.Success(data);
        }

        public async Task<Response<IEnumerable<DetailsCategoryVM>>> SearchByNameAsync(string name)
        {
            var Query = _categoryRepo.SearchByName(name);
            var categories = await Query.ToListAsync();

            if (categories == null || !categories.Any())
            {
                return Response<IEnumerable<DetailsCategoryVM>>.Fail(CategoryError.NotFound);
            }

            var data = _mapper.Map<IEnumerable<DetailsCategoryVM>>(categories);

            return Response<IEnumerable<DetailsCategoryVM>>.Success(data);
        }

        public async Task<Response<bool>> UpdateAsync(UpdateCategoryVM vm)
        {

            var Query = _categoryRepo.GetById(vm.Id);
            var category = await Query.FirstOrDefaultAsync();

            if (category == null || category.IsDeleted)
            {
                return Response<bool>.Fail(CategoryError.NotFound);
            }
            string? IconPath = null;

            if (vm.Icon != null)
            {
                IconPath = Upload.UploadFile("Files", vm.Icon);
            }

            _mapper.Map(vm, category);

            category.Icon = IconPath;

            await _categoryRepo.UpdateAsync(category);

            return Response<bool>.Success(true);
        }

    }
}
