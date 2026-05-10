using Greeny.DAL.Enums;
using static Greeny.BLL.Abstraction.Errors;

namespace Greeny.BLL.Errors
{
    public static class AuthError
    {
        public static Error InvalidToken
      = new Error("User.InvalidToken", "Invalid token", ErrorType.BadRequest);

        public static Error ExpiredToken
            = new Error("User.ExpiredToken", "Token has expired", ErrorType.Unauthorized);
    }
}
