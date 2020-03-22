using CarRentalDDD.Domain.SeedWork;
using CarRentalDDD.Domain.SeedWork.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarRentalDDD.Infra.Repositories
{
    public class RepositoryBase<TEntity> : IRepository<TEntity> where TEntity : Entity, IAggregateRoot
    {
        private readonly RentalContext _context;

        public RepositoryBase(RentalContext context)
        {
            _context = context ?? throw new OArgumentNullException(nameof(context));
        }
        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(IQueryRepository<TEntity> queryRepository = null)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            if (queryRepository != null)
            {
                query = AddIncludes(queryRepository.GetInclusions(), query);
                if (queryRepository.HasSpecifications)
                {
                    var expression = queryRepository.GetSpecificationExpression();
                    return await query.Where(expression).AsNoTracking().ToListAsync();
                }

            }
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> FirstAsync(IQueryRepository<TEntity> queryRepository = null)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            if (queryRepository != null)
            {
                query = AddIncludes(queryRepository.GetInclusions(), query);
                if (queryRepository.HasSpecifications)
                {
                    var expression = queryRepository.GetSpecificationExpression();
                    return await query.FirstOrDefaultAsync(expression);
                }
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<TEntity> SingleAsync(IQueryRepository<TEntity> queryRepository)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            query = AddIncludes(queryRepository.GetInclusions(), query);
            if (queryRepository.HasSpecifications)
            {
                var expression = queryRepository.GetSpecificationExpression();
                return await query.SingleOrDefaultAsync(expression);
            }
            else
            {
                return await query.SingleOrDefaultAsync();
            }
        }


        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }


        private IQueryable<TEntity> AddIncludes(IEnumerable<IInclusion<TEntity>> inclusions, IQueryable<TEntity> query)
        {
            if (inclusions != null && inclusions.Count() > 0)
                query = inclusions.Aggregate(query, (current, include) => current.Include(include.Expression));
            return query;
        }


    }
}
