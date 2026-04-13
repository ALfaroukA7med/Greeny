
namespace Greeny.DAL.Repository.Interfaces
{
    public interface INotificationRepo
    {
        Task<IEnumerable<Notification>> GetAllAsync();

        Task<Notification> GetByIdAsync(string id);

        Task<bool> CreateAsync(Notification notification);

        Task<bool> UpdateAsync(Notification notification);

        Task<bool> DeleteAsync(string id);
    }
}
