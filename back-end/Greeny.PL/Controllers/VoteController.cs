using Greeny.BLL.Services.Implementation;
using Greeny.BLL.Services.Interfaces;
using Greeny.DAL.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Greeny.PL.Controllers
{
    [Authorize] // Prevents anonymous users from hitting these actions
    public class VoteController : Controller
    {
        private readonly IPostRepo _postrepo;
        private readonly IVoteService _voteservice; // Used to quickly grab the updated score count

        public VoteController(IVoteService voteservice, IPostRepo postrepo)
        {
            _voteservice = voteservice;
            _postrepo = postrepo;
        }

        [HttpPost("upvote/{id}")]
        public async Task<IActionResult> UpVote(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var result = await _voteservice.UpVote(userId, id);
            if (!result.IsSuccess) return BadRequest(result.Error);

            // Fetch the newly updated vote total to send back to the UI
            var post = _postrepo.GetByIdAsync(id).FirstOrDefault();



            return RedirectToAction("Feed", "Post");
        }

        [HttpPost("downvote/{id}")]
        public async Task<IActionResult> DownVote(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var result = await _voteservice.DownVote(userId, id);
            if (!result.IsSuccess) return BadRequest(result.Error);

            var post = _postrepo.GetByIdAsync(id).FirstOrDefault();
            return RedirectToAction("Feed", "Post");
        }
    }
}
