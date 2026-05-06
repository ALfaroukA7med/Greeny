namespace Greeny.DAL.Entities
{
    public class User : IdentityUser
    {
        // Additional properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? ProfilePicture { get; set; } = null;
        public string? Address { get; set; } = null;
        public string OTP { get; set; }
        public bool IsDeleted { get; set; } = false;

        // Relationships
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public ICollection<Post> Posts { get; set; } =  new HashSet<Post>();
        public ICollection<Notification> Notifications { get; set; } = new HashSet<Notification>();
        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
        public ICollection<Order> Orders { get; set; } = new HashSet<Order>();
        public ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();
        public int? CartId { get; set; } 
        public Cart? Cart { get; set; } = null;

    }
}
