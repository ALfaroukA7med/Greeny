

using Greeny.BLL.Admin.ModelVM.Notification;


namespace Greeny.BLL.Admin.Services.Interfaces
{
    public interface INotificationService
    {
        public Task<Response<bool>> CreateAsync(CreateNotificationVM model);

        public Task<Response<IEnumerable<DetailsNotificationVM>>> GetUserNotificationsAsync(string userId);

        public Task<Response<bool>> MarkAsReadAsync(int notificationId);
    }
}
