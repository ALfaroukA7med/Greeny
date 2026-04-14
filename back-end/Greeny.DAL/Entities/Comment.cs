namespace Greeny.DAL.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        [Range(0,5)]
        public int Votes { get; set; } = 0;
        public DateTime Date { get; set; } = DateTime.UtcNow;

        //Relationships
        public string UserId { get; set; }
        public User User { get; set; }
        public int PostId { get; set; } 
        public Post Post { get; set; }
    }
}
