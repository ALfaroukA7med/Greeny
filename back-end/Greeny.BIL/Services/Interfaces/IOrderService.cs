using Greeny.BLL.Abstraction;
using Greeny.BLL.ModelVM.Order;
using Greeny.DAL.Enums;

namespace Greeny.BLL.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Response<IEnumerable<OrderListVM>>> GetAllAsync();
        Task<Response<OrderListVM>> GetByIdAsync(int id);

        Task<Response<IEnumerable<OrderListVM>>> GetByUserIdAsync(string userId);
        Task<Response<IEnumerable<OrderListVM>>> GetByStatusAsync(Status status);
        Task<Response<IEnumerable<OrderListVM>>> GetByUserIdAndStatusAsync(string userId, Status status);

        Task<Response<bool>> CreateAsync(OrderCreateVM order);
        Task<Response<bool>> UpdateAsync(OrderUpdateVM order);
        Task<Response<bool>> DeleteAsync(int id);

        Task<Response<IEnumerable<OrderListVM>>> GetRecentOrdersAsync();
        Task<Response<decimal>> GetTotalSalesAsync();
    }
}