namespace PizzaLab.Data
{
    using Common;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class EfRepository<TEntity> : IRepository<TEntity>, IDisposable
        where TEntity : class
    {
        private readonly PizzaLabDbContext context;
        private DbSet<TEntity> dbSet;

        public EfRepository(PizzaLabDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public IQueryable<TEntity> All()
        {
            return this.dbSet;
        }

        public Task AddAsync(TEntity entity)
        {
            return this.dbSet.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            this.dbSet.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            this.dbSet.Remove(entity);
        }

        public Task<int> SaveChangesAsync()
        {
            return this.context.SaveChangesAsync();
        }

        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}
