namespace Greeny.BLL.Auth.ModelVM
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
