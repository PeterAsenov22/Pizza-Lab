namespace PizzaLab.Data
{
    using Common;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class EfRepository<TEntity> : IRepository<TEntity>, IDisposable
        where TEntity : class
    {
        private readonly PizzaLabDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public EfRepository(PizzaLabDbContext context)
        {
            this._context = context;
            this._dbSet = context.Set<TEntity>();
        }

        public IQueryable<TEntity> All()
        {
            return this._dbSet;
        }

        public Task AddAsync(TEntity entity)
        {
            return this._dbSet.AddAsync(entity);
        }

        public Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            return this._dbSet.AddRangeAsync(entities);
        }

        public void Update(TEntity entity)
        {
            this._dbSet.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            this._dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            this._dbSet.RemoveRange(entities);
        }

        public Task<int> SaveChangesAsync()
        {
            return this._context.SaveChangesAsync();
        }

        public void Dispose()
        {
            this._context.Dispose();
        }
    }
}
