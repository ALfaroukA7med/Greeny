using Greeny.DAL.Enums;
using static Greeny.BLL.Abstraction.Errors;

namespace Greeny.BLL.Errors
{
    public class CartItemError
    {
        public static Error NotFound
           = new Error("CartItem.NotFound", "CartItem not found", ErrorType.NotFound);

        public static Error InvalidData
    = new Error("CartItem.InvalidData", "Invalid CartItem data", ErrorType.BadRequest);
    }
}
