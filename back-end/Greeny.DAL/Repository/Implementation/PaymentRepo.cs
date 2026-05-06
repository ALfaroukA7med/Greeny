

using Greeny.DAL.Database;
using Greeny.DAL.Repository.Interfaces;

namespace Greeny.DAL.Repository.Implementation
{
    public class PaymentRepo : IPaymentRepo
    {
        private readonly GreenyDbContext _context;

        public PaymentRepo(GreenyDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Payment> GetAll()
        {
            return _context.Payments.Where(p=>!p.IsDeleted).AsNoTracking();
        }

        public async Task<Payment> GetByIdAsync(int id)
        {
            return await _context.Payments.Include(p=>p.Order).FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        }

        public async Task UpdateAsync(Payment newPayment)
        {
            await _context.Payments.Where(p => p.Id == newPayment.Id && !p.IsDeleted)
               .ExecuteUpdateAsync(setter => setter
               .SetProperty(p => p.TransactionRef, newPayment.TransactionRef)
               .SetProperty(p => p.Method, newPayment.Method)
               .SetProperty(p => p.Amount, newPayment.Amount)
               .SetProperty(p => p.Status, newPayment.Status)
               .SetProperty(p => p.PaidAt, DateTime.Now));
        }

        public async Task DeleteAsync(int id)
        {
            await _context.Payments.Where(p => p.Id == id && !p.IsDeleted)
               .ExecuteUpdateAsync(setter => setter
               .SetProperty(p => p.IsDeleted, true));
        }

        public IQueryable<Payment> GetByUserId(string userId)
        {
            return _context.Payments
            .Where(p => p.UserId == userId && !p.IsDeleted).AsNoTracking();
        }

      public IQueryable<Payment> GetAllByStatus(string status)
        {
            return _context.Payments
           .Where(p => p.Status == status && !p.IsDeleted)
           .AsNoTracking();
        }

    }
}
