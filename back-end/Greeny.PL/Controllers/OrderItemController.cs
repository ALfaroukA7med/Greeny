using Greeny.BLL.ModelVM.OrderItem;
using Greeny.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Greeny.PL.Controllers
{
    public class OrderItemController : Controller
    {
        private readonly IOrderItemService _service;

        public OrderItemController(IOrderItemService service)
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
        public async Task<IActionResult> Create(OrderItemCreateVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var result = await _service.CreateAsync(vm);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("","Failed To Create");
                return View(vm);
            }

            return RedirectToAction(nameof(GetByOrderId), new { orderId = vm.OrderId });
        }

        #endregion

        #region Update

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (!result.IsSuccess)
                return NotFound();

            var vm = new OrderItemUpdateVM
            {
                Id = result.Data.Id,
                Quantity = result.Data.Quantity,
                UnitPrice = result.Data.UnitPrice
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(OrderItemUpdateVM vm)
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
        public async Task<IActionResult> Delete(int orderId, int productId)
        {
            var result = await _service.RemoveProductFromOrderAsync(orderId, productId);

            if (!result.IsSuccess)
                return NotFound();

            return RedirectToAction(nameof(GetByOrderId), new { orderId });
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

        #region Get By OrderId

        [HttpGet]
        public async Task<IActionResult> GetByOrderId(int orderId)
        {
            var result = await _service.GetByOrderIdAsync(orderId);

            if (!result.IsSuccess)
                return NotFound();

            return View(result.Data);
        }

        #endregion

        #region Total Price

        [HttpGet]
        public async Task<IActionResult> TotalPrice(int orderId)
        {
            var result = await _service.GetTotalPriceByOrderIdAsync(orderId);

            return View(result.Data);
        }

        #endregion

        #region Items Count

        [HttpGet]
        public async Task<IActionResult> ItemsCount(int orderId)
        {
            var result = await _service.GetItemsCountByOrderIdAsync(orderId);

            return View(result.Data);
        }

        #endregion
    }
}