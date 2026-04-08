namespace Greeny.DAL.Entities
{
    public class Catefory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Icon { get; set; } = null;
    }
}
