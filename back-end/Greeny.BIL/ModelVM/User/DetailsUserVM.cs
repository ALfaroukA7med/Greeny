using Microsoft.AspNetCore.Http;

namespace Greeny.BLL.ModelVM.User
{
    public class DetailsUserVM
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; } = string.Empty;
        public string? ProfilePicture { get; set; } = null;
        public string? Address { get; set; } = null;
        public bool IsDeleted { get; set; }
    }
}
