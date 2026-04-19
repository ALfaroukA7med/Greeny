using AutoMapper;
using Greeny.BLL.Admin.ModelVM.ProductVM;
using Greeny.DAL.Entities;
public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, DetailsProductVM>();
        CreateMap<CreateProductVM, Product>();
        CreateMap<UpdateProductVM, Product>();
    }
}