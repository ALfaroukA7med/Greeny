namespace Greeny.DAL.Entities
{
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; } = null;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; } = false;


        // Relationships
        public string CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();

        public ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();
        public ICollection<CartItem> CartItems { get; set; } = new HashSet<CartItem>();
    }
}
