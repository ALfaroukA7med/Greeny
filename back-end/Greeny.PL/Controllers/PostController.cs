using Greeny.BLL.Errors;
using Greeny.BLL.Helper;
using Greeny.BLL.ModelVM.Post;
using Greeny.BLL.ModelVM.Wrapper;
using Greeny.BLL.Services.Implementation;
using Greeny.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
                Posts = posts?.Value.ToList(),
                CreatePost = new PostCreateVM()
            };
            return View("Feed", commvm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(Prefix = "CreatePost")] PostCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please fill out the content before posting.";
                return RedirectToAction("Feed");
            }

            vm.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _postService.AddAsync(vm);

            if (!result.IsSuccess)
            {
                TempData["ErrorMessage"] = "Failed to create post.";
                return RedirectToAction("Feed");
            }

            return RedirectToAction("Feed");
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var post = await _postService.GetByIdAsync(id);
            // i'm already getting all post comments in post.

            return View("Details", post);
        }


        // Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(currentUserId))
            {
                return Challenge(); // Forces the browser to redirect to the Login page
            }

            var result = await _postService.DeleteAsync(currentUserId, id);
            if (!result.IsSuccess)
            {
                if (result.Error == UserError.Unauthorized)
                {
                    TempData["ErrorMessage"] = "You do not have permission to delete this post.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Post not found or has already been deleted.";
                }
            }
            else
            {
                TempData["SuccessMessage"] = "Post deleted successfully!";
            }

            return RedirectToAction("Feed");
        }

    }
}