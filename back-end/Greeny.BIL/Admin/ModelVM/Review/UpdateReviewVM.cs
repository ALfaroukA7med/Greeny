namespace Greeny.BLL.Admin.ModelVM.Review
{
    public class UpdateReviewVM
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        [Range(1,5)]
        public int Stars { get; set; }
        public DateTime Date { get; set; }
        public int ProductId { get; set; }
        public bool IsDeleted { get; set; } 
    }
}
