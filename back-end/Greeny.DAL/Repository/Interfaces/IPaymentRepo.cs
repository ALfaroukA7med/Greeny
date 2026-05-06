
namespace Greeny.DAL.Repository.Interfaces
{
    public interface IPaymentRepo
    {
        IQueryable<Payment> GetAll();

        Task<Payment> GetByIdAsync(int id);

        Task CreateAsync(Payment payment);

        Task UpdateAsync(Payment payment);

        Task DeleteAsync(int id);

        IQueryable<Payment> GetByUserId(string userId);
        IQueryable<Payment> GetAllByStatus(string status);
    }
}
