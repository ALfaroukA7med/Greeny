using Greeny.BLL.Abstraction;
using Greeny.BLL.ModelVM.CartItem;
using Greeny.BLL.Errors;
using Greeny.BLL.ModelVM.Cart;
using Greeny.BLL.Services.Interfaces;

namespace Greeny.BLL.Services.Implementation
{
    public class CartService : ICartService
    {
        private readonly ICartRepo _repo;
        public CartService(ICartRepo repo)
        {
            _repo = repo;
        }

        public async Task<Response<CartDetailsVM>> GetAllItem(string userId)
        {

            var items = await _repo.GetByUserId(userId);

            if (items == null)
            {
                return Response<CartDetailsVM>.Fail(CartError.NotFound);
            }

            var itemsVM = items.CartItems.Where(c=>!c.IsDeleted).Select(ci => new DetailsCartItemVM
            {
                Id = ci.Id,
                ProductName = ci.Product.Name,
                CategoryName = ci.Product.Category.Name,
                ImageUrl = ci.Product.Image,
                Price = ci.Product.Price,
                Quantity = ci.Quantity
            }).ToList();

            var totalPrice = itemsVM.Sum(item => item.Price * item.Quantity);
            
            CartDetailsVM Result = new CartDetailsVM
            {
                CartId= items.Id,
                Items = itemsVM,
                Total = totalPrice
            };
            return Response<CartDetailsVM>.Success(Result);
        }

    }
}
