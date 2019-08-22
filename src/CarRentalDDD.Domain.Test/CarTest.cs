using CarRentalDDD.Domain.Models.Cars;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using FluentAssertions;

namespace CarRentalDDD.Domain.Test
{
    [TestClass]
    public class CarTest
    {
        [TestMethod]
        public void Create_ReturnsCarCreated()
        {
            Mock<Car> expected = new Mock<Car>();
            expected.SetupGet(t => t.Model).Returns("Corola");
            expected.SetupGet(t => t.Make).Returns("Toyota");
            expected.SetupGet(t => t.Registration).Returns("123456");
            expected.SetupGet(t => t.Odometer).Returns(50000);
            expected.SetupGet(t => t.Year).Returns(2000);
            expected.SetupGet(t => t.Maintenances).Returns(new List<Maintenance>());

            var actual = new Car("Corola", "Toyota", "123456", 2000, 50000);
            actual.Should().BeEquivalentTo(expected.Object);            
        }
    }
}
