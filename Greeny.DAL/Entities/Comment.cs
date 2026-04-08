namespace Greeny.DAL.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Votes { get; set; } = 0;
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
