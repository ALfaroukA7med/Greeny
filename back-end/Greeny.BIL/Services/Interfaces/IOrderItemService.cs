using Greeny.BLL.Abstraction;
using Greeny.BLL.ModelVM.OrderItem;

namespace Greeny.BLL.Services.Interfaces
{
    public interface IOrderItemService
    {
        //Task<Response<bool>> CreateAsync(OrderItemCreateVM vm);
        Task<Response<bool>> RemoveProductFromOrderAsync(int orderId, int productId);
        Task<Response<OrderItemDetailsVM>> GetByIdAsync(int id);
        Task<Response<IEnumerable<OrderItemDetailsVM>>> GetByOrderIdAsync(int orderId);

        //Task<Response<decimal>> GetTotalPriceByOrderIdAsync(int orderId);
        //Task<Response<int>> GetItemsCountByOrderIdAsync(int orderId);
        //Task<Response<bool>> ProductExistsInOrderAsync(int orderId, int productId);
    }
}