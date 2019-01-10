namespace PizzaLab.WebAPI
{
    using AutoMapper;
    using Data;
    using Data.Common;
    using Data.Models;
    using Helpers;
    using Helpers.Logging;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.IdentityModel.Tokens;
    using Middlewares.Extensions;
    using Services.DataServices;
    using Services.DataServices.Contracts;
    using Swashbuckle.AspNetCore.Swagger;
    using System.Text;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PizzaLabDbContext>(options =>
            {
                options.UseSqlServer(
                    this.Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddAutoMapper();

            var facebookSettingsSection = Configuration.GetSection("FacebookSettings");
            services.Configure<FacebookSettings>(facebookSettingsSection);

            var jwtSettingsSection = Configuration.GetSection("JwtSettings");
            services.Configure<JwtSettings>(jwtSettingsSection);

            // configure jwt authentication
            var jwtSettings = jwtSettingsSection.Get<JwtSettings>();
            var key = Encoding.ASCII.GetBytes(jwtSettings.Secret);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                    options.SignIn.RequireConfirmedEmail = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredUniqueChars = 0;
                    options.Password.RequiredLength = 8;
                })
                .AddEntityFrameworkStores<PizzaLabDbContext>()
                .AddDefaultTokenProviders();

            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<ICategoriesService, CategoriesService>();
            services.AddScoped<IIngredientsService, IngredientsService>();
            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<IReviewsService, ReviewsService>();
            services.AddScoped<IProductsIngredientsService, ProductsIngredientsService>();
            services.AddScoped<IUsersLikesService, UsersLikesService>();
            services.AddScoped<IOrdersService, OrdersService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "PizzaLab Web API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            loggerFactory.AddContext(LogLevel.Error, app);

            app.Use(async (ctx, next) =>
            {
                await next();
                if (ctx.Response.StatusCode == 204)
                {
                    ctx.Response.ContentLength = 0;
                }
            });

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseSeedAdminMiddleware();
            app.UseSeedCategoriesMiddleware();
            app.UseSeedIngredientsMiddleware();
            app.UseSeedProductsMiddleware();

            app.UseHttpsRedirection();
            app.UseAuthentication(); 
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzaLab API V1");
            });
        }
    }
}
