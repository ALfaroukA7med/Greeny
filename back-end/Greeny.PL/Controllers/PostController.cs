using Greeny.BLL.ModelVM.Post;
using Greeny.BLL.ModelVM.Wrapper;
using Greeny.BLL.Services.Implementation;
using Greeny.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Greeny.PL.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        //private readonly IUserService userService;
        
        public PostController(IPostService postService, ICommentService commentservice)
        {
            _postService = postService;
            _commentService = commentservice;
        }
        [HttpGet]
        public async Task<IActionResult> Feed()
        {
            var posts = await _postService.GetAllAsync();
            var commvm = new CommunityVM()
            {
                Posts = posts.Value.ToList(),
                CreatePost = new PostCreateVM()
            };
            return View("Feed", commvm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostCreateVM vm)
        {
            if (!ModelState.IsValid)
                return View("Feed");

            var result = await _postService.AddAsync(vm);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", "Failed to create post");
                return View("Feed");
            }

            return RedirectToAction("Feed");
        }
        public async Task<IActionResult> Details(int id)
        {
            var post = await _postService.GetByIdAsync(id);
            var comments = await _commentService.GetAllByPostId(id);

            return View("Post");
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