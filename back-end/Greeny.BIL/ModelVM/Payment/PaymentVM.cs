namespace Greeny.BLL.ModelVM.Payment
{
    public class PaymentVM
    {
        public int Id { get; set; }
        public int OrderId { get; set; }

        public string TransactionRef { get; set; }
        public string Method { get; set; }
        public decimal Amount { get; set; }

        public string Status { get; set; }
        public DateTime? PaidAt { get; set; }
    }
}