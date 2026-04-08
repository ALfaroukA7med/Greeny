namespace Greeny.DAL.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public string Method { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime PaidAt { get; set; } = DateTime.Now;
        public decimal Amount { get; set; } = 0.0m;
        public string TransactionRef { get; set; }
    }
}
