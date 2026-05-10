namespace Greeny.BLL.ModelVM.Category
{
    public class CreateCategoryVM
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string? Icon { get; set; }
    }
}
