using Microsoft.AspNetCore.Mvc;
using Greeny.BLL.ModelVM.Category;
using Greeny.BLL.Services.Interfaces;

namespace Greeny.PL.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: All Categories
        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.GetAllAsync();

            if (!result.IsSuccess)
                return View(Enumerable.Empty<DetailsCategoryVM>());

            return View(result.Data);
        }

        // DETAILS by id
        public async Task<IActionResult> Details(int id)
        {
            var result = await _categoryService.GetByIdAsync(id);

            if (!result.IsSuccess)
                return NotFound();

            return View(result.Data);
        }

        // CREATE (GET)
        public IActionResult Create()
        {
            return View();
        }

        // CREATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCategoryVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var result = await _categoryService.CreateAsync(vm);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", "Failed to create category");
                return View(vm);
            }

            return RedirectToAction(nameof(Index));
        }

        // EDIT (GET)

        public async Task<IActionResult> Edit(int id)
        {
            var result = await _categoryService.GetByIdAsync(id);

            if (!result.IsSuccess)
                return NotFound();

            var vm = new UpdateCategoryVM
            {
                Id = result.Data.Id,
                Name = result.Data.Name,
                Description = result.Data.Description,
                Icon = result.Data.Icon,
            };

            return View(vm);
        }

        // EDIT (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateCategoryVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var result = await _categoryService.UpdateAsync(vm);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", "Failed to update category");
                return View(vm);
            }

            return RedirectToAction(nameof(Index));
        }

        // DELETE
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryService.DeleteAsync(id);

            if (!result.IsSuccess)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        // SEARCH
        public async Task<IActionResult> Search(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return RedirectToAction(nameof(Index));
            var result = await _categoryService.SearchByNameAsync(name);

            if (!result.IsSuccess)
                return View("Index", Enumerable.Empty<DetailsCategoryVM>());

            return View("Index", result.Data);
        }
    }
}