namespace Greeny.Models
{
    public class Blog
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public int Votes { get; set; }

        public DateTime Date { get; set; }

        //Relationships
        public ICollection<Comment> Comments { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
