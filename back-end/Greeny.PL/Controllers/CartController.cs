using Greeny.BLL.ModelVM.Cart;
using Greeny.BLL.Services.Interfaces;
using Greeny.DAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Greeny.PL.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IActionResult> Index(int cartId)
        {
            var result = await _cartService.GetAllItem(cartId);

            if (!result.IsSuccess)
            {
                return View(new CartDetailsVM());
            }
            ViewBag.CartCount = result.Data.Items.Sum(i => i.Quantity);
            ViewBag.CartId = cartId;

            return View(result.Data);
        }
    }
}