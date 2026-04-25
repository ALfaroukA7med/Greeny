
namespace Greeny.DAL.Repository.Interfaces
{
    public interface INotificationRepo
    {
        //IQueryable<Notification>GetAllAsync();

        IQueryable<Notification> GetByIdAsync(int id);

        Task CreateAsync(Notification notification);

        //Task UpdateAsync(Notification notification);

        //Task DeleteAsync(int id);

        IQueryable<Notification> GetByUserIdAsync(string userId);
        //IQueryable<Notification>> GetUnreadByUserIdAsync(string userId);
        Task<bool> MarkAsReadAsync(int notificationId);
        Task<int> UnreadExists(string userId);
    }
}
