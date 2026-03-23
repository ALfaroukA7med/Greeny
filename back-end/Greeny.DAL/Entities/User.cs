namespace Greeny.DAL.Entities
{
    public class User : IdentityUser
    {
        public User() { }

        public User(string FName, string LName, string address, string? profilePicture = null)
        {
            FirstName = FName;
            LastName = LName;
            Address = address;
            ProfilePicture = profilePicture;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? ProfilePicture { get; set; } = null;
        public string? Address { get; set; } = null;
        public bool IsDeleted { get; set; } = false;

        // Relationships
        public ICollection<Comment> Comments { get; set; } = null;
        public ICollection<Blog> Blogs { get; set; } = null;
        public ICollection<Notification> Notifications { get; set; } = null;
        public ICollection<Review> Reviews { get; set; } = null;
        public ICollection<Order> Orders { get; set; } = null;
        public ICollection<Payment> Payments { get; set; } = null;
        public int CartId { get; set; }
        public Cart Cart { get; set; }

        //Methods
        public void Delete()
        {
            IsDeleted = true;
        }
        public void Edit(string FName, string LName, string address, string? profilePicture = null)
        {
            FirstName = FName;
            LastName = LName;
            Address = address;
            ProfilePicture = profilePicture;
        }
    }
}
