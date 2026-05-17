using Greeny.BLL.ModelVM.Review;

namespace Greeny.BLL.ModelVM.ProductVM
{
    public class DetailsProductVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Image { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public double averageRating { get; set; } = 0;
        public int totalReviews { get; set; } = 0;

        public List<DetailsReviewVM> Reviews { get; set; }
    }
}
