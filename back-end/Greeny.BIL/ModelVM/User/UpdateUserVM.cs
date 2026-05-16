using Microsoft.AspNetCore.Http;

namespace Greeny.BLL.ModelVM.User
{
    public class UpdateUserVM
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string? LastName { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        [MaxLength(200)]
        public string? Address { get; set; }

        public IFormFile? Picture { get; set; }
        public string? ProfilePicture { get; set; }
    }
}
