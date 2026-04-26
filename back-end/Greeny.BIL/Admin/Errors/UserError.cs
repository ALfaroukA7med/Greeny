
using Greeny.DAL.Enums;
using static Greeny.BLL.Admin.Response.Errors;

namespace Greeny.BLL.Admin.Errors
{
    public static class UserError
    {
        public static Error NotFound
            = new Error("User.NotFound", "User not found", ErrorType.NotFound);

        public static Error InvalidData
   = new Error("User.InvalidData", "Invalid User data", ErrorType.BadRequest);
    }
}
