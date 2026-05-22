using Greeny.BLL.ModelVM.Comment;
using Greeny.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Greeny.PL.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CommentCreateVM vm)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index", new { postId = vm.PostId });

            var result = await _commentService.AddAsync(vm);

            if (!result.IsSuccess)
            {
                TempData["Error"] = "Failed to add comment";
            }

            return RedirectToAction("Index", new { postId = vm.PostId });
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

            return RedirectToAction("Index", new { postId });
        }

    }
}