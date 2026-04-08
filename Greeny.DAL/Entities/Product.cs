namespace Greeny.DAL.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; } = null;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
