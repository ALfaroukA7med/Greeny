
using Greeny.BLL.Abstraction;
using Greeny.BLL.Helper;
using Greeny.BLL.ModelVM.Payment;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;


namespace Greeny.BLL.Services.Implementation
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepo _repo;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public PaymentService(IPaymentRepo repo , IHttpContextAccessor httpContextAccessor)
        {
            _repo = repo;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<bool>> CreateAsync(PaymentCreateVM vm)
        {
            if (vm == null)
            {
                return Response<bool>.Fail(PaymentError.NotFound);
            }

            var userId = _httpContextAccessor.HttpContext.User
             .FindFirst(ClaimTypes.NameIdentifier)?.Value;

            string? imagePath = null;
            if (vm.ImageFile != null)
            {
                imagePath = Upload.UploadFile("Files", vm.ImageFile);
            }
            var entity = new Payment
            {
                UserId = userId,
                OrderId = vm.OrderId,
                Email = vm.Email,
                TransactionRef = "0100",
                FullName = vm.FullName,
                Address = vm.Address,
                City = vm.City,
                Amount = 100000,
                Status = "Pending",
                Method = "Cash",
                PaidAt = DateTime.Now
            };

            entity.MethodImage = imagePath;

            await _repo.CreateAsync(entity);


            return Response<bool>.Success(true);
        }

        public async Task<Response<bool>> UpdateAsync(PaymentUpdateVM vm)
        {
            var entity = new Payment
            {
                Id = vm.Id,
                TransactionRef = vm.TransactionRef,
                Method = vm.Method,
                Amount = vm.Amount,
                Status = vm.Status
            };

            await _repo.UpdateAsync(entity);

            return Response<bool>.Success(true);
        }

        public async Task<Response<bool>> DeleteAsync(int id)
        {
            await _repo.DeleteAsync(id);

            return Response<bool>.Success(true);
        }

        //public async Task<Response<PaymentVM>> GetByIdAsync(int id)
        //{
        //    var payment = await _repo.GetByIdAsync(id);

        //    if (payment == null)
        //        return Response<PaymentVM>.Fail(PaymentError.NotFound);

        //    var vm = new PaymentVM
        //    {
        //        Id = payment.Id,
        //        OrderId = payment.OrderId,
        //        TransactionRef = (payment.TransactionRef != null) ? payment.TransactionRef : string.Empty,
        //        Method = payment.Method,
        //        Amount = payment.Amount,
        //        Status = payment.Status,
        //        PaidAt = payment.PaidAt
        //    };

        //    return Response<PaymentVM>.Success(vm);
        //}

        //public async Task<Response<IEnumerable<PaymentVM>>> GetAllAsync()
        //{
        //    var payments = await _repo.GetAll()
        //        .Select(p => new PaymentVM
        //        {
        //            Id = p.Id,
        //            OrderId = p.OrderId,
        //            TransactionRef = (p.TransactionRef != null) ? p.TransactionRef : string.Empty,
        //            Method = p.Method,
        //            Amount = p.Amount,
        //            Status = p.Status,
        //            PaidAt = p.PaidAt
        //        })
        //        .ToListAsync();

        //    return Response<IEnumerable<PaymentVM>>.Success(payments);
        //}

        //public async Task<Response<IEnumerable<PaymentVM>>> GetByUserIdAsync(string userId)
        //{
        //    var payments = await _repo.GetByUserId(userId)
        //        .Select(p => new PaymentVM
        //        {
        //            Id = p.Id,
        //            OrderId = p.OrderId,
        //            TransactionRef = (p.TransactionRef != null) ? p.TransactionRef : string.Empty,
        //            Method = p.Method,
        //            Amount = p.Amount,
        //            Status = p.Status,
        //            PaidAt = p.PaidAt
        //        })
        //        .ToListAsync();

        //    return Response<IEnumerable<PaymentVM>>.Success(payments);
        //}

        //public async Task<Response<IEnumerable<PaymentVM>>> GetByStatusAsync(string status)
        //{
        //    var payments = await _repo.GetAllByStatus(status)
        //        .Select(p => new PaymentVM
        //        {
        //            Id = p.Id,
        //            OrderId = p.OrderId,
        //            TransactionRef = (p.TransactionRef != null) ? p.TransactionRef : string.Empty,
        //            Method = p.Method,
        //            Amount = p.Amount,
        //            Status = p.Status,
        //            PaidAt = p.PaidAt
        //        })
        //        .ToListAsync();

        //    return Response<IEnumerable<PaymentVM>>.Success(payments);
        //}
    }
}