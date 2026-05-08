using Greeny.BLL.Abstraction;
using Greeny.BLL.ModelVM.OrderItem;

namespace Greeny.BLL.Services.Implementation
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepo _repo;

        public OrderItemService(IOrderItemRepo repo)
        {
            _repo = repo;
        }

        public async Task<Response<bool>> CreateAsync(OrderItemCreateVM vm)
        {
            var entity = new OrderItem
            {
                OrderId = vm.OrderId,
                ProductId = vm.ProductId,
                Quantity = vm.Quantity,
                UnitPrice = vm.UnitPrice
            };

            await _repo.CreateAsync(entity);

            return Response<bool>.Success(true);
        }

        public async Task<Response<bool>> UpdateAsync(OrderItemUpdateVM vm)
        {
            var entity = new OrderItem
            {
                Id = vm.Id,
                Quantity = vm.Quantity,
                UnitPrice = vm.UnitPrice
            };

            await _repo.UpdateAsync(entity);

            return Response<bool>.Success(true);
        }

        public async Task<Response<bool>> RemoveProductFromOrderAsync(int orderId, int productId)
        {
            var result = await _repo.RemoveProductFromOrderAsync(orderId, productId);

            if (!result)
                return Response<bool>.Fail(OrderItemError.NotFound);

            return Response<bool>.Success(true);
        }

        public async Task<Response<OrderItemVM>> GetByIdAsync(int id)
        {
            var item = await _repo.GetById(id)
                .FirstOrDefaultAsync();

            if (item == null)
                return Response<OrderItemVM>.Fail(OrderItemError.NotFound);

            var vm = new OrderItemVM
            {
                Id = item.Id,
                OrderId = item.OrderId,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice
            };

            return Response<OrderItemVM>.Success(vm);
        }

        public async Task<Response<IEnumerable<OrderItemVM>>> GetByOrderIdAsync(int orderId)
        {
            var items = await _repo.GetByOrderId(orderId)
                .Select(i => new OrderItemVM
                {
                    Id = i.Id,
                    OrderId = i.OrderId,
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                })
                .ToListAsync();

            return Response<IEnumerable<OrderItemVM>>.Success(items);
        }

        public async Task<Response<decimal>> GetTotalPriceByOrderIdAsync(int orderId)
        {
            var total = await _repo.GetTotalPriceByOrderIdAsync(orderId);

            return Response<decimal>.Success(total);
        }

        public async Task<Response<int>> GetItemsCountByOrderIdAsync(int orderId)
        {
            var count = await _repo.GetItemsCountByOrderIdAsync(orderId);

            return Response<int>.Success(count);
        }

        public async Task<Response<bool>> ProductExistsInOrderAsync(int orderId, int productId)
        {
            var exists = await _repo.ProductExistsInOrderAsync(orderId, productId);

            return Response<bool>.Success(exists);
        }
    }
}