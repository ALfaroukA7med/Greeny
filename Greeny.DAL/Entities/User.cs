namespace Greeny.DAL.Entities
{
    public class User : IdentityUser
    {
        // Additional properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? ProfilePicture { get; set; } = null;
        public string? Address { get; set; } = null;
        public bool IsDeleted { get; set; } = false;

        
    }
}
