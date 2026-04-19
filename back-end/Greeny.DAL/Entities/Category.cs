namespace Greeny.DAL.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Icon { get; set; } = null;
        public bool IsDeleted { get; set; } = false;

        // Relationships
        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
