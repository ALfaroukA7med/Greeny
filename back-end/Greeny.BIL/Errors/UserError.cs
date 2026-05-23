using Greeny.DAL.Enums;
using static Greeny.BLL.Abstraction.Errors;

namespace Greeny.BLL.Errors
{
    public static class UserError
    {
        public static Error NotFound
            = new Error("User.NotFound", "User not found", ErrorType.NotFound);

        public static Error InvalidData
           = new Error("User.InvalidData", "Invalid User data", ErrorType.BadRequest);

        public static Error Unauthorized
           = new Error("User.InvalidData", "Invalid User data", ErrorType.Unauthorized);
    }
}
