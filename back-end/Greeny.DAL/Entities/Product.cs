namespace Greeny.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; } = null;
        public int Quantity { get; set; }
        public float Price { get; set; }

        // Relationships
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Review> Reviews { get; set; } = null;

        public int OrderItemId { get; set; }
        public OrderItem OrderItem { get; set; }

        public int CartItemId { get; set; }
        public CartItem CartItem { get; set; }
    }
}
