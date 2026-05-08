using Greeny.BLL.ModelVM.Post;
using Greeny.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Greeny.PL.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _postService.GetAllAsync();

            if (!result.IsSuccess)
                return View(Enumerable.Empty<PostListVM>());

            return View(result.Data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var result = await _postService.GetByIdAsync(id);

            if (!result.IsSuccess || result.Data == null)
                return NotFound();

            return View(result.Data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostListVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var result = await _postService.AddAsync(vm);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", "Failed to create post");
                return View(vm);
            }

            return RedirectToAction("Index");
        }

        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _postService.DeleteAsync(id);

            if (!result.IsSuccess)
            {
                TempData["Error"] = "Post not found";
            }
            else
            {
                TempData["Success"] = "Post deleted successfully";
            }

            return RedirectToAction("Index");
        }

    }
}