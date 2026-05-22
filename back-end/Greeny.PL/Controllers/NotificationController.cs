using Greeny.BLL.ModelVM.Notification;
using Greeny.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace Greeny.PL.Controllers
{
    public class NotificationController : Controller
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task<IActionResult> Index(string userId)
        {
            var result = await _notificationService.GetUserNotificationsAsync(userId);

            if (!result.IsSuccess)
                return View(Enumerable.Empty<DetailsNotificationVM>());

            return View(result.Data);
        }

        public async Task<IActionResult>NotiNav(string userId)
        {
            var result = await _notificationService.GetUserNotificationsAsync(userId);
            if (!result.IsSuccess)
                return NotFound();
            return View("_Layout", result.Data);
        }
        // POST: Create Notification
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateNotificationVM vm)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index", new { userId = vm.ReceiverId });

            var result = await _notificationService.CreateAsync(vm);

            if (!result.IsSuccess)
            {
                TempData["Error"] = "Failed to send notification";
                return RedirectToAction("Index", new { userId = vm.ReceiverId });
            }

            TempData["Success"] = "Notification sent successfully";
            return RedirectToAction("Index", new { userId = vm.ReceiverId });
        }


        // Mark as Read
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAsRead(int id, string userId)
        {
            var result = await _notificationService.MarkAsReadAsync(id);

            if (!result.IsSuccess)
            {
                TempData["Error"] = "Failed to update notification";
            }

            return RedirectToAction("Index", new { userId });
        }
        public async Task<IActionResult> Read(int id, string? url)
        {
            var result = await _notificationService.MarkAsReadAsync(id);

            return Redirect(url ?? "/");
        }
    }
}