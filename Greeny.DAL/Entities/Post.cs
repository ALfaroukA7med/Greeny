namespace Greeny.DAL.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Votes { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string? ImagePath { get; set; }


        //Relationships
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public string UserId { get; set; }
        public User User { get; set; }
    }
}
