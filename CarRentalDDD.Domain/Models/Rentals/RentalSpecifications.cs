using CarRentalDDD.Domain.SeedWork;
using System;

namespace CarRentalDDD.Domain.Models.Rentals
{
    public class RentalsByFiltersSpecification : SpecificationBase<Rental>
    {        
        public RentalsByFiltersSpecification(Guid? customerId, Guid? carId)
            : base(t => t.Car.Id == (carId ?? t.Car.Id) && t.Customer.Id == (customerId ?? t.Customer.Id))
        {
            this.AddInclude(t => t.Car);
            this.AddInclude(t => t.Customer);
        }
    }
}
