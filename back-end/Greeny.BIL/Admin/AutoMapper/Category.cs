

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, DetailsCategoryVM>();
        CreateMap<CreateCategoryVM, Category>();
        CreateMap<UpdateCategoryVM, Category>();
    }
}