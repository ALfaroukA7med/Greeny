using Greeny.DAL.Repository.Interfaces;
using Greeny.DAL.Database;
namespace Greeny.DAL.Repository.Implementation
{
    public class NotificationRepo : INotificationRepo
    {

        private readonly GreenyDbContext _context;

        public NotificationRepo(GreenyDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(Notification notification)
        {
                await _context.Notifications.AddAsync(notification);
                await _context.SaveChangesAsync();
                return true;
        }

        public async Task<IEnumerable<Notification>> GetAllAsync()
        {
                return await _context.Notifications.ToListAsync();
        }

        public async Task<Notification?> GetByIdAsync(string id) 
        {
                var result = await _context.Notifications.FirstOrDefaultAsync(n => n.Id == id);
                return result;
        }

        public async Task<bool> UpdateAsync(Notification newNotification)
        {
                var result = await _context.Notifications.FirstOrDefaultAsync(n => n.Id == newNotification.Id);

            if (result == null) { return false; }

                result.Content = newNotification.Content;
                result.Date = DateTime.Now;
                await _context.SaveChangesAsync();
                return true;
        }

        public async Task<bool> DeleteAsync(string id)
        {
                var result = await _context.Notifications.FirstOrDefaultAsync(n => n.Id == id);
                if (result == null) { return false; }
                _context.Notifications.Remove(result);
                await _context.SaveChangesAsync();
                return true;
        }

    }
}
