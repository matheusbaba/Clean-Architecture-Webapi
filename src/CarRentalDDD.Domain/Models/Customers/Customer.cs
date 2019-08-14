using CarRentalDDD.Domain.Models.Shared;
using CarRentalDDD.Domain.SeedWork;
using System;

namespace CarRentalDDD.Domain.Models.Customers
{
    public class Customer : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public Email Email { get; private set; }
        public string DriverLicense { get; private set; }
        public DateTime DOB { get; private set; }
        public Address Address { get; private set; }
        public Phone Phone { get; private set; }

        protected Customer()
        {
        }
        public Customer(string name, string driverLicense, DateTime DOB, Email email, Address address, Phone phone)
        {
            if (string.IsNullOrEmpty(name))
                throw CustomException.NullArgument(nameof(name));

            if (string.IsNullOrEmpty(driverLicense))
                throw CustomException.NullArgument(nameof(driverLicense));

            this.Name = name;
            this.DriverLicense = driverLicense;
            this.DOB = DOB;
            this.Address = address ?? throw CustomException.NullArgument(nameof(address));
            this.Phone = phone ?? throw CustomException.NullArgument(nameof(phone));
            this.Email = email ?? throw CustomException.NullArgument(nameof(email));
        }

        public void UpdateEmail(Email email)
        {
            this.Email = email ?? throw CustomException.NullArgument(nameof(email));
        }
        public void UpdatePhone(Phone phone)
        {
            this.Phone = phone ?? throw CustomException.NullArgument(nameof(phone));
        }
        public void UpdateAddress(Address address)
        {
            this.Address = address ?? throw CustomException.NullArgument(nameof(address));
        }

    }
}
