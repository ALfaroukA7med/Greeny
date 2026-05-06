namespace Greeny.BLL.Admin.ModelVM.Payment
{
    public class PaymentCreateVM
    {
        public int OrderId { get; set; }

        public string TransactionRef { get; set; }
        public string Method { get; set; }
        public decimal Amount { get; set; }
    }
}