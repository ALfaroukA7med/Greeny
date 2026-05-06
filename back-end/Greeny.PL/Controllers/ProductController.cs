using Greeny.BLL.Admin.ModelVM.Category;
using Greeny.BLL.Admin.ModelVM.ProductVM;
using Greeny.BLL.Admin.Services.Interfaces;
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
        public async Task<IActionResult> Index()
        {
            var result = await _productService.GetAllAsync();

            if (!result.IsSuccess)
                return View("Index", Enumerable.Empty<DetailsProductVM>());

            return View("Index", result.Data);
        }

        // GET: Product Details
        public async Task<IActionResult> Details(int id)
        {
            var result = await _productService.GetByIdAsync(id);

            if (!result.IsSuccess)
                return NotFound();

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

        // SEARCH by name
        public async Task<IActionResult> Search(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return RedirectToAction(nameof(Index));
            var result = await _productService.SearchByNameAsync(name);
            if (!result.IsSuccess)
                return View("Index", Enumerable.Empty<DetailsProductVM>());

            return View("Index", result.Data);
        }

        //Get by category id
        public async Task<IActionResult> GetByCategory(int id)
        {
            var result = await _productService.GetAllByCategoryIdAsync(id);
            if (!result.IsSuccess)
                return View("Index", Enumerable.Empty<DetailsProductVM>());

            return View("Index", result.Data);
        }


        //Get All in stock
        public async Task<IActionResult> GetAllInStock()
        {
            var result = await _productService.GetInStockAsync();

            if (!result.IsSuccess)
                return View("Index", Enumerable.Empty<DetailsProductVM>());

            return View("Index", result.Data);
        }


        //Get All out stock
        public async Task<IActionResult> GetAllOutStock()
        {
            var result = await _productService.GetOutStockAsync();

            if (!result.IsSuccess)
                return View("Index", Enumerable.Empty<DetailsProductVM>());

            return View("Index", result.Data);
        }


        //Get Lest Expensive
        public async Task<IActionResult> GetLestExpensive()
        {
            var result = await _productService.GetLestExpensiveAsync();
            if (!result.IsSuccess)
                return View("Index", Enumerable.Empty<DetailsProductVM>());

            return View("Index", result.Data);
        }



        //Get Most Expensive
        public async Task<IActionResult> GetMostExpensive()
        {
            var result = await _productService.GetMostExpensiveAsync();
            if (!result.IsSuccess)
                return View("Index", Enumerable.Empty<DetailsProductVM>());

            return View("Index", result.Data);
        }



    }
}