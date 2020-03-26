using CarRentalDDD.API.Customers.Commands;
using CarRentalDDD.Domain.Models.Customers;
using CarRentalDDD.Domain.SeedWork;
using FakeItEasy;
using System;
using System.Threading;
using Xunit;

namespace CarRentalDDD.API.Tests.Customers.Commands
{

    // Using FakeItEasy

    public class CreateCustomerCommandTests
    {
        [Theory]
        [InlineData("Baba", "123123", "1991/04/04", "street", "city", "123123", "email@email.com", "123123123")]
        [InlineData("Schenina", "22222222", "1981-04-04", "street2", "city2", "5666666", "email_2@email.com", "5454545")]
        public void Handler_ShouldWork(string name, string driverLicense, string dob, string street, string city, string zipcode, string email, string phone)
        {            
            var customerRepository = A.Fake<ICustomerRepository>();
            var uow = A.Fake<IUnitOfWork>();

            var command = new CreateCustomerCommand(name, driverLicense, DateTime.Parse(dob), street, city, zipcode, email, phone);
            var handler = new CreateCustomerCommand.Handler(customerRepository, uow, APITestsHelper.GetMapper());

            var result = handler.Handle(command, new CancellationToken()).Result;

            A.CallTo(() => customerRepository.Add(A<Customer>._)).MustHaveHappenedOnceExactly();
            A.CallTo(() => uow.CommitAsync(A<CancellationToken>._)).MustHaveHappenedOnceExactly();
            Assert.Equal(name, result.Name);
            Assert.Equal(driverLicense, result.DriverLicense);
            Assert.Equal(DateTime.Parse(dob), result.DOB);
            Assert.Equal(street, result.Street);
            Assert.Equal(city, result.City);
            Assert.Equal(zipcode, result.ZipCode);
            Assert.Equal(email, result.Email);
            Assert.Equal(phone, result.Phone);
        }
    }
}
