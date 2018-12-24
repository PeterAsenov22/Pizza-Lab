namespace PizzaLab.WebAPI.Middlewares.Extensions
{
    using Microsoft.AspNetCore.Builder;

    public static class SeedProductsExtension
    {
        public static IApplicationBuilder UseSeedProductsMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SeedProductsMiddleware>();
        }
    }
}
