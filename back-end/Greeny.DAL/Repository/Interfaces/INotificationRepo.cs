
namespace Greeny.DAL.Repository.Interfaces
{
    public interface INotificationRepo
    {
        IQueryable<Notification> GetById(int id);



        Task CreateAsync(Notification notification);
        IQueryable<Notification> GetByUserId(string userId);

        //IQueryable<Notification>> GetUnreadByUserIdAsync(string userId);

        Task<bool> MarkAsReadAsync(int notificationId);
        Task<bool> UnreadExists(string userId);
        Task GetByIdAsync(int notificationId);
    }
}
