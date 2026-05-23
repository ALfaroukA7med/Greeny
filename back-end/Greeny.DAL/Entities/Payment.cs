namespace Greeny.DAL.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public string? Method { get; set; } = "Cash";
        public string? Status { get; set; } = "Pending";
        public DateTime? PaidAt { get; set; }
        public decimal? Amount { get; set; }
        public string? TransactionRef { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string MethodImage { get; set; }
        public bool IsDeleted { get; set; } = false;



        // Relationships
        public string UserId { get; set; }
        public  User User { get; set; }

        public int OrderId { get; set; }
        public  Order Order { get; set; }
    }
}
