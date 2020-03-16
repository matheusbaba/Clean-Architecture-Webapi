using CarRentalDDD.Domain.SeedWork.Repository;
using System;

namespace CarRentalDDD.Domain.Models.Rentals
{
    public class RentalRepositoryHelper
    {

        public class Specifications
        {
            public static ISpecification<Rental> ByCustomerId(Guid id) =>
                new Specification<Rental>(t => t.Customer.Id == id);


            public static ISpecification<Rental> ByCarId(Guid id) =>
                new Specification<Rental>(t => t.Car.Id == id);
        }

        public class Inclusions
        {
            public static IInclusion<Rental> Cars() =>
                            new Inclusion<Rental>(t => t.Car);


            public static IInclusion<Rental> Customers() =>
                            new Inclusion<Rental>(t => t.Customer);
        }
    }
}
