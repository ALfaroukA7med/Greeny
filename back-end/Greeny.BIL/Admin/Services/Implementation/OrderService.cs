using Greeny.BLL.Admin.Errors;
using Greeny.BLL.Admin.ModelVM.Order;
using Greeny.BLL.Extension;
using Greeny.DAL.Entities;
using Greeny.DAL.Enums;
using Greeny.DAL.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Greeny.BLL.Admin.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo _orderRepo;

        public OrderService(IOrderRepo orderRepo)
        {
            _orderRepo = orderRepo;
        }

        public async Task<Response<bool>> CreateAsync(OrderCreateVM order)
        {
            var entity = new Order
            {
                Address = order.Address,
                UserId = order.UserId,
                Status = Status.Pending,
                Date = DateTime.UtcNow,
                TotalPrice = 0
            };

            await _orderRepo.CreateAsync(entity);

            return Response<bool>.Success(true);
        }

        public async Task<Response<bool>> DeleteAsync(int id)
        {
            var exists = await _orderRepo.ExistsAsync(id);

            if (!exists)
                return Response<bool>.Fail(OrderError.NotFound);

            await _orderRepo.DeleteAsync(id);

            return Response<bool>.Success(true);
        }

        public async Task<Response<OrderListVM>> GetByIdAsync(int id)
        {
            var order = await _orderRepo.GetByIdAsync(id)
                .FirstOrDefaultAsync();

            if (order == null)
                return Response<OrderListVM>.Fail(OrderError.NotFound);

            var vm = new OrderListVM
            {
                Id = order.Id,
                Address = order.Address,
                Status = order.Status,
                TotalPrice = order.TotalPrice,
                Date = order.Date,
                UserId = order.UserId
            };

            return Response<OrderListVM>.Success(vm);
        }

        public async Task<Response<IEnumerable<OrderListVM>>> GetAllAsync()
        {
            var orders = await _orderRepo.GetAllAsync()
                .Select(o => new OrderListVM
                {
                    Id = o.Id,
                    Address = o.Address,
                    Status = o.Status,
                    TotalPrice = o.TotalPrice,
                    Date = o.Date,
                    UserId = o.UserId
                })
                .ToListAsync();

            return Response<IEnumerable<OrderListVM>>.Success(orders);
        }

        public async Task<Response<bool>> UpdateAsync(OrderUpdateVM order)
        {
            var exists = await _orderRepo.ExistsAsync(order.Id);

            if (!exists)
                return Response<bool>.Fail(OrderError.NotFound);

            var entity = new Order
            {
                Id = order.Id,
                Address = order.Address,
                Status = order.Status,
                TotalPrice = order.TotalPrice
            };

            await _orderRepo.UpdateAsync(entity);

            return Response<bool>.Success(true);
        }

        public async Task<Response<IEnumerable<OrderListVM>>> GetByUserIdAsync(string userId)
        {
            var orders = await _orderRepo.GetOrdersByUserIdAsync(userId)
                .Select(o => new OrderListVM
                {
                    Id = o.Id,
                    Address = o.Address,
                    Status = o.Status,
                    TotalPrice = o.TotalPrice,
                    Date = o.Date,
                    UserId = o.UserId
                })
                .ToListAsync();

            return Response<IEnumerable<OrderListVM>>.Success(orders);
        }

        public async Task<Response<IEnumerable<OrderListVM>>> GetByStatusAsync(Status status)
        {
            var orders = await _orderRepo.GetOrdersByStatusAsync(status)
                .Select(o => new OrderListVM
                {
                    Id = o.Id,
                    Address = o.Address,
                    Status = o.Status,
                    TotalPrice = o.TotalPrice,
                    Date = o.Date,
                    UserId = o.UserId
                })
                .ToListAsync();

            return Response<IEnumerable<OrderListVM>>.Success(orders);
        }

        public async Task<Response<IEnumerable<OrderListVM>>> GetByUserIdAndStatusAsync(string userId, Status status)
        {
            var orders = await _orderRepo.GetOrdersByUserIdAndStatusAsync(userId, status)
                .Select(o => new OrderListVM
                {
                    Id = o.Id,
                    Address = o.Address,
                    Status = o.Status,
                    TotalPrice = o.TotalPrice,
                    Date = o.Date,
                    UserId = o.UserId
                })
                .ToListAsync();

            return Response<IEnumerable<OrderListVM>>.Success(orders);
        }

        public async Task<Response<IEnumerable<OrderListVM>>> GetRecentOrdersAsync()
        {
            var orders = await _orderRepo.GetRecentOrdersAsync()
                .Select(o => new OrderListVM
                {
                    Id = o.Id,
                    Address = o.Address,
                    Status = o.Status,
                    TotalPrice = o.TotalPrice,
                    Date = o.Date,
                    UserId = o.UserId
                })
                .ToListAsync();

            return Response<IEnumerable<OrderListVM>>.Success(orders);
        }

        public async Task<Response<decimal>> GetTotalSalesAsync()
        {
            var total = await _orderRepo.GetTotalSalesAsync();

            return Response<decimal>.Success(total);
        }
    }
}