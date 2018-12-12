namespace PizzaLab.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class PizzaLabDbContext : IdentityDbContext<ApplicationUser>
    {
        public PizzaLabDbContext(DbContextOptions<PizzaLabDbContext> options)
            : base(options) { }
    }
}
