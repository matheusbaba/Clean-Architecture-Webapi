using CarRentalDDD.Domain.Models.Cars;
using CarRentalDDD.Domain.SeedWork;
using System;
using Xunit;

namespace CarRentalDDD.Domain.Tests.Models.Cars
{
    public class CarTests
    {
        
        [Theory]
        [InlineData("", "make", "registration", "Model")]
        [InlineData("model", "", "registration", "Make")]
        [InlineData("model", "make", "", "Registration")]
        public void Constructor_ThrowsOArgumentNullException(string model, string make, string registration, string parameter)
        {
            var result = Assert.Throws<OArgumentNullException>(()=> new Car(model,make,registration,2010,50));
            Assert.Equal(parameter,  result.ParamName);
        }

        [Theory]
        [InlineData(1499,0, "Year")]
        [InlineData(1399, 0, "Year")]
        [InlineData(2000, -1, "Odometer")]
        [InlineData(2000, -15, "Odometer")]
        public void Constructor_ThrowsOInvalidArgumentException(int year, int odometer, string parameter)
        {
            var result = Assert.Throws<OInvalidArgumentException>(() => new Car("model", "make", "registration", year, odometer));
            Assert.Equal(parameter, result.ParamName);
        }
        

        [Theory]
        [InlineData(-1)]
        [InlineData(-1000)]
        public void UpdateOdometer_ThrowsInvalidArgumentException(int odometer)
        {
            var car = new Car("model", "make", "reg", 2000, 20000);
            var result = Assert.Throws<OInvalidArgumentException>(() => car.UpdateOdometer(odometer));
            Assert.Equal("Odometer", result.ParamName);
        }

        [Fact]
        public void UpdateOdometer_ShouldWork()
        {
            var car = new Car("model", "make", "reg", 2000, 20000);
            int expected = 40000;
            car.UpdateOdometer(expected);
            Assert.Equal(expected, car.Odometer);
        }

        
        [Fact]        
        public void AddMaintenance_ThrowsOArgumentNullException()
        {
            var car = new Car("model", "make", "reg", 2000, 20000);
            var result = Assert.Throws<OArgumentNullException>(() => car.AddMaintenance(null));
            Assert.Equal("Maintenance", result.ParamName);
        }


        [Fact]
        public void AddMaintenance_ShouldWork()
        {
            var car = new Car("model", "make", "reg", 2000, 20000);
            var maintenance = new Maintenance(DateTime.Now, "service", "descr");

            car.AddMaintenance(maintenance);

            Assert.True(car.Maintenances.Contains(maintenance));
        }
    }
}
