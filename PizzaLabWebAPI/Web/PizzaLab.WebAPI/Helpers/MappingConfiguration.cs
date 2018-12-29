namespace PizzaLab.WebAPI.Helpers
{
    using AutoMapper;
    using Data.Models;
    using Models.Account.FacebookModels;
    using Models.Account.InputModels;   
    using Models.Categories.ViewModels;
    using Models.Ingredients.ViewModels;
    using Models.Reviews.ViewModels;
    using Models.Products.ViewModels;
    using System.Linq;  

    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            this.CreateMap<RegisterInputModel, ApplicationUser>();
            this.CreateMap<FacebookUserData, ApplicationUser>();
            this.CreateMap<Review, ReviewViewModel>()
                .ForMember(dest => dest.ReviewText, opt => opt.MapFrom(src => src.Text))
                .ForMember(dest => dest.CreatorUsername, opt => opt.MapFrom(src => src.Creator.UserName));
            this.CreateMap<Product, ProductViewModel>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Ingredients,
                    opt => opt.MapFrom(src => src.Ingredients.Select(i => i.Ingredient.Name)))
                .ForMember(dest => dest.Likes,
                    opt => opt.MapFrom(src => src.Likes.Select(ul => ul.ApplicationUser.UserName)));
            this.CreateMap<Category, CategoryViewModel>();
            this.CreateMap<Ingredient, IngredientViewModel>();
        }
    }
}
