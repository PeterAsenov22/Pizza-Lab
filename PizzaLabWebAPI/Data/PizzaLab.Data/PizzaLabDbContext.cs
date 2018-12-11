namespace PizzaLab.Data
{
    using Microsoft.EntityFrameworkCore;

    public class PizzaLabDbContext : DbContext
    {
        public PizzaLabDbContext(DbContextOptions options)
            : base(options) { }
    }
}
