using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Greeny.BLL.ModelVM.ProductVM
{
    public class CreateProductVM
    {
        [Required(ErrorMessage = "Product name is required")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 100 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MinLength(10, ErrorMessage = "Description must be at least 10 characters")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Image is required")]
        public IFormFile? Image { get; set; }

        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be more than 0")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 100000, ErrorMessage = "Price must be between 0.01 and 100000")]
        public decimal Price { get; set; }


        [Required(ErrorMessage = "Category is required")]
        public int? CategoryId { get; set; }

        public IEnumerable<SelectListItem>? Categories { get; set; }
    }
}
