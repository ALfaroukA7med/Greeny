
using Greeny.BLL.ModelVM.Order;
using Greeny.BLL.Services.Implementation;
using Greeny.BLL.Services.Interfaces;
using Greeny.DAL.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Greeny.PL.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Checkout(int cartId)
        {
            var result = await _service.CheckoutAsync(cartId);

            if (!result.IsSuccess)
                if (!result.IsSuccess)
                    return NotFound();

            decimal shipping;
            if (result.Data.TotalPrice == 0)
            {
                shipping = 0;
            }
            else if (result.Data.TotalPrice >= 40)
            {
                shipping = 0;
            }
            else
            {
                shipping = 4.99m;
            }
            ViewBag.Shipping = shipping;
            return View(result.Data);
        }





        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();

            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (!result.IsSuccess)
                return NotFound();

            return View(result.Data);
        }
      


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);

            if (!result.IsSuccess)
                return NotFound();

            return RedirectToAction(nameof(GetAll));
        }

        [HttpGet]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            var result = await _service.GetByUserIdAsync(userId);

            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetByStatus(Status status)
        {
            var result = await _service.GetByStatusAsync(status);

            return View(result.Data);
        }


        [HttpGet]
        public async Task<IActionResult> RecentOrders()
        {
            var result = await _service.GetRecentOrdersAsync();

            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> TotalSales()
        {
            var result = await _service.GetTotalSalesAsync();

            return View(result.Data);
        }

    }
}