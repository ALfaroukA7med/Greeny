

using Greeny.BLL.Admin.ModelVM.Cart;

namespace Greeny.BLL.Admin.Services.Implementation
{
    public class CartService : ICartService
    {
        private readonly ICartRepo _repo;
        public CartService(ICartRepo repo)
        {
            _repo = repo;
        }

        public async Task<Response<CartDetailsVM>> GetAllItem(int cartId)
        {

            var items = _repo.GetById(cartId);

            if (items == null || !items.Any())
            {
                return Response<CartDetailsVM>.Fail(Errors.CartError.NotFound);
            }
            
            var itemsVM = items.Select(item => new CartItemVM
            {
                ProductName = item.Product.Name, 
                CategoryName = item.Product.Category?.Name,
                ImageUrl = item.Product.ImageUrl,
                Price = item.Product.Price,
                Quantity = item.Quantity
            });
            
            var totalPrice = itemsVM.Sum(item => item.Price * item.Quantity);
            
            CartDetailsVM Result = new CartDetailsVM
            {
                Items = itemsVM,
                Total = totalPrice
            };
            return Response<CartDetailsVM>.Success(Result);
        }
    }
}
