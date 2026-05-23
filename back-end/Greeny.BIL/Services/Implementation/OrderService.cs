using Greeny.BLL.Abstraction;
using Greeny.BLL.ModelVM.OrderItem;
using Greeny.DAL.Enums;
using MailKit.Search;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;


namespace Greeny.BLL.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo _orderRepo;
        private readonly ICartItemRepo _cartItemRepo;
        private readonly ICartRepo _cartRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public OrderService(IOrderRepo orderRepo,ICartItemRepo cartItemRepo, IHttpContextAccessor httpContextAccessor, ICartRepo cartRepo)
        {
            _orderRepo = orderRepo;
            _cartItemRepo = cartItemRepo;
            _cartRepo = cartRepo;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Response<OrderDetailsVM>> CheckoutAsync(int cartId)
        {
            var cartItems = await _cartItemRepo.GetByCartId(cartId).ToListAsync();

            if (!cartItems.Any())
                return Response<OrderDetailsVM>.Fail(CartError.Empty);
            var userId = _httpContextAccessor.HttpContext.User
              .FindFirst(ClaimTypes.NameIdentifier)?.Value;
            decimal total = 0;

            var order = new Order
            {
                Status = Status.Pending,
                Date = DateTime.UtcNow,
                UserId= userId,
                OrderItems = new List<OrderItem>()
            };

            foreach (var item in cartItems)
            {
                var orderItem = new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.Product.Price
                };

                total += orderItem.UnitPrice * orderItem.Quantity;

                order.OrderItems.Add(orderItem);
            }

            order.TotalPrice = total;

            await _orderRepo.CreateAsync(order);

            await _cartItemRepo.ClearCartAsync(cartId);

            var resultVm = new OrderDetailsVM
            {
                Id = order.Id,
                TotalPrice = order.TotalPrice,
                Address = "Egypt",
                Status = order.Status,
                Date = order.Date,
                UserId = order.UserId,
                Items = order.OrderItems.Select(i => new OrderItemDetailsVM
                {
                    ProductId = i.ProductId,
                    ProductName = i.Product?.Name,
                    CategoryName = i.Product?.Category?.Name,
                    ProductImage = i.Product?.Image,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };
            return Response<OrderDetailsVM>.Success(resultVm);
        }

        //public async Task<Response<bool>> CreateAsync(OrderCreateVM order)
        //{
        //    var entity = new Order
        //    {
        //        Address = order.Address,
        //        UserId = order.UserId,
        //        Status = Status.Pending,
        //        Date = DateTime.UtcNow,
        //        TotalPrice = 0
        //    };

        //    await _orderRepo.CreateAsync(entity);

        //    return Response<bool>.Success(true);
        //}

        public async Task<Response<bool>> DeleteAsync(int id)
        {
            var exists = await _orderRepo.ExistsAsync(id);

            if (!exists)
                return Response<bool>.Fail(OrderError.NotFound);

            await _orderRepo.DeleteAsync(id);

            return Response<bool>.Success(true);
        }

        public async Task<Response<OrderDetailsVM>> GetByIdAsync(int id)
        {
            var order = await _orderRepo.GetByIdAsync(id).FirstOrDefaultAsync();

            if (order == null)
                return Response<OrderDetailsVM>.Fail(OrderError.NotFound);

            var vm = new OrderDetailsVM
            {
                Id = order.Id,
                Address = order.Address,
                Status = order.Status,
                TotalPrice = order.TotalPrice,
                Date = order.Date,
                UserId = order.UserId,

                Items = order.OrderItems.Select(i => new OrderItemDetailsVM
                {
                    Id = i.Id,
                    OrderId = i.OrderId,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };

            return Response<OrderDetailsVM>.Success(vm);
        }

        public async Task<Response<IEnumerable<OrderDetailsVM>>> GetAllAsync()
        {
            var orders = await _orderRepo.GetAllAsync()
                .Select(o => new OrderDetailsVM
                {
                    Id = o.Id,
                    Address = o.Address,
                    Status = o.Status,
                    TotalPrice = o.TotalPrice,
                    Date = o.Date,
                    UserId = o.UserId,
                    Items = o.OrderItems.Select(i => new OrderItemDetailsVM
                    {
                        Id = i.Id,
                        OrderId = i.OrderId,
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice
                    }).ToList()
                })
                .ToListAsync();

            return Response<IEnumerable<OrderDetailsVM>>.Success(orders);
        }

        //public async Task<Response<bool>> UpdateAsync(OrderUpdateVM order)
        //{
        //    var exists = await _orderRepo.ExistsAsync(order.Id);

        //    if (!exists)
        //        return Response<bool>.Fail(OrderError.NotFound);

        //    var entity = new Order
        //    {
        //        Id = order.Id,
        //        Address = order.Address,
        //        Status = order.Status,
        //        TotalPrice = order.TotalPrice
        //    };

        //    await _orderRepo.UpdateAsync(entity);

        //    return Response<bool>.Success(true);
        //}

        public async Task<Response<IEnumerable<OrderDetailsVM>>> GetByUserIdAsync(string userId)
        {
            var orders = await _orderRepo.GetOrdersByUserIdAsync(userId)
                .Select(o => new OrderDetailsVM
                {
                    Id = o.Id,
                    Address = o.Address,
                    Status = o.Status,
                    TotalPrice = o.TotalPrice,
                    Date = o.Date,
                    UserId = o.UserId,
                    Items = o.OrderItems.Select(i => new OrderItemDetailsVM
                    {
                        Id = i.Id,
                        OrderId = i.OrderId,
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice
                    }).ToList()
                })
                .ToListAsync();

            return Response<IEnumerable<OrderDetailsVM>>.Success(orders);
        }

        public async Task<Response<IEnumerable<OrderDetailsVM>>> GetByStatusAsync(Status status)
        {
            var orders = await _orderRepo.GetOrdersByStatusAsync(status)
                .Select(o => new OrderDetailsVM
                {
                    Id = o.Id,
                    Address = o.Address,
                    Status = o.Status,
                    TotalPrice = o.TotalPrice,
                    Date = o.Date,
                    UserId = o.UserId,
                    Items = o.OrderItems.Select(i => new OrderItemDetailsVM
                    {
                        Id = i.Id,
                        OrderId = i.OrderId,
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice
                    }).ToList()
                })
                .ToListAsync();

            return Response<IEnumerable<OrderDetailsVM>>.Success(orders);
        }

        //public async Task<Response<IEnumerable<OrderDetailsVM>>> GetByUserIdAndStatusAsync(string userId, Status status)
        //{
        //    var orders = await _orderRepo.GetOrdersByUserIdAndStatusAsync(userId, status)
        //        .Select(o => new OrderDetailsVM
        //        {
        //            Id = o.Id,
        //            Address = o.Address,
        //            Status = o.Status,
        //            TotalPrice = o.TotalPrice,
        //            Date = o.Date,
        //            UserId = o.UserId,
        //            Items = o.OrderItems.Select(i => new OrderItemDetailsVM
        //            {
        //                Id = i.Id,
        //                OrderId = i.OrderId,
        //                ProductId = i.ProductId,
        //                Quantity = i.Quantity,
        //                UnitPrice = i.UnitPrice
        //            }).ToList()
        //        })
        //        .ToListAsync();

        //    return Response<IEnumerable<OrderDetailsVM>>.Success(orders);
        //}

        public async Task<Response<IEnumerable<OrderDetailsVM>>> GetRecentOrdersAsync()
        {
            var orders = await _orderRepo.GetRecentOrdersAsync()
                .Select(o => new OrderDetailsVM
                {
                    Id = o.Id,
                    Address = o.Address,
                    Status = o.Status,
                    TotalPrice = o.TotalPrice,
                    Date = o.Date,
                    UserId = o.UserId,
                    Items = o.OrderItems.Select(i => new OrderItemDetailsVM
                    {
                        Id = i.Id,
                        OrderId = i.OrderId,
                        ProductId = i.ProductId,
                        Quantity = i.Quantity,
                        UnitPrice = i.UnitPrice
                    }).ToList()
                })
                .ToListAsync();

            return Response<IEnumerable<OrderDetailsVM>>.Success(orders);
        }

        public async Task<Response<decimal>> GetTotalSalesAsync()
        {
            var total = await _orderRepo.GetTotalSalesAsync();

            return Response<decimal>.Success(total);
        }
    }
}