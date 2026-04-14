namespace Greeny.DAL.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsRead { get; set; } = false;
        public DateTime Date { get; set; } = DateTime.UtcNow;
        // Relationships

        public string ReceiverId { get; set; } 
        public User Receiver { get; set; }

        public string? SenderId { get; set; }
        public User? Sender { get; set; }
    }
}
