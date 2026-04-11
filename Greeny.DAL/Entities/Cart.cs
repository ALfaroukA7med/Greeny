namespace Greeny.DAL.Entities
{
    public class Cart
    {
        public int Id { get; set; }

        // Relationships
        public ICollection<CartItem> CartItems { get; set; } = new HashSet<CartItem>();

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
