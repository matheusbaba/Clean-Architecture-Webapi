using CarRentalDDD.Domain.Models.Rentals;
using CarRentalDDD.Domain.SeedWork;
using CarRentalDDD.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CarRentalDDD.Infra.Rentals.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly RentalContext _context;

        public RentalRepository(RentalContext context)
        {
            _context = context ?? throw CustomException.NullArgument(nameof(context));
        }

        public void Add(Rental entity)
        {
            _context.Rentals.Add(entity);
        }

        public async Task<IEnumerable<Rental>> FindAllAsync(ISpecification<Rental> specification)
        {
            var query = specification.Includes
                .Aggregate(_context.Set<Rental>().AsQueryable(), (current, include) => current.Include(include));
            return await query.Where(specification.Expression).AsNoTracking().ToListAsync();
        }

        public async Task<Rental> FindOneAsync(ISpecification<Rental> specification)
        {
            var query = specification.Includes
                .Aggregate(_context.Set<Rental>().AsQueryable(), (current, include) => current.Include(include));
            return await query.FirstOrDefaultAsync(specification.Expression);
        }

        public void Remove(Rental entity)
        {
            _context.Rentals.Remove(entity);
        }

        public void Update(Rental entity)
        {
            _context.Rentals.Update(entity);
        }
    }
}
