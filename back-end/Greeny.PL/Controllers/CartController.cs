using Greeny.BLL.ModelVM.Cart;
using Greeny.BLL.Services.Interfaces;
using Greeny.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Greeny.PL.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _cartService.GetAllItem(userId);
            if (!result.IsSuccess || result.Data == null)
            {
                ViewBag.CartCount = 0;
                return View(new CartDetailsVM());
            }

            ViewBag.TotalItems = result.Data.Items.Sum(i => i.Quantity);
            return View(result.Data);
        }
    }
}