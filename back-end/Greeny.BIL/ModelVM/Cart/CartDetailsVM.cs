using Greeny.BLL.ModelVM.CartItem;

namespace Greeny.BLL.ModelVM.Cart
{
    public class CartDetailsVM
    {
        public IEnumerable<DetailsCartItemVM> Items { get; set; }
        public decimal Total { get; set; }
    }
}
