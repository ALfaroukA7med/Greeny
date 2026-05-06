
using Greeny.DAL.Enums;
using static Greeny.BLL.Admin.Response.Errors;

namespace Greeny.BLL.Admin.Errors
{
    public class CategoryError
    {
        public static Error NotFound
            = new Error("Category.NotFound", "Category not found", ErrorType.NotFound);

        public static Error InvalidData
    = new Error("Category.InvalidData", "Invalid Category data", ErrorType.BadRequest);
    }
}
