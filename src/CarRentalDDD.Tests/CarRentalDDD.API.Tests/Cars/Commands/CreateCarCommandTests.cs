using CarRentalDDD.API.Cars.Commands;
using CarRentalDDD.Domain.Models.Cars;
using CarRentalDDD.Domain.SeedWork;
using Moq;
using System.Threading;
using Xunit;

namespace CarRentalDDD.API.Tests.Cars.Commands
{
    public class CreateCarCommandTests
    {   
        [Theory]
        [InlineData("make1", "model1", "123213", 2000, 0)]
        [InlineData("make2", "model2", "555555", 2020, 50000)]
        public void Handler_ShouldWork(string make, string model, string registration, int year, int odometer)
        {            
            var carRepositoryMock = new Mock<ICarRepository>();
            var uowMock = new Mock<IUnitOfWork>();

            var handler = new CreateCarCommand.Handler(carRepositoryMock.Object, uowMock.Object, APITestsHelper.GetMapper());
            var createCarCommand = new CreateCarCommand(model, make, registration, odometer, year);

            var result = handler.Handle(createCarCommand, new CancellationToken()).Result;

            carRepositoryMock.Verify(t => t.Add(It.IsAny<Car>()), Times.Once);
            uowMock.Verify(t => t.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(make, result.Make);
            Assert.Equal(model, result.Model);
            Assert.Equal(registration, result.Registration);
            Assert.Equal(year, result.Year);
            Assert.Equal(odometer, result.Odometer);

        }
    }
}
