namespace Greeny.Models
{
    public class Order
    {
        public int Id { get; set; }
        public float TotalPrice { get; set; }
        public string Address { get; set; }
        public int Status { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        // Relationships
        public ICollection<OrderItem> OrderItems { get; set; }
        public int PaymentId { get; set; }
        public Payment Payment { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
