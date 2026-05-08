using Greeny.DAL.Enums;
using static Greeny.BLL.Abstraction.Errors;

namespace Greeny.BLL.Errors
{
    public class RefPlanetError
    {
        public static Error NotFound
            = new Error("RefPlanet.NotFound", "RefPlanet not found", ErrorType.NotFound);

        public static Error InvalidData
    = new Error("RefPlanet.InvalidData", "Invalid RefPlanet data", ErrorType.BadRequest);
    }
}
