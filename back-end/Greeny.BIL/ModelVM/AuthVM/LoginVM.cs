using System.ComponentModel.DataAnnotations;

namespace Greeny.BLL.ModelVM.AuthVM
{
    public class LoginVM
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name ="Remember Me !!")]
        public bool RememberMe { get; set;}
    }
}
