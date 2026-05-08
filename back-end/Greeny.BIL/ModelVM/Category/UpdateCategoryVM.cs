namespace Greeny.BLL.ModelVM.Category
{
    public class UpdateCategoryVM
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string? Icon { get; set; }
    }
}
