using Greeny.DAL.Enums;
using static Greeny.BLL.Abstraction.Errors;

namespace Greeny.BLL.Errors
{
    public class CartError
    {
        public static Error NotFound = new Error("Cart.NotFound", "Cart not found", ErrorType.NotFound);
    }
}
