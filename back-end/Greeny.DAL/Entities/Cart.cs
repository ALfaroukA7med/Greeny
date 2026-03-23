namespace Greeny.Models
{
    public class Cart
    {
        public int Id { get; set; }

        // Relationships
        public ICollection<CartItem> CartItems { get; set; } = null;

        public string UserId { get; set; }
        public User User { get; set; }

    }
}
