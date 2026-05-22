using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Greeny.BLL.ModelVM.Category
{
    public class CreateCategoryVM
    {

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public IFormFile? Icon { get; set; }
        public string? IconUrl { get; set; }
    }
}
