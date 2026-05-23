using Greeny.BLL.ModelVM.Comment;
using Greeny.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Greeny.PL.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IPostService _postService;

        public CommentController(ICommentService commentService, IPostService postService)
        {
            _commentService = commentService;
            _postService = postService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(Prefix = "NewComment")] CommentCreateVM vm)
        {
            vm.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);


            if (!ModelState.IsValid)
                return RedirectToAction("Details", "Post", new {id = vm.PostId});


            var result = await _commentService.AddAsync(vm);

            if (!result.IsSuccess)
            {
                TempData["Error"] = "Failed to add comment";
            }

            return RedirectToAction("Details", "Post", new {id = vm.PostId });
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, int postId)
        {
            var result = await _commentService.DeleteAsync(id);

            if (!result.IsSuccess)
            {
                TempData["Error"] = "Comment not found";
            }
            else
            {
                TempData["Success"] = "Comment deleted successfully";
            }

            return RedirectToAction("Details", "Post",  new { id = postId });
        }

    }
}