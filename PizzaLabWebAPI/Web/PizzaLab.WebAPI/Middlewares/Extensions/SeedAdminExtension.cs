namespace PizzaLab.WebAPI.Middlewares.Extensions
{
    using Microsoft.AspNetCore.Builder;

    public static class SeedAdminExtension
    {
        public static IApplicationBuilder UseSeedAdminMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SeedAdminMiddleware>();
        }
    }
}
