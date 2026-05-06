using Greeny.BLL.Admin.ModelVM.OrderItem;
using Greeny.BLL.Extension;

namespace Greeny.BLL.Admin.Services.Interfaces
{
    public interface IOrderItemService
    {
        Task<Response<bool>> CreateAsync(OrderItemCreateVM vm);
        Task<Response<bool>> UpdateAsync(OrderItemUpdateVM vm);
        Task<Response<bool>> RemoveProductFromOrderAsync(int orderId, int productId);

        Task<Response<OrderItemVM>> GetByIdAsync(int id);
        Task<Response<IEnumerable<OrderItemVM>>> GetByOrderIdAsync(int orderId);

        Task<Response<decimal>> GetTotalPriceByOrderIdAsync(int orderId);
        Task<Response<int>> GetItemsCountByOrderIdAsync(int orderId);

        Task<Response<bool>> ProductExistsInOrderAsync(int orderId, int productId);
    }
}