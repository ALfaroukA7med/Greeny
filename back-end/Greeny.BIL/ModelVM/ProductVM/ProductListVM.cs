namespace Greeny.BLL.ModelVM.ProductVM
{
    public class ProductListVM
    {
        public int ProudctId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }
        public double Rating { get; set; }
        public int TotalReviews { get; set; }
        public int NumberOfProuduts { get; set; }
        public string Image { get; set; }

        public int Quantity { get; set; }

    }
}
