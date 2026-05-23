
using Greeny.BLL.Abstraction;
using Greeny.BLL.ModelVM.CartItem;
using Greeny.DAL.Repository.Implementation;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Greeny.BLL.Services.Implementation
{
    public class CartItemService : ICartItemService
    {

        private readonly ICartItemRepo _cartItemRepo;
        private readonly ICartRepo _cartRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public CartItemService(ICartItemRepo cartItemRepo, ICartRepo cartRepo, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _cartItemRepo = cartItemRepo;
            _cartRepo = cartRepo;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<Response<bool>> CreateAsync(CreateCartItemVM vm)
        {
            if (vm == null)
                return Response<bool>.Fail(CartItemError.InvalidData);

            var userId = _httpContextAccessor.HttpContext.User
                .FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var cart = await _cartRepo.GetByUserId(userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId
                };

                await _cartRepo.CreateAsync(cart);
            }

            var existingItem = cart.CartItems
                .FirstOrDefault(ci => ci.ProductId == vm.ProductId);

            if (existingItem != null)
            {
                existingItem.Quantity += vm.Quantity;

                await _cartItemRepo.UpdateAsync(existingItem,cart.Id);
            }
            else
            {
                var cartItem = _mapper.Map<CartItem>(vm);
                cartItem.CartId = cart.Id;

                await _cartItemRepo.CreateAsync(cartItem);
            }

            return Response<bool>.Success(true);
        }
        public async Task<Response<bool>> DeleteAsync(int id, int cartId)
        {
            var cartItem = await _cartItemRepo.GetById(id);

            if (cartItem == null || cartItem.IsDeleted || cartItem.CartId != cartId)
            {
                return Response<bool>.Fail(CartItemError.NotFound);
            }

            await _cartItemRepo.DeleteAsync(id, cartId);

            return Response<bool>.Success(true);
        }

        public async Task<Response<IEnumerable<DetailsCartItemVM>>> GetByCartId(int cartId)
        {
            var Query = _cartItemRepo.GetByCartId(cartId);
            var cartItems = await Query.ToArrayAsync();

            if (cartItems == null || !cartItems.Any())
            {
                return Response<IEnumerable<DetailsCartItemVM>>.Fail(CartItemError.NotFound);
            }

            var data = _mapper.Map<IEnumerable<DetailsCartItemVM>>(cartItems);

            return Response<IEnumerable<DetailsCartItemVM>>.Success(data);
        }

        public async Task<Response<bool>> UpdateAsync(UpdateCartItemVM vm,int cartId)
        {
            var cartItem = await _cartItemRepo.GetById(vm.Id);

            if (cartItem == null || cartItem.IsDeleted)
            {
                return Response<bool>.Fail(CartItemError.NotFound);
            }

            _mapper.Map(vm, cartItem);
            await _cartItemRepo.UpdateAsync(cartItem, cartId);

            return Response<bool>.Success(true);
        }

        public async Task<Response<bool>> DecreaseQuantityAsync(int cartItemId , int cartId)
        {
            var item = await _cartItemRepo.GetById(cartItemId);
            if (item == null)
            {
                return Response<bool>.Fail(CartItemError.NotFound);
            }

            if (item.Quantity <= 1)
            {
                await _cartItemRepo.DeleteAsync(cartItemId ,cartId);
            }
            else
            {
                item.Quantity -= 1;
                await _cartItemRepo.UpdateAsync(item,cartId);
            }

            return Response<bool>.Success(true);
        }


        public async Task<Response<bool>> IncreaseQuantityAsync(int cartItemId, int cartId)
        {
            var item = await _cartItemRepo.GetById(cartItemId);
            if (item == null)
            {
                return Response<bool>.Fail(CartItemError.NotFound);
            }

                item.Quantity += 1;
                await _cartItemRepo.UpdateAsync(item, cartId);

            return Response<bool>.Success(true);
        }


      public async  Task<Response<DetailsCartItemVM>> GetById(int Id)
        {
            var cartItem = await _cartItemRepo.GetById(Id);
            if (cartItem == null)
            {
                return Response<DetailsCartItemVM>.Fail(CartItemError.NotFound);
            }

            var data = _mapper.Map<DetailsCartItemVM>(cartItem);

            return Response<DetailsCartItemVM>.Success(data);
        }

    }
}
