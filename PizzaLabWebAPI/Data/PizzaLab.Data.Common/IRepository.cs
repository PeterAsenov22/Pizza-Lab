namespace PizzaLab.Data.Common
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IRepository<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> All();

        Task AddAsync(TEntity entity);

        Task AddRangeAsync(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void DeleteRange(IEnumerable<TEntity> entities);

        Task<int> SaveChangesAsync();
    }
}
