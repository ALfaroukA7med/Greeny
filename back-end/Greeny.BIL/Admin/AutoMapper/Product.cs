
public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, DetailsProductVM>()
       .ForMember(dest => dest.CategoryName,
        opt => opt.MapFrom(src => src.Category.Name));

        CreateMap<CreateProductVM, Product>();
        CreateMap<UpdateProductVM, Product>();
    }
}