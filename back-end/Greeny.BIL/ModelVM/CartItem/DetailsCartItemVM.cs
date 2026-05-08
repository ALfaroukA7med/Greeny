namespace Greeny.BLL.ModelVM.CartItem
{
    public class DetailsCartItemVM
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public string CategoryName { get; set; }
    }
}
