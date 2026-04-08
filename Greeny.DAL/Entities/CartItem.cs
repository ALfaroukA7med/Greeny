namespace Greeny.DAL.Entities
{
    public class CartItem
    {
        public int Id { get; set; }
        public int Quantity { get; set; } = 0;
        public decimal TotalPrice { get; set; } = 0.0m;
    }
}
