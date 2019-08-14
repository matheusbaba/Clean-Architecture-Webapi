using MediatR;
using System;

namespace CarRentalDDD.API.Customers.Commands
{
    public class CreateCustomerCommand : IRequest<CustomerDTO>
    {
        public string Name { get; set; }
        public string DriverLicense { get; set; }
        public DateTime DOB { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public CreateCustomerCommand(string name, string driverLicense, DateTime DOB, string street, string city, string zipCode, string email, string phone)
        {
            this.Name = name;
            this.DriverLicense = driverLicense;
            this.DOB = DOB;
            this.Street = street;
            this.City = city;
            this.ZipCode = zipCode;
            this.Phone = phone;
            this.Email = email;
        }
    }

}
