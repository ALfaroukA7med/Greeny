using Greeny.DAL.Enums;

namespace Greeny.BLL.Admin.ModelVM.Order
{
    public class OrderListVM
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public string Address { get; set; }
        public Status Status { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
    }
}