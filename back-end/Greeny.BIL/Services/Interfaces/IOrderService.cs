using Greeny.BLL.Abstraction;
using Greeny.DAL.Enums;

namespace Greeny.BLL.Services.Interfaces
{
    public interface IOrderService
    {
        Task<Response<IEnumerable<OrderDetailsVM>>> GetAllAsync();
        Task<Response<OrderDetailsVM>> GetByIdAsync(int id);
        Task<Response<IEnumerable<OrderDetailsVM>>> GetByUserIdAsync(string userId);
        Task<Response<IEnumerable<OrderDetailsVM>>> GetByStatusAsync(Status status);
        //Task<Response<IEnumerable<OrderListVM>>> GetByUserIdAndStatusAsync(string userId, Status status);
        //Task<Response<bool>> CreateAsync(OrderCreateVM order);
        Task<Response<bool>> DeleteAsync(int id);
        Task<Response<IEnumerable<OrderDetailsVM>>> GetRecentOrdersAsync();
        Task<Response<decimal>> GetTotalSalesAsync();
        Task<Response<int>> CheckoutAsync(int cartId, string address);
    }
}