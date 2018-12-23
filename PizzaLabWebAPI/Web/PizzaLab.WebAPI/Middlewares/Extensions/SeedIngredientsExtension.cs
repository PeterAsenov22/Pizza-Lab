namespace PizzaLab.WebAPI.Middlewares.Extensions
{
    using Microsoft.AspNetCore.Builder;

    public static class SeedIngredientsExtension
    {
        public static IApplicationBuilder UseSeedIngredientsMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SeedIngredientsMiddleware>();
        }
    }
}
