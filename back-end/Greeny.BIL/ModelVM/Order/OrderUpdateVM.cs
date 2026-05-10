using Greeny.DAL.Enums;

public class OrderUpdateVM
{
    public int Id { get; set; }
    public string Address { get; set; }
    public Status Status { get; set; }
    public decimal TotalPrice { get; set; } = 0;
}