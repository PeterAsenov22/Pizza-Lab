namespace PizzaLab.WebAPI.Middlewares.Extensions
{
    using Microsoft.AspNetCore.Builder;

    public static class SeedCategoriesExtension
    {
        public static IApplicationBuilder UseSeedCategoriesMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SeedCategoriesMiddleware>();
        }
    }
}
