using Greeny.BLL.Abstraction;
using Greeny.BLL.Extension;
using Greeny.BLL.ModelVM.Payment;

namespace Greeny.BLL.Services.Interfaces
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