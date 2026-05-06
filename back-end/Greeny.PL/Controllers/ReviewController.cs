using Microsoft.AspNetCore.Mvc;
using Greeny.BLL.Admin.Services.Interfaces;
using Greeny.BLL.Admin.ModelVM.Review;

namespace Greeny.PL.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        // GET: Reviews by Product
        public async Task<IActionResult> Index(int productId)
        {
            var result = await _reviewService.GetAllByProductIdAsync(productId);

            if (!result.IsSuccess)
                return View(Enumerable.Empty<DetailsReviewVM>());

            return View(result.Data);
        }

        // DETAILS
        public async Task<IActionResult> Details(int id)
        {
            var result = await _reviewService.GetByIdAsync(id);

            if (!result.IsSuccess)
                return NotFound();

            return View(result.Data);
        }

        // CREATE (GET)
        public IActionResult Create(int productId)
        {
            var vm = new CreateReviewVM
            {
                ProductId = productId
            };

            return View(vm);
        }

        // CREATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateReviewVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var result = await _reviewService.CreateAsync(vm);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", "Failed to add review");
                return View(vm);
            }

            return RedirectToAction("Index", new { productId = vm.ProductId });
        }

        // EDIT (GET)
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _reviewService.GetByIdAsync(id);

            if (!result.IsSuccess)
                return NotFound();

            var vm = new UpdateReviewVM
            {
                Id = result.Data.Id,
                Stars = result.Data.Stars,
                Content = result.Data.Content,
                Date = DateTime.Now,
                ProductId = result.Data.ProductId
            };
            return View(vm);
        }

        // EDIT (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateReviewVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var result = await _reviewService.UpdateAsync(vm);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", "Failed to update review");
                return View(vm);
            }

            return RedirectToAction("Index", new { productId = vm.ProductId });
        }

        // DELETE
        [HttpDelete]
        public async Task<IActionResult> Delete(int id, int productId)
        {
            var result = await _reviewService.DeleteAsync(id);

            if (!result.IsSuccess)
                return NotFound();

            return RedirectToAction("Index", new { productId });
        }

        public async Task<IActionResult> CountReviewByProductId(int productId)
        {
            var result = await _reviewService.CountByProductIdAsync(productId);
            return Content(result.Data.ToString());
        }


    }
}