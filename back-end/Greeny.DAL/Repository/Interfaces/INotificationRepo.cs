
namespace Greeny.DAL.Repository.Interfaces
{
    public interface INotificationRepo
    {
        Task<IEnumerable<Notification>> GetAllAsync();

        Task<Notification> GetByIdAsync(int id);

        Task<bool> CreateAsync(Notification notification);

        Task<bool> UpdateAsync(Notification notification);

        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<Notification>> GetByUserIdAsync(string userId);
        Task<IEnumerable<Notification>> GetUnreadByUserIdAsync(string userId);
        Task<bool> MarkAsReadAsync(int notificationId);
        Task<int> CountUnreadAsync(string userId);
    }
}
