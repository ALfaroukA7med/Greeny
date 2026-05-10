using Greeny.BLL.ModelVM.Payment;
using Greeny.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Greeny.PL.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _service;

        public PaymentController(IPaymentService service)
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
        public async Task<IActionResult> Create(PaymentCreateVM vm)
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

        #region Update

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _service.GetByIdAsync(id);

            if (!result.IsSuccess)
                return NotFound();

            var payment = result.Data;

            var vm = new PaymentUpdateVM
            {
                Id = payment.Id,
                TransactionRef = payment.TransactionRef,
                Method = payment.Method,
                Amount = payment.Amount,
                Status = payment.Status
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(PaymentUpdateVM vm)
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

        #region Get All

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();

            return View(result.Data);
        }

        #endregion

        #region Get By User Id

        [HttpGet]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            var result = await _service.GetByUserIdAsync(userId);

            return View(result.Data);
        }

        #endregion

        #region Get By Status

        [HttpGet]
        public async Task<IActionResult> GetByStatus(string status)
        {
            var result = await _service.GetByStatusAsync(status);

            return View(result.Data);
        }

        #endregion
    }
}