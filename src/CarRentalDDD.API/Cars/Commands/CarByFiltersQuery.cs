using MediatR;
using System.Collections.Generic;

namespace CarRentalDDD.API.Cars.Commands
{
    public class CarByFiltersQuery : IRequest<IEnumerable<CarDTO>>
    {
        public string Model { get; set; }
        public string Make { get; set; }

        public CarByFiltersQuery(string model, string make)
        {
            this.Model = model ?? string.Empty;
            this.Make = make ?? string.Empty;
        }
    }
}
