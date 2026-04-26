using Greeny.BLL.Admin.Erorrs;
using static Greeny.BLL.Admin.Response.Errors;

namespace Greeny.BLL.Admin.Errors
{
    public class ReviewError
    {
        public static Error NotFound
            = new Error("Review.NotFound", "Review not found", ErrorType.NotFound);

        public static Error InvalidData
    = new Error("Review.InvalidData", "Invalid review data", ErrorType.BadRequest);
    }


}
