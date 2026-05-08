using Greeny.BLL.ModelVM.CartItem;

public class CartItemProfile : Profile
{
    public CartItemProfile()
    {
        CreateMap<CartItem, DetailsCartItemVM>();
        CreateMap<CreateCartItemVM, CartItem>();
        CreateMap<UpdateCartItemVM, CartItem>();
    }
}