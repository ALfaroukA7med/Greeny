using Greeny.BLL.Admin.ModelVM.CartItem;
using Greeny.BLL.Admin.Services.Interfaces;
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
                return View(Enumerable.Empty<DetailsCartItemVM>());

            return View(result.Data);
        }
    }
}