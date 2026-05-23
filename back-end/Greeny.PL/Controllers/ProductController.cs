using Greeny.BLL.ModelVM.Category;
using Greeny.BLL.ModelVM.ProductVM;
using Greeny.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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


        public async Task<IActionResult> Index()
        {

            var result = await _productService.GetAllAsync(null, null, null);

            var empty = new MarketPlaceVM
            {
                Categories = new List<string>(),
                Products = new List<ProductListVM>()
            };

            if (!result.IsSuccess)
                return View("Index", empty);

            return View("Index", result.Data);
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
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _productService.GetByIdAsync(id);

            if (result.Data == null)
                return NotFound();


            return View(result.Data);
        }

        // CREATE (GET) 
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var vm = new CreateProductVM
            {
                Categories = await GetCategoriesAsync()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductVM vm)
        {

            vm.Categories = await GetCategoriesAsync();

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return View(vm);
            }

            var result = await _productService.CreateAsync(vm);

            if (!result.IsSuccess)
            {

                return View(vm);
            }

            TempData["Success"] = "Product created successfully";

            return RedirectToAction(nameof(Index));
        }

        private async Task<IEnumerable<SelectListItem>>
            GetCategoriesAsync()
        {
            return (await _categoryService.GetAllAsync()).Data
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                });
        }


        // EDIT (GET)
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _productService.GetByIdAsync(id);

            if (!result.IsSuccess || result.Data == null)
                return NotFound();

            var vm = new UpdateProductVM
            {
                Id = result.Data.Id,
                Name = result.Data.Name,
                Description = result.Data.Description,
                ImageUrl = result.Data.Image,
                Quantity = result.Data.Quantity,
                Price = result.Data.Price,
                CategoryId = result.Data.CategoryId,
                Categories = await GetCategoriesAsync()
            };

            return View(vm);
        }

        // EDIT 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateProductVM vm)
        {
            vm.Categories = await GetCategoriesAsync();

            if (!ModelState.IsValid)
                return View(vm);

            var result = await _productService.UpdateAsync(vm);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", "Failed to update product");

                return View(vm);
            }

            TempData["Success"] = "Product updated successfully";

            return RedirectToAction(nameof(Index));
        }

        // DELETE
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.DeleteAsync(id);

            if (!result.IsSuccess)
                return NotFound();

            TempData["Success"] = "Product deleted successfully";

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateQuantity([FromBody] UpdateProductVM incomingVm)
        {
            if (incomingVm == null || incomingVm.Id <= 0 || incomingVm.Quantity < 0)
            {
                return BadRequest(new { message = "بيانات غير صالحة، يرجى إدخال كمية صحيحة." });
            }

            var result = await _productService.GetByIdAsync(incomingVm.Id);

            if (!result.IsSuccess || result.Data == null)
            {
                return NotFound(new { message = "هذا المنتج غير موجود." });
            }

            incomingVm.Name = result.Data.Name;
            incomingVm.Description = result.Data.Description;
            incomingVm.ImageUrl = result.Data.Image;
            incomingVm.Price = result.Data.Price;
            incomingVm.CategoryId = result.Data.CategoryId;

            ModelState.Clear();

            var updateResult = await _productService.UpdateAsync(incomingVm);

            if (!updateResult.IsSuccess)
            {
                return BadRequest(new { message = "فشل تحديث الكمية في قاعدة البيانات." });
            }

            return Ok(new { success = true, message = "تم تحديث الكمية بنجاح." });
        }

        private async Task LoadCategoriesAsync()
        {
            var result = await _categoryService.GetAllAsync();

            ViewBag.Categories = result.Data
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
                .ToList();
        }

    }

}