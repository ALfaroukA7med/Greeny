namespace Greeny.BLL.ModelVM.Notification
{
    public class DetailsNotificationVM
    {

            public int Id { get; set; }
            public string SenderName { get; set; }

            public string Content { get; set; }

            public bool IsRead { get; set; }

            public DateTime CreatedAt { get; set; }
            public string? Url { get; set; }
    }
}
