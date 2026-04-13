

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

        public async Task<bool> CreateAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Payment>> GetAllAsync()
        {
            return await _context.Payments.ToListAsync();
        }

        public async Task<Payment?> GetByIdAsync(string id)
        {
            var result = await _context.Payments.FirstOrDefaultAsync(p => p.Id == id);
            return result;
        }

        public async Task<bool> UpdateAsync(Payment newPayment)
        {
            var result = await _context.Payments.FirstOrDefaultAsync(p => p.Id == newPayment.Id);
            if (result == null)
            {
                return false;
            }
            result.TransactionRef = newPayment.TransactionRef;
            result.Method = newPayment.Method;
            result.Amount = newPayment.Amount;
            result.Status = newPayment.Status;
            result.PaidAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _context.Payments.FirstOrDefaultAsync(p => p.Id == id);
            if (result == null) { return false; }
            _context.Payments.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
