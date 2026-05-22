using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Client;

namespace Greeny.BLL.ModelVM.Category
{
    public class DetailsCategoryVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int NumberOfProducts { get; set; }
        public string? Icon { get; set; }
    }
}
