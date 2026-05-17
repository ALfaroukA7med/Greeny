using Greeny.BLL.ModelVM.User;
using Greeny.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Greeny.PL.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // ==========================
        // Get All Active Users
        // ==========================
        public async Task<IActionResult> Index()
        {
            var result = await _userService.GetAllActiveAsync();

            return View(result.Data);
        }

        // ==========================
        // Get Deleted Users
        // ==========================
        public async Task<IActionResult> DeletedUsers()
        {
            var result = await _userService.GetAllDeletedAsync();

            return View(result.Data);
        }

        // Details
        public async Task<IActionResult> Details(string id)
        {
            var result = await _userService.GetByIdAsync(id);

            if (!result.IsSuccess)
            {
                return NotFound();
            }

            return View(result.Data);
        }

        // Update (GET)
        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var result = await _userService.GetByIdAsync(id);

            if (!result.IsSuccess)
            {
                return NotFound();
            }

            var user = result.Data;

            var vm = new UpdateUserVM
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                ProfilePicture =user.ProfilePicture
            };

            return View(vm);
        }

        // Update (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateUserVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var result = await _userService.UpdateAsync(vm);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", "Failed Update");

                return View(vm);
            }

            return RedirectToAction(nameof(Details), new { id = vm.Id });
        }

        // Delete
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _userService.DeleteAsync(id);

            if (!result.IsSuccess)
            {
                return NotFound();
            }

            return RedirectToAction("Index","Home");
        }


    }
}