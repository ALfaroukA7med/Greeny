using AutoMapper;
using Greeny.BLL.Admin.ModelVM.ProductVM;
using Greeny.DAL.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, DetailsProductVM>()
            .ForMember(dest => dest.CategoryName,
                opt => opt.MapFrom(src => src.Category.Name))

            .ForMember(dest => dest.ReviewsCount,
                opt => opt.MapFrom(src => src.Reviews.Count));

        CreateMap<CreateProductVM, Product>();
        CreateMap<UpdateProductVM, Product>();
    }
}