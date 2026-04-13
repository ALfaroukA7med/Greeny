namespace Greeny.DAL.Entities
{
    public class Payment
    {
        public string Id { get; set; }
        public string Method { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime? PaidAt { get; set; }
        public decimal Amount { get; set; }
        public string? TransactionRef { get; set; }
         
        // Relationships
        public string UserId { get; set; }
        public  User User { get; set; }

        public string OrderId { get; set; }
        public  Order Order { get; set; }
    }
}
