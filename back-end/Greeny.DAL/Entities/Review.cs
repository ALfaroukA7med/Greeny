namespace Greeny.DAL.Entities
{
    public class Review
    {
        public string Id { get; set; }
        public string? Content { get; set; } = null;
        public int Stars { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;

        // Relationships
        public string UserId { get; set; }
        public User User { get; set; }

        public string ProductId { get; set; }
        public Product Product { get; set; }
    }
}
