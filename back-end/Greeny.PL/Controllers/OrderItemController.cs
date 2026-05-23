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
    }
}