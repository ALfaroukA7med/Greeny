using Greeny.DAL.Enums;
using static Greeny.BLL.Admin.Response.Errors;

namespace Greeny.BLL.Admin.Errors
{
    public class CartError
    {
        public static Error NotFound = new Error("Cart.NotFound", "Cart not found", ErrorType.NotFound);
    }
}
