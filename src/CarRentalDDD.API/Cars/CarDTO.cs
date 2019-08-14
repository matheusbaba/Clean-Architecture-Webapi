using System;

namespace CarRentalDDD.API.Cars
{
    public class CarDTO
    {
        public Guid Id { get; set; }
        public string Model { get; set; }
        public string Make { get; set; }
        public string Registration { get; set; }
        public int Odometer { get; set; }
        public int Year { get; set; }
    }
}
