namespace Greeny.BLL.ModelVM.Review
{
    public class CreateReviewVM
    {
        [Required]
        public string? Content { get; set; } = null;
        [Range(1, 5)]
        public int Stars { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public int ProductId { get; set; }
    }
}
