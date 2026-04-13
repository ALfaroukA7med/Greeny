namespace Greeny.DAL.Entities
{
    public class OrderItem
    {
        public string Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        // Relationships
        public string ProductId { get; set; }
        public virtual Product Product { get; set; }

        public string OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
