namespace Greeny.BLL.Admin.ModelVM.Cart
{
    public class CartDetailsVM
    {
        public IEnumerable<CartItemVM> Items { get; set; }
        public decimal Total { get; set; }
    }
}
