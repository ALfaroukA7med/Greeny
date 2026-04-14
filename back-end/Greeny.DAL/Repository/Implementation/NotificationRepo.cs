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

        public async Task<Notification?> GetByIdAsync(int id) 
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

        public async Task<bool> DeleteAsync(int id)
        {
                var result = await _context.Notifications.FirstOrDefaultAsync(n => n.Id == id);
                if (result == null) { return false; }
                _context.Notifications.Remove(result);
                await _context.SaveChangesAsync();
                return true;
        }

        public async Task<IEnumerable<Notification>> GetByUserIdAsync(string userId)
        {
            return await _context.Notifications.Where(n => n.ReceiverId == userId).Include(n=>n.Sender).ToListAsync();
        }


        public async Task<IEnumerable<Notification>> GetUnreadByUserIdAsync(string userId)
        {
            return await _context.Notifications.Where(n => n.ReceiverId == userId && n.IsRead==false).Include(n => n.Sender).ToListAsync();
        }

        public async Task<bool> MarkAsReadAsync(int notificationId)
        {
            var notification = await _context.Notifications
                .FirstOrDefaultAsync(n => n.Id == notificationId);

            if (notification == null)
                return false;

            notification.IsRead = true;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<int> CountUnreadAsync(string userId)
        {
            return await _context.Notifications
               .CountAsync(n => n.ReceiverId == userId && n.IsRead == false);
        }



    }
}
