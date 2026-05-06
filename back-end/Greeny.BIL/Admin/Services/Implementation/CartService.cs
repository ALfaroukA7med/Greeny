

using Greeny.BLL.Admin.ModelVM.Cart;
using Greeny.BLL.Admin.ModelVM.CartItem;

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

            var itemsVM = items.SelectMany(cart => cart.CartItems.Select(ci => new DetailsCartItemVM
            {
                ProductName = ci.Product.Name,
                CategoryName = ci.Product.Category.Name,
                ImageUrl = ci.Product.Image,
                Price = ci.Product.Price,
                Quantity = ci.Quantity
            }));

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
