using Microsoft.AspNetCore.Http;

namespace Greeny.BLL.ModelVM.Payment
{
    public class PaymentCreateVM
    { 
        public int OrderId { get; set; }
        public string? TransactionRef { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime? PaidAt { get; set; }
        // Shipping
        public string FullName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }

        // Payment Image
        public string MethodImage { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}