namespace Greeny.BLL.ModelVM.Review
{
    public class DetailsReviewVM
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public int Stars { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }

        public string? UserImage { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; }

    }
}
