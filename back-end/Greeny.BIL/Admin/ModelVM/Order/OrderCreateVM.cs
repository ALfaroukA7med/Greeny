using Greeny.BLL.Admin.ModelVM.OrderItem;

public class OrderCreateVM
{
    public string Address { get; set; }

    public string UserId { get; set; }

    public List<OrderItemCreateVM> Items { get; set; }
}