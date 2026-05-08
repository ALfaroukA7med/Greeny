using Greeny.BLL.Abstraction;
using Greeny.BLL.Errors;
using Greeny.BLL.ModelVM.Notification;
using Greeny.BLL.Services.Interfaces;


namespace Greeny.BLL.Services.Implementation
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepo _notificationRepo;
        public NotificationService(INotificationRepo notificationRepo)
        {
            _notificationRepo = notificationRepo;
        }

        public async Task<Response<bool>> CreateAsync(CreateNotificationVM model)
        {
            if (model == null)
            {
                return Response<bool>.Fail(NotificationError.InvaildData);
            }

            var Notification = new Notification
            {
                Content = model.Message,
                ReceiverId = model.ReceiverId,
                SenderId = model.SenderId,
                Date = model.CreateAt
            };

            await _notificationRepo.CreateAsync(Notification);
            return Response<bool>.Success(true);
        }

        public async Task<Response<IEnumerable<DetailsNotificationVM>>> GetUserNotificationsAsync(string userId)
        {
            var Notifications = await _notificationRepo.GetByUserId(userId)
                .Select(n => new DetailsNotificationVM
                {
                    SenderName = n.Sender.FirstName + " " + n.Sender.LastName,
                    Content = n.Content,
                    IsRead = false,
                    CreatedAt = n.Date
                })
                .ToListAsync();
               
            return Response<IEnumerable<DetailsNotificationVM>>.Success(Notifications);
        }

        public async Task<Response<bool>> MarkAsReadAsync(int notificationId)
        {
            var notification = await _notificationRepo.MarkAsReadAsync(notificationId);
            return Response<bool>.Success(true);
        }
    }
}
