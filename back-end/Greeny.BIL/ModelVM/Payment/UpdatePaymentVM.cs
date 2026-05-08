namespace Greeny.BLL.ModelVM.Payment
{
    public class PaymentUpdateVM
    {
        public int Id { get; set; }

        public string TransactionRef { get; set; }
        public string Method { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
    }
}