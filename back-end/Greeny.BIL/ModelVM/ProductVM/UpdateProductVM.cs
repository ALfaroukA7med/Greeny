using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Greeny.BLL.ModelVM.ProductVM
{
    public class UpdateProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile? Image { get; set; }

        public string? ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem>? Categories { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
