using AutoMapper;
using Greeny.BLL.Abstraction;
using Greeny.BLL.ModelVM.CartItem;
using Greeny.BLL.Services.Interfaces;
using Greeny.DAL.Repository.Interfaces;

namespace Greeny.BLL.Services.Implementation
{
    public class CartItemService : ICartItemService
    {

        private readonly ICartItemRepo _cartItemRepo;
        private readonly IMapper _mapper;

        public CartItemService(ICartItemRepo cartItemRepo, IMapper mapper)
        {
            _cartItemRepo = cartItemRepo;
            _mapper = mapper;
        }

        public async Task<Response<bool>> CreateAsync(CreateCartItemVM vm)
        {
            if (vm == null)
            {
                return Response<bool>.Fail(CartItemError.InvalidData);
            }

            var cartItem = _mapper.Map<CartItem>(vm);

            await _cartItemRepo.CreateAsync(cartItem);

            return Response<bool>.Success(true);
        }

        public async Task<Response<bool>> DeleteAsync(int id)
        {
            var cartItem = await _cartItemRepo.GetById(id);

            if (cartItem == null || cartItem.IsDeleted)
            {
                return Response<bool>.Fail(CartItemError.NotFound);
            }

            await _cartItemRepo.DeleteAsync(id);

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

        public async Task<Response<bool>> UpdateAsync(UpdateCartItemVM vm)
        {
            var cartItem = await _cartItemRepo.GetById(vm.Id);

            if (cartItem == null || cartItem.IsDeleted)
            {
                return Response<bool>.Fail(CartItemError.NotFound);
            }

            _mapper.Map(vm, cartItem);
            await _cartItemRepo.UpdateAsync(cartItem);

            return Response<bool>.Success(true);
        }

        public async Task<Response<bool>> DecreaseQuantityAsync(int cartItemId)
        {
            var item = await _cartItemRepo.GetById(cartItemId);
            if (item == null)
            {
                return Response<bool>.Fail(CartItemError.NotFound);
            }

            if (item.Quantity <= 1)
            {
                await _cartItemRepo.DeleteAsync(cartItemId);
            }
            else
            {
                item.Quantity -= 1;
                await _cartItemRepo.UpdateAsync(item);
            }

            return Response<bool>.Success(true);
        }


        public async Task<Response<bool>> IncreaseQuantityAsync(int cartItemId)
        {
            var item = await _cartItemRepo.GetById(cartItemId);
            if (item == null)
            {
                return Response<bool>.Fail(CartItemError.NotFound);
            }

                item.Quantity += 1;
                await _cartItemRepo.UpdateAsync(item);

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
