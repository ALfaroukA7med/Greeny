

public class ReviewProfile : Profile
{
    public ReviewProfile()
    {
        CreateMap<Review, DetailsReviewVM>()
       .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
       .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));

        CreateMap<Review, CreateCategoryVM>();
        CreateMap<Review, UpdateCategoryVM>();
    }
}
