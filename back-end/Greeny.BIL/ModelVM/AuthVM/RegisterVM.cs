namespace Greeny.BLL.ModelVM.AuthVM
{
    public class RegisterVM
    {
            [Required]
            public string FirstName { get; set; }

            [Required]
            public string LastName { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required]
            [Compare("Password")]
            [Display(Name ="Confirm Password")]
            [DataType(DataType.Password)]
           public string ConfirmPassword {  get; set; }
    }
}
