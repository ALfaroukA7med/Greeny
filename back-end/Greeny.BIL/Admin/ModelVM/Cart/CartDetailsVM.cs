using Greeny.BLL.Admin.ModelVM.CartItem;

namespace Greeny.BLL.Admin.ModelVM.Cart
{
    public class CartDetailsVM
    {
        public IEnumerable<DetailsCartItemVM> Items { get; set; }
        public decimal Total { get; set; }
    }
}
