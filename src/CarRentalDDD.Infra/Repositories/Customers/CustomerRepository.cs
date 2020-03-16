using CarRentalDDD.Domain.Models.Customers;
using CarRentalDDD.Infra.Repositories;

namespace CarRentalDDD.Infra.Customers.Repositories
{
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        public CustomerRepository(RentalContext context) : base(context)
        {
        }
    }
}
