
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

        #region Create

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderCreateVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var result = await _service.CreateAsync(vm);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", "Failed To Create");
                return View(vm);
            }

            return RedirectToAction(nameof(GetAll));
        }

        #endregion

        #region Get All

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();

            return View(result.Data);
        }

        #endregion

        #region Get By Id

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (!result.IsSuccess)
                return NotFound();

            return View(result.Data);
        }

        #endregion

        #region Update

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (!result.IsSuccess)
                return NotFound();

            var vm = new OrderUpdateVM
            {
                Id = result.Data.Id,
                Address = result.Data.Address,
                Status = result.Data.Status,
                TotalPrice = result.Data.TotalPrice
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(OrderUpdateVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var result = await _service.UpdateAsync(vm);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", "Failed To Update");
                return View(vm);
            }

            return RedirectToAction(nameof(GetById), new { id = vm.Id });
        }

        #endregion

        #region Delete

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);

            if (!result.IsSuccess)
                return NotFound();

            return RedirectToAction(nameof(GetAll));
        }

        #endregion

        #region Get By UserId

        [HttpGet]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            var result = await _service.GetByUserIdAsync(userId);

            return View(result.Data);
        }

        #endregion

        #region Get By Status

        [HttpGet]
        public async Task<IActionResult> GetByStatus(Status status)
        {
            var result = await _service.GetByStatusAsync(status);

            return View(result.Data);
        }

        #endregion

        #region Get By UserId And Status

        [HttpGet]
        public async Task<IActionResult> GetByUserIdAndStatus(string userId, Status status)
        {
            var result = await _service.GetByUserIdAndStatusAsync(userId, status);

            return View(result.Data);
        }

        #endregion

        #region Recent Orders

        [HttpGet]
        public async Task<IActionResult> RecentOrders()
        {
            var result = await _service.GetRecentOrdersAsync();

            return View(result.Data);
        }

        #endregion

        #region Total Sales

        [HttpGet]
        public async Task<IActionResult> TotalSales()
        {
            var result = await _service.GetTotalSalesAsync();

            return View(result.Data);
        }

        #endregion
    }
}