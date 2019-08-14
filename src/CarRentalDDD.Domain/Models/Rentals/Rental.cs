using CarRentalDDD.Domain.Models.Cars;
using CarRentalDDD.Domain.Models.Customers;
using CarRentalDDD.Domain.Models.Rentals.DomainEvents;
using CarRentalDDD.Domain.SeedWork;
using System;

namespace CarRentalDDD.Domain.Models.Rentals
{
    public class Rental : Entity, IAggregateRoot
    {
        public DateTime PickUpDate { get; private set; }
        public DateTime DropOffDate { get; private set; }
        public Customer Customer { get; private set; }        
        public Car Car { get; private set; }

        protected Rental()
        {

        }
        public Rental(DateTime pickup, DateTime dropoff, Customer customer, Car car)
        {
            if (pickup > dropoff)
                throw new CustomException("Drop off date should be greater than Pick up date");
            this.Customer = customer;
            this.Car = car;

            this.AddDomainEvent(new RentalCreatedDomainEvent(this));
        }
    }
}
