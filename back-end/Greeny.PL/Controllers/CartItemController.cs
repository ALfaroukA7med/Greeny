using Greeny.BLL.ModelVM.CartItem;
using Greeny.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Greeny.PL.Controllers
{
    public class CartItemController : Controller
    {
        private readonly ICartItemService _cartItemService;

        public CartItemController(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateCartItemVM vm)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index", "Product");

            var result = await _cartItemService.CreateAsync(vm);

            return RedirectToAction("Index", "Cart");
        }


        // Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int id, int cartId)
        {
            var result= await _cartItemService.DeleteAsync(id, cartId);
            if (!result.IsSuccess)
            {
                return NotFound();
            }
            return RedirectToAction("Index", "Cart", new { cartId });
        }

        // Increase 1 
        public async Task<IActionResult> Increase(int id, int cartId)
        {
            await _cartItemService.IncreaseQuantityAsync(id, cartId);
            return RedirectToAction("Index", "Cart", new { cartId });
        }

        // decrease 1
        public async Task<IActionResult> Decrease(int id, int cartId)
        {
            await _cartItemService.DecreaseQuantityAsync(id, cartId);
            return RedirectToAction("Index", "Cart", new { cartId });
        }



    }
}