using Greeny.DAL.Enums;
using System;
using System.Collections.Generic;
using Greeny.BLL.ModelVM.OrderItem;

public class OrderDetailsVM
{
    public int Id { get; set; }
    public decimal TotalPrice { get; set; }
    public string Address { get; set; }
    public Status Status { get; set; }
    public DateTime Date { get; set; }

    public string UserName { get; set; }

    public List<OrderItemVM> Items { get; set; }
}