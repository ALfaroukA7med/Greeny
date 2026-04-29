using Greeny.DAL.Database;

namespace Greeny.DAL.Repository.Interfaces
{
    public interface IPaymentService
    {
        Task<Response<bool>> CreateAsync(Payment payment);
        Task<Response<IEnumerable<Payment>>> GetAllAsync();
        Task<Response<Payment?>> GetByIdAsync(int id);
        Task<Response<bool>> UpdateAsync(Payment newPayment);
        Task<Response<bool>> DeleteAsync(int id);
        Task<Response<IEnumerable<Payment>>> GetByUserIdAsync(string userId);
        Task<Response<Payment?>> GetByOrderIdAsync(int orderId);
        Task<Response<bool>> ExistsByOrderIdAsync(int orderId);
        Task<Response<IEnumerable<Payment>>> GetAllByStatusAsync(string status);
    }
}