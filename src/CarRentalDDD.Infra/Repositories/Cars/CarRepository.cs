using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarRentalDDD.Domain.Models.Cars;
using CarRentalDDD.Domain.SeedWork;
using CarRentalDDD.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CarRentalDDD.Infra.Cars.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly RentalContext _context;

        public CarRepository(RentalContext context)
        {
            _context = context ?? throw CustomException.NullArgument(nameof(context));
        }
        public void Add(Car entity)
        {
            _context.Cars.Add(entity);
        }

        public async Task<IEnumerable<Car>> FindAllAsync(ISpecification<Car> specification)
        {
            var query = specification.Includes
                .Aggregate(_context.Set<Car>().AsQueryable(), (current, include) => current.Include(include));
            return await query.Where(specification.Expression).AsNoTracking().ToListAsync();
        }

        public async Task<Car> FindOneAsync(ISpecification<Car> specification)
        {
            var query = specification.Includes
                .Aggregate(_context.Set<Car>().AsQueryable(), (current, include) => current.Include(include));
            return await query.FirstOrDefaultAsync(specification.Expression);
        }

        public void Remove(Car entity)
        {
            _context.Cars.Remove(entity);
        }

        public void Update(Car entity)
        {
            _context.Cars.Update(entity);
        }
    }
}
