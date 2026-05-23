namespace Greeny.BLL.ModelVM.OrderItem
{
    public class OrderItemDetailsVM
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; } 
        public string ProductImage { get; set; }
        public decimal Total => Quantity * UnitPrice;
    }
}