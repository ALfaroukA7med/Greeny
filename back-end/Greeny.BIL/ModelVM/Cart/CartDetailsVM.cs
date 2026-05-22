using Greeny.BLL.ModelVM.CartItem;

namespace Greeny.BLL.ModelVM.Cart
{
    public class CartDetailsVM
    {
        public int CartId { get; set; }
        public IEnumerable<DetailsCartItemVM> Items { get; set; }
        public decimal Total { get; set; }
    }
}
