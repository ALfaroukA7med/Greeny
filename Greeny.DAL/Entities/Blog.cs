namespace Greeny.DAL.Entities
{
    public class Blog
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Votes { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
