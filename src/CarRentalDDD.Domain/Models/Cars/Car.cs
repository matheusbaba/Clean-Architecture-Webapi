using CarRentalDDD.Domain.SeedWork;
using System.Collections.Generic;

namespace CarRentalDDD.Domain.Models.Cars
{
    public class Car : Entity, IAggregateRoot
    {
        public string Model { get; private set; }
        public string Make { get; private set; }
        public string Registration { get; private set; }
        public int Odometer { get; private set; }
        public int Year { get; private set; }
        public ICollection<Maintenance> Maintenances { get; private set; }

        public Car(string model, string make, string registration, int year, int odometer)
        {            
            if (string.IsNullOrEmpty(model))
                throw CustomException.NullArgument(nameof(model));

            if (string.IsNullOrEmpty(make))
                throw CustomException.NullArgument(nameof(make));

            if (string.IsNullOrEmpty(registration))
                throw CustomException.NullArgument(nameof(registration)); 

            if (year < 1500)
                throw CustomException.InvalidArgument(nameof(year));

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
            this.Odometer = odometer;
        }

        public void AddMaintenance(Maintenance maintenance)
        {
            this.Maintenances.Add(maintenance ?? throw CustomException.NullArgument(nameof(maintenance)));
        }

    }


}
