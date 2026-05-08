using Greeny.DAL.Enums;
using static Greeny.BLL.Abstraction.Errors;

namespace Greeny.BLL.Errors
{
    public class ReviewError
    {
        public static Error NotFound
            = new Error("Review.NotFound", "Review not found", ErrorType.NotFound);

        public static Error InvalidData
    = new Error("Review.InvalidData", "Invalid review data", ErrorType.BadRequest);
    }


}
