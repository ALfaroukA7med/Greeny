

namespace Greeny.Models
{
    public class Review
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int Stars { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        // product
        public int UserId { get; set; }
        public User User { get; set; } 

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
