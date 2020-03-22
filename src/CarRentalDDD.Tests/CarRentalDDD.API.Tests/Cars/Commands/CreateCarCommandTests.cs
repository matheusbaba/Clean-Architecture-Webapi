using AutoMapper;
using CarRentalDDD.API.Cars.Commands;
using CarRentalDDD.API.Mappings;
using CarRentalDDD.Domain.Models.Cars;
using CarRentalDDD.Domain.SeedWork;
using CarRentalDDD.Infra.Cars.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace CarRentalDDD.API.Tests.Cars.Commands
{
    public class CreateCarCommandTests
    {
        private IMapper _mapper;

        public CreateCarCommandTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Mapping>();
            });
            _mapper = new Mapper(config);
        }

        [Theory]
        [InlineData("make1", "model1", "123213", 2000, 0)]
        [InlineData("make2", "model2", "555555", 2020, 50000)]
        public void Handler_ShouldWork(string make, string model, string registration, int year, int odometer)
        {
            var createCarCommand = new CreateCarCommand(model, make, registration, odometer, year);
            var carRepositoryMock = new Mock<ICarRepository>();
            carRepositoryMock.Setup(t => t.Add(It.IsAny<Car>())).Verifiable();
            var uowMock = new Mock<IUnitOfWork>();
            uowMock.Setup(t => t.CommitAsync(It.IsAny<CancellationToken>())).Verifiable();

            var handler = new CreateCarCommand.Handler(carRepositoryMock.Object, uowMock.Object, _mapper);
            var result = handler.Handle(createCarCommand, new CancellationToken()).Result;

            Assert.Equal(make, result.Make);
            Assert.Equal(model, result.Model);
            Assert.Equal(registration, result.Registration);
            Assert.Equal(year, result.Year);
            Assert.Equal(odometer, result.Odometer);

        }
    }
}
