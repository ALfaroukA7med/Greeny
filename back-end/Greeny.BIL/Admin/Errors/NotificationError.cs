using Greeny.DAL.Enums;
using static Greeny.BLL.Admin.Response.Errors;

namespace Greeny.BLL.Admin.Errors
{
    public class NotificationError
    {
        public static Error NotFound = new Error("Notification.NotFound", "Notification not found", ErrorType.NotFound);
        public static Error InvaildData = new Error("Notification.InvaildData", "Notification Invaild data", ErrorType.BadRequest);
    }
}
