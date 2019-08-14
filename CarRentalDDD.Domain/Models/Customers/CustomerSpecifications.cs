using CarRentalDDD.Domain.SeedWork;
using System;

namespace CarRentalDDD.Domain.Models.Customers
{
    public class CustomersByFiltersSpecification : SpecificationBase<Customer>
    {
        public CustomersByFiltersSpecification(string name, string driverlicense)
            : base(t => t.Name.Contains(name) && t.DriverLicense.Contains(driverlicense))
        {

        }
    }

    public class CustomerByIdSpecification : SpecificationBase<Customer>
    {
        public CustomerByIdSpecification(Guid id)
            : base(t => t.Id == id)
        {
        }
    }
}
