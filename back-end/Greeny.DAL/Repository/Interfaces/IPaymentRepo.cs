
namespace Greeny.DAL.Repository.Interfaces
{
    public interface IPaymentRepo
    {
        Task<IEnumerable<Payment>> GetAllAsync();

        Task<Payment> GetByIdAsync(string id);

        Task<bool> CreateAsync(Payment payment);

        Task<bool> UpdateAsync(Payment payment);

        Task<bool> DeleteAsync(string id);
    }
}
