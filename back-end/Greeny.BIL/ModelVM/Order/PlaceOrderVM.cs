

using Greeny.BLL.ModelVM.OrderItem;
using Microsoft.AspNetCore.Http;

namespace Greeny.BLL.ModelVM.Order
{
    public class PlaceOrderVM
    {
        // Contact
        public string Email { get; set; }
        public string Phone { get; set; }

        // Shipping
        public string FullName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }

        // Payment Image
        public string MethodImage { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}