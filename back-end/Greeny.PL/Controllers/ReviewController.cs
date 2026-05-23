using Microsoft.AspNetCore.Mvc;
using Greeny.BLL.ModelVM.Review;
using Greeny.BLL.Services.Interfaces;
using System.Security.Claims;

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateReviewVM vm)
        {
            if (!User.Identity.IsAuthenticated)
            {
                TempData["Error"] = "You must be logged in to write a review.";
                return RedirectToAction("Details", "Product", new { id = vm.ProductId });
            }

            if (vm.ProductId == 0 || vm.Stars < 1 || vm.Stars > 5 || string.IsNullOrWhiteSpace(vm.Content))
            {
                TempData["Error"] = "Please provide a valid rating and review content.";
                return RedirectToAction("Details", "Product", new { id = vm.ProductId });
            }

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            vm.UserId = currentUserId; 
            var result = await _reviewService.CreateAsync(vm);

            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Error.Description; 
                return RedirectToAction("Details", "Product", new { id = vm.ProductId });
            }

            TempData["Success"] = "Review added successfully!";
            return RedirectToAction("Details", "Product", new { id = vm.ProductId });
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