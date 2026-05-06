using Greeny.BLL.Admin.ModelVM.CartItem;
using Greeny.BLL.Admin.Services.Interfaces;
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

        public async Task<IActionResult> Index(int cartId)
        {
            var result = await _cartItemService.GetByCartId(cartId);
            if (!result.IsSuccess)
                return View(Enumerable.Empty<DetailsCartItemVM>());

            return View(result.Data);
        }

        // GET: Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        public async Task<IActionResult> Create(CreateCartItemVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var result = await _cartItemService.CreateAsync(vm);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", "Error while creating item");
                return View(vm);
            }

            return RedirectToAction("Index");
        }



        // GET: Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _cartItemService.GetById(id);

            if (!result.IsSuccess)
                return RedirectToAction("Index");

            var vm = new UpdateCartItemVM
            {
                Id = result.Data.Id,
                Quantity = result.Data.Quantity
            };
            return View(vm);
        }

        // POST: Edit
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateCartItemVM vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var result = await _cartItemService.UpdateAsync(vm);

            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", "Update failed");
                return View(vm);
            }

            return RedirectToAction("Index");
        }


        // Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int id, int cartId)
        {
            var result= await _cartItemService.DeleteAsync(id);
            if (!result.IsSuccess)
                return NotFound();
            return RedirectToAction("Index", new { cartId });
        }

        // Increase 1 
        public async Task<IActionResult> Increase(int id, int cartId)
        {
            await _cartItemService.IncreaseQuantityAsync(id);
            return RedirectToAction("Index", new { cartId });
        }

        // decrease 1
        public async Task<IActionResult> Decrease(int id, int cartId)
        {
            await _cartItemService.DecreaseQuantityAsync(id);
            return RedirectToAction("Index", new { cartId });
        }



    }
}