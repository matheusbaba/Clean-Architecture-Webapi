using MediatR;

namespace CarRentalDDD.API.Cars.Commands
{
    public class CreateCarCommand : IRequest<CarDTO>
    {
        public string Model { get; set; }

        public string Make { get; set; }

        public string Registration { get; set; }

        public int Odmometer { get; set; }

        public int Year { get; set; }

        public CreateCarCommand(string model, string make, string registration, int odometer, int year)
        {
            this.Model = model;
            this.Make = make;
            this.Registration = registration;
            this.Year = year;
        }
    }
}
