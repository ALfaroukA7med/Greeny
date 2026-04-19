using AutoMapper;
using Greeny.BLL.Admin.ModelVM.Category;
using Greeny.DAL.Entities;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, DetailsCategoryVM>();
        CreateMap<CreateCategoryVM, Category>();
        CreateMap<UpdateCategoryVM, Category>();
    }
}