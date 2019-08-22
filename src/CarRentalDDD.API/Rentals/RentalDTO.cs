using CarRentalDDD.API.Cars;
using CarRentalDDD.API.Customers;
using System;

namespace CarRentalDDD.API.Rentals
{
    public class RentalDTO
    {
        public Guid Id { get; set; }
        public DateTime PickUpDate { get; set; }
        public DateTime DropOffDate { get; set; }
        public CustomerDTO Customer { get; set; }
        public CarDTO Car { get; set; }
    }
}
