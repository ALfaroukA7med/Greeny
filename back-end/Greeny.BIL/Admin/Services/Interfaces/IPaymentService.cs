using Greeny.BLL.Admin.ModelVM.Payment;
using Greeny.BLL.Extension;

namespace Greeny.BLL.Admin.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<Response<bool>> CreateAsync(PaymentCreateVM vm);
        Task<Response<bool>> UpdateAsync(PaymentUpdateVM vm);
        Task<Response<bool>> DeleteAsync(int id);

        Task<Response<PaymentVM>> GetByIdAsync(int id);
        Task<Response<IEnumerable<PaymentVM>>> GetAllAsync();

        Task<Response<IEnumerable<PaymentVM>>> GetByUserIdAsync(string userId);
        Task<Response<IEnumerable<PaymentVM>>> GetByStatusAsync(string status);
    }
}