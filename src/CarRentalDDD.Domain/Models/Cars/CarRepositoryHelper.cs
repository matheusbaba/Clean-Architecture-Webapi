using CarRentalDDD.Domain.SeedWork.Repository;
using System;

namespace CarRentalDDD.Domain.Models.Cars
{
    public class CarRepositoryHelper
    {
        public class Specifications
        {
            public static ISpecification<Car> ByMake(string make) =>
                new Specification<Car>(t => t.Make.Contains(make));


            public static ISpecification<Car> ByModel(string model) =>
                new Specification<Car>(t => t.Model.Contains(model));


            public static ISpecification<Car> ById(Guid id) =>
                new Specification<Car>(t => t.Id == id);
        }


        public class Inclusions
        {
            public static IInclusion<Car> Maintenances() =>
                new Inclusion<Car>(t => t.Maintenances);
        }

    }

}
