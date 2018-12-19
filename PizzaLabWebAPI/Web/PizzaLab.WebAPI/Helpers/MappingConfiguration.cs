namespace PizzaLab.WebAPI.Helpers
{
    using AutoMapper;
    using Data.Models;
    using Models.Account.FacebookModels;
    using Models.Account.InputModels;

    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            this.CreateMap<RegisterInputModel, ApplicationUser>();
            this.CreateMap<FacebookUserData, ApplicationUser>();
        }
    }
}
