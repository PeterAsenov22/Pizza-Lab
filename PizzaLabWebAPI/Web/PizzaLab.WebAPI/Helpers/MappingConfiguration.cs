namespace PizzaLab.WebAPI.Helpers
{   
    using AutoMapper;
    using Data.Models;  
    using Models.Account.FacebookModels;
    using Models.Account.InputModels;
    using Models.Categories.ViewModels;
    using Models.Ingredients.ViewModels;
    using Models.Orders.InputModels;
    using Models.Orders.ViewModels;
    using Models.Products.ViewModels;
    using Models.Reviews.ViewModels;
    using Services.DataServices.Models.Categories;
    using Services.DataServices.Models.Ingredients;
    using Services.DataServices.Models.Orders;
    using Services.DataServices.Models.Products;
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

            this.CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Likes, opt => opt.MapFrom(src => src.Likes.Select(l => l.ApplicationUser)))
                .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.Ingredients.Select(i => i.Ingredient)));
            this.CreateMap<ProductDto, Product>()
                .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.Ingredients.Select(idto => new ProductsIngredients
                {
                    IngredientId = idto.Id
                })))
                .ForMember(dest => dest.Category, opt => opt.Ignore())
                .ForMember(dest => dest.Likes, opt => opt.Ignore());
            this.CreateMap<ProductDto, ProductViewModel>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Ingredients,
                    opt => opt.MapFrom(src => src.Ingredients.Select(i => i.Name)))
                .ForMember(dest => dest.Likes,
                    opt => opt.MapFrom(src => src.Likes.Select(u => u.UserName)));

            this.CreateMap<Category, CategoryDto>();
            this.CreateMap<CategoryDto, CategoryViewModel>();

            this.CreateMap<Ingredient, IngredientDto>();
            this.CreateMap<IngredientDto, IngredientViewModel>();

            this.CreateMap<OrderProductInputModel, OrderProductDto>();
            this.CreateMap<OrderProductDto, OrderProductViewModel>();
            this.CreateMap<OrderProductDto, OrderProduct>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            this.CreateMap<OrderProduct, OrderProductDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name));
            this.CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.CreatorEmail, opt => opt.MapFrom(src => src.Creator.Email))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.OrderProducts, opt => opt.MapFrom(src => src.Products));
            this.CreateMap<OrderDto, OrderViewModel>()
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.CreationDate));
        }
    }
}
