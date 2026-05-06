namespace Greeny.BLL.Admin.ModelVM.Notification
{
    public class CreateNotificationVM
    {
        public string ReceiverId { get; set; }

        public string SenderId { get; set; }

        public string Message { get; set; }

        public DateTime CreateAt { get; set; }
    }
}
