namespace PizzaLab.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class PizzaLabDbContext : IdentityDbContext<ApplicationUser>
    {
        public PizzaLabDbContext(DbContextOptions<PizzaLabDbContext> options)
            : base(options) { }

        public DbSet<Log> Logs { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Order> Orders { get; set; }
    }
}
