namespace PizzaLab.WebAPI.Middlewares
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Services.DataServices.Contracts;
    using System;
    using System.Threading.Tasks;

    public class SeedCategoriesMiddleware
    {
        private readonly RequestDelegate _next;

        public SeedCategoriesMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider provider)
        {
            var categoriesService = provider.GetService<ICategoriesService>();
            if (!categoriesService.Any())
            {
                await categoriesService.CreateRangeAsync(new string[]
                {
                    "Vegetarian",
                    "Traditional",
                    "Italian",
                    "Premium",
                    "American"
                });
            }

            await this._next(context);
        }
    }
}
