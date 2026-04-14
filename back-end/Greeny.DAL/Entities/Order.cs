namespace Greeny.DAL.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; } = 0.0m;
        public string Address { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime Date { get; set; } = DateTime.UtcNow;
        
        // Relationships
        public  ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();

        public int? PaymentId { get; set; }
        public  Payment? Payment { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
