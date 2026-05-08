using Greeny.DAL.Enums;
using static Greeny.BLL.Abstraction.Errors;

namespace Greeny.BLL.Errors
{
    public static class ProductError
    {
        public static Error NotFound
            = new Error("Product.NotFound", "Product not found", ErrorType.NotFound);

        public static Error InvalidData
            = new Error("Product.InvalidData", "Invalid Product data", ErrorType.BadRequest);
    }
}
