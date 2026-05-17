using Greeny.BLL.ModelVM.Category;
using Greeny.BLL.ModelVM.ProductVM;
using Greeny.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Greeny.PL.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        // GET: All Products
        public async Task<IActionResult> MarketPlace()
        {
            var result = await _productService.GetAllAsync(null, null, null);

            var empty = new MarketPlaceVM { Categories = new List<string>() ,
            Products = new List<ProductListVM>()};

            if (!result.IsSuccess)
                return View("MarketPlace", empty);

            return View("MarketPlace", result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Search(string? searchTerm,string? categoryName,string? sortOrder)
        {
            var result = await _productService.GetAllAsync(searchTerm,categoryName,sortOrder);

            if (result.Data == null)
            {
                return PartialView(
                    "_ProductsPartial",
                    new List<ProductListVM>());
            }
            return PartialView(
                "_ProductsPartial",
                result.Data.Products);
        }

        // GET: Product Details
        public async Task<IActionResult> Details(int id)
        {
            var result = await _productService.GetByIdAsync(id);

            //if (!result.IsSuccess)
            //    return NotFound();

            return View(result.Data);
        }

        // CREATE (GET) 
        public async Task<IActionResult> Create()
        {
            var result = await _categoryService.GetAllAsync();

            ViewBag.Categories = result.IsSuccess
                ? result.Data
                : new List<DetailsCategoryVM>();

            return View(new CreateProductVM());
        }

        // CREATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var result = await _productService.CreateAsync(vm);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", "Failed to create product");
                return View(vm);
            }

            return RedirectToAction(nameof(Index));
        }

        // EDIT (GET)
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _productService.GetByIdAsync(id);

            if (!result.IsSuccess)
                return NotFound();
            var vm = new UpdateProductVM
            {
                Id =result.Data.Id,
                Name = result.Data.Name,
                Description = result.Data.Description,
                Image = result.Data.Image,
                Quantity = result.Data.Quantity,
                Price = result.Data.Price,
            };

            return View(vm);
        }

        // EDIT 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateProductVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var result = await _productService.UpdateAsync(vm);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", "Failed to update product");
                return View(vm);
            }

            return RedirectToAction(nameof(Index));
        }

        // DELETE
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.DeleteAsync(id);

            if (!result.IsSuccess)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

    }
}