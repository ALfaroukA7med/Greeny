using Greeny.DAL.Enums;

namespace Greeny.BLL.Admin.Response
{
    public class Errors
    {
        public record Error(string Code, string Description, ErrorType StatusCode)
        {
            public static readonly Error None = new Error(string.Empty, string.Empty, ErrorType.None);
        }
    }
}
