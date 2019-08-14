using MediatR;
using System.Collections.Generic;
namespace CarRentalDDD.API.Customers.Commands
{
    public class CustomerByFiltersQuery : IRequest<IEnumerable<CustomerDTO>>
    {
        public string Name { get; set; }
        public string DriverLicense { get; set; }

        public CustomerByFiltersQuery(string name, string driverLicense)
        {
            this.Name = name ?? string.Empty;
            this.DriverLicense = driverLicense ?? string.Empty;
        }
    }
}
