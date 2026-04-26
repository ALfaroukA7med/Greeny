

using Greeny.DAL.Enums;
using static Greeny.BLL.Admin.Response.Errors;

namespace Greeny.BLL.Admin.Errors
{
    public class RefPlanetError
    {
        public static Error NotFound
            = new Error("RefPlanet.NotFound", "RefPlanet not found", ErrorType.NotFound);

        public static Error InvalidData
    = new Error("RefPlanet.InvalidData", "Invalid RefPlanet data", ErrorType.BadRequest);
    }
}
