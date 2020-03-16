using CarRentalDDD.Domain.Models.Cars;
using CarRentalDDD.Domain.SeedWork;
using System;
using Xunit;

namespace CarRentalDDD.Domain.Tests.Models.Cars
{
    public class MaintenanceTests
    {

        [Fact]
        public void Constructor_ThrowsOArgumentNullException()
        {
            var result = Assert.Throws<OArgumentNullException>(() => new Maintenance(DateTime.Now, "", ""));
            Assert.Equal("Service", result.ParamName);
        }
    }
}
