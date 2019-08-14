using CarRentalDDD.Domain.SeedWork;
using System;

namespace CarRentalDDD.Domain.Models.Cars
{
    public class CarByIdWithMaintenancesSpecification : SpecificationBase<Car>
    {
        public CarByIdWithMaintenancesSpecification(Guid id)
            : base(t => t.Id == id)
        {
            AddInclude(t => t.Maintenances);
        }
    }

    public class CarByIdSpecification : SpecificationBase<Car>
    {
        public CarByIdSpecification(Guid id)
            : base(t => t.Id == id)
        {
        }
    }

    public class CarsByFiltersSpecification : SpecificationBase<Car>
    {
        public CarsByFiltersSpecification(string model, string make)
            : base(t => t.Make.Contains(model) && t.Model.Contains(make))
        {
        }
    }
}
