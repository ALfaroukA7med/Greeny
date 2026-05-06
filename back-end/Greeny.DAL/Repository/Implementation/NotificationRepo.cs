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

        public async Task CreateAsync(Notification notification)
        {
                await _context.Notifications.AddAsync(notification);
                await _context.SaveChangesAsync();
        }

        public IQueryable<Notification> GetAll()
        {
                return _context.Notifications
                .Where(n=>!n.IsDeleted)
                .AsNoTracking();
        }

        public IQueryable<Notification> GetById(int id)
        {
            return _context.Notifications
            .Where(n => n.Id == id)
            .AsNoTracking();
        }

        public Task GetByIdAsync(int notificationId)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Notification> GetByUserId(string userId)
        {
            return _context.Notifications
            .Where(n => n.ReceiverId == userId)
            .AsNoTracking();
        }


        public IQueryable<Notification> GetUnreadByUserId(string userId)
        {
            return _context.Notifications
            .Where(n => n.ReceiverId == userId && !n.IsRead)
            .AsNoTracking();
        }

        public async Task<bool> MarkAsReadAsync(int notificationId)
        {
            var rowsaffected = await _context.Notifications
                .Where(n => n.Id == notificationId)
                .ExecuteUpdateAsync(setter => setter
                .SetProperty(p => p.IsRead, true)
                );

            return rowsaffected > 0;
        }

        public async Task<bool> UnreadExists(string userId)
        {
            return await _context.Notifications
                .AnyAsync(n => n.ReceiverId == userId && !n.IsRead && !n.IsDeleted);
        }
    }
}
