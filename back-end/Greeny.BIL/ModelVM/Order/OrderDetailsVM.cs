using Greeny.DAL.Enums;
using Greeny.BLL.ModelVM.OrderItem;

public class OrderDetailsVM
{
    public int Id { get; set; }
    public decimal TotalPrice { get; set; }
    public string Address { get; set; }
    public Status Status { get; set; }
    public DateTime Date { get; set; }
    public string UserId { get; set; }
    public List<OrderItemDetailsVM> Items { get; set; }
}