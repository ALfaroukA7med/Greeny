using Greeny.DAL.Entities;

namespace Greeny.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public string Method { get; set; }
        public int Status { get; set; }
        public DateTime PaidAt { get; set; } = DateTime.Now;
        public float Amount { get; set; }
        public string TransactionRef { get; set; }

        // Relationships
        public string UserId { get; set; }
        public User User { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
