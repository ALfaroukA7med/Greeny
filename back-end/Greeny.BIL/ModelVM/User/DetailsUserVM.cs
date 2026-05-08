namespace Greeny.BLL.ModelVM.User
{
    public class DetailsUserVM
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? ProfilePicture { get; set; } = null;
        public string? Address { get; set; } = null;
    }
}
