using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRentalDDD.Domain.SeedWork.Repository
{
    public interface IRepository<TEntity> where TEntity: IAggregateRoot
    {
        Task<TEntity> FirstAsync(IQueryRepository<TEntity> queryRepository);
        Task<TEntity> SingleAsync(IQueryRepository<TEntity> queryRepository);
        Task<IEnumerable<TEntity>> FindAllAsync(IQueryRepository<TEntity> queryRepository = null);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}
