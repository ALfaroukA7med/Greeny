namespace Greeny.DAL.Entities
{
    public class CartItem
    {
        public string Id { get; set; }
        public int Quantity { get; set; } = 0;

        // by default, the total price is calculated as quantity * product price, but we can also set it manually if needed
        // public decimal TotalPrice { get; set; } = 0.0m;

        //Relationships
        public string CartId { get; set; }
        public Cart Cart { get; set; }

        public string ProductId { get; set; }
        public Product Product { get; set; }
    }
}
