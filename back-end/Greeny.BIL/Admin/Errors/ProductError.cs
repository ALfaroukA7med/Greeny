using static Greeny.BLL.Admin.Response.Errors;

namespace Greeny.BLL.Admin.Erorrs
{
    public static class ProductError
    {
        public static Error NotFound
            = new Error("Product.NotFound", "Product not found", ErrorType.NotFound);

        public static Error InvalidData
   = new Error("Product.InvalidData", "Invalid Product data", ErrorType.BadRequest);
    }
}
