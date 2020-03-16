using CarRentalDDD.Domain.SeedWork.Repository;
using System;

namespace CarRentalDDD.Domain.Models.Customers
{
    public class CustomerRepositoryHelper
    {
        public class Specifications
        {

            /// <summary>
            /// Filter customer by driver license
            /// </summary>
            public static ISpecification<Customer> ByDriverLicense(string driverLicense) =>
                new Specification<Customer>(t => t.DriverLicense.Contains(driverLicense));

            /// <summary>
            /// Filter customer by name
            /// </summary>
            public static ISpecification<Customer> ByName(string name) =>
                new Specification<Customer>(t => t.Name.Contains(name));

            /// <summary>
            /// Filter customer by id
            /// </summary>
            public static ISpecification<Customer> ById(Guid id) =>
                new Specification<Customer>(t => t.Id == id);
        }

        public class Inclusions
        {

        }
    }
}
