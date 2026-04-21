
namespace Greeny.DAL.Repository.Interfaces
{
    public interface IPaymentRepo
    {
        Task<IEnumerable<Payment>> GetAllAsync();

        Task<Payment> GetByIdAsync(int id);

        Task<bool> CreateAsync(Payment payment);

        Task<bool> UpdateAsync(Payment payment);

        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<Payment>> GetByUserIdAsync(string userId);
        Task<Payment> GetByOrderIdAsync(int orderId);
        Task<bool> ExistsByOrderIdAsync(int orderId);
        Task<IEnumerable<Payment>> GetAllByStatusAsync(string status);
    }
}
