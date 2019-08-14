using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CarRentalDDD.Domain.Models.Customers;
using CarRentalDDD.Domain.SeedWork;
using CarRentalDDD.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CarRentalDDD.Infra.Customers.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly RentalContext _context;
        public CustomerRepository(RentalContext context)
        {
            _context = context ?? throw CustomException.NullArgument(nameof(context));
        }
        public void Add(Customer entity)
        {
            _context.Customers.Add(entity);
        }

        public async Task<IEnumerable<Customer>> FindAllAsync(ISpecification<Customer> specification)
        {
            var query = specification.Includes
                .Aggregate(_context.Set<Customer>().AsQueryable(), (current, include) => current.Include(include));
            return await query.Where(specification.Expression).AsNoTracking().ToListAsync();
        }

        public async Task<Customer> FindOneAsync(ISpecification<Customer> specification)
        {
            var query = specification.Includes
                .Aggregate(_context.Set<Customer>().AsQueryable(), (current, include) => current.Include(include));
            return await query.FirstOrDefaultAsync(specification.Expression);
        }

        public void Remove(Customer entity)
        {
            _context.Customers.Remove(entity);
        }

        public void Update(Customer entity)
        {
            _context.Customers.Update(entity);
        }
    }
}
