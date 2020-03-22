using CarRentalDDD.Domain.Models.Rentals;
using CarRentalDDD.Domain.SeedWork;
using System;
using System.Collections.Generic;

namespace CarRentalDDD.Domain.Models.Cars
{
    public class Car : Entity, IAggregateRoot
    {
        public virtual string Model { get; private set; }
        public virtual string Make { get; private set; }
        public virtual string Registration { get; private set; }
        public virtual int Odometer { get; private set; }
        public virtual int Year { get; private set; }
        public virtual ICollection<Maintenance> Maintenances { get; private set; }
        public ICollection<Rental> Rentals { get; private set; }

        public Car(string model, string make, string registration, int year, int odometer)
        {
            if (string.IsNullOrEmpty(model))
                throw new OArgumentNullException(nameof(Model));

            if (string.IsNullOrEmpty(make))
                throw new OArgumentNullException(nameof(Make));

            if (string.IsNullOrEmpty(registration))
                throw new OArgumentNullException(nameof(Registration));

            if (year < 1500)
                throw new OInvalidArgumentException(nameof(Year));

            if (odometer < 0)
                throw new OInvalidArgumentException(nameof(Odometer));

            this.Model = model;
            this.Make = make;
            this.Registration = registration;
            this.Year = year;
            this.Odometer = odometer;
            this.Maintenances = new List<Maintenance>();
        }

        protected Car()
        {
        }

        public void UpdateOdometer(int odometer)
        {
            if (odometer < 0)
                throw new OInvalidArgumentException(nameof(Odometer));
            this.Odometer = odometer;
        }

        public void AddMaintenance(Maintenance maintenance)
        {
            this.Maintenances.Add(maintenance ?? throw new OArgumentNullException(nameof(Maintenance)));
        }

    }
}
