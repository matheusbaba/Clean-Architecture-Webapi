using CarRentalDDD.API.Cars;
using CarRentalDDD.API.Cars.Commands;
using CarRentalDDD.API.Cars.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CarRentalDDD.API.Tests.Cars
{
    using Microsoft.Extensions.Logging;
    using System;
    public class CarControllerTests : ControllerTestsBase
    {

        [Fact]
        public async void Get_ById_ShouldReturnCar()
        {
            var expected = new CarWithMaintenancesDTO();
            var mediatorMock = new Mock<IMediator>();
            var logger = new Mock<ILogger<CarController>>();
            var controller = new CarController(mediatorMock.Object, logger.Object);
            mediatorMock.Setup(t => t.Send(It.IsAny<CarByIdQuery>(), It.IsAny<CancellationToken>()))
                .Returns(() => Task.FromResult(expected));

            var response = await controller.Get(expected.Id);
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var car = Assert.IsAssignableFrom<CarWithMaintenancesDTO>(okResult.Value);
            Assert.Equal(expected, car);
        }

        [Fact]
        public async void Get_ById_ShouldReturnNotFound()
        {
            CarWithMaintenancesDTO car = null;
            var mediatorMock = new Mock<IMediator>();
            var logger = new Mock<ILogger<CarController>>();
            var controller = new CarController(mediatorMock.Object, logger.Object);

            mediatorMock.Setup(t => t.Send(It.IsAny<CarByIdQuery>(), It.IsAny<CancellationToken>()))
                .Returns(() => Task.FromResult(car));

            var response = await controller.Get(Guid.NewGuid());
            Assert.IsType<NotFoundResult>(response.Result);
        }

        [Fact]
        public async void Get_ByFilters_ShouldReturnListofCars()
        {
            IEnumerable<CarDTO> expected = new List<CarDTO>{
                new CarDTO()
            };
            var mediatorMock = new Mock<IMediator>();
            var logger = new Mock<ILogger<CarController>>();
            var controller = new CarController(mediatorMock.Object, logger.Object);

            mediatorMock.Setup(t => t.Send(It.IsAny<CarByFiltersQuery>(), It.IsAny<CancellationToken>()))
                .Returns(() => Task.FromResult(expected));

            var response = await controller.Get("make", "model");
            var okResult = Assert.IsType<OkObjectResult>(response.Result);
            var cars = Assert.IsAssignableFrom<IEnumerable<CarDTO>>(okResult.Value);
            Assert.Equal(expected, cars);
        }

        [Fact]
        public async void Post_CreateCar_ShouldWork()
        {
            var expected = new CarDTO();
            var createCarRequest = new CreateCarRequest()
            {
                Make = "Make",
                Model = "Model",
                Registration = "Reg",
                Year = 2000
            };
            var mediatorMock = new Mock<IMediator>();
            var logger = new Mock<ILogger<CarController>>();
            var controller = new CarController(mediatorMock.Object, logger.Object);
            mediatorMock.Setup(t => t.Send(It.IsAny<CreateCarCommand>(), It.IsAny<CancellationToken>()))
                .Returns(() => Task.FromResult(expected));

            ValidateModelState(createCarRequest, controller);
            var response = await controller.Post(createCarRequest);

            var okResult = Assert.IsType<CreatedResult>(response);
            var car = Assert.IsAssignableFrom<CarDTO>(okResult.Value);
            Assert.Equal(expected, car);
        }

        [Fact]        
        public async void Post_CreateCar_ShouldReturnBadRequest()
        {
            var createCarRequest = new CreateCarRequest();
            var mediatorMock = new Mock<IMediator>();
            var logger = new Mock<ILogger<CarController>>();
            var controller = new CarController(mediatorMock.Object, logger.Object);

            ValidateModelState(createCarRequest, controller);
            var response = await controller.Post(createCarRequest);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(response);
            var errors = Assert.IsAssignableFrom<SerializableError>(badRequestResult.Value);
            Assert.True(errors.ContainsKey(nameof(createCarRequest.Make)));
            Assert.True(errors.ContainsKey(nameof(createCarRequest.Model)));
            Assert.True(errors.ContainsKey(nameof(createCarRequest.Registration)));
            Assert.True(errors.ContainsKey(nameof(createCarRequest.Year)));
        }

   
        [Fact]
        public async void Post_CreateMaintenance_ShouldWork()
        {
            var expected = new MaintenanceDTO();
            var createMaintenanceRequest = new CreateMaintenanceRequest()
            {
                Date = DateTime.Now,
                Service = "service"
            };
        
            var mediatorMock = new Mock<IMediator>();
            var logger = new Mock<ILogger<CarController>>();
            var controller = new CarController(mediatorMock.Object, logger.Object);
            mediatorMock.Setup(t => t.Send(It.IsAny<CreateMaintenanceCommand>(), It.IsAny<CancellationToken>()))
                .Returns(() => Task.FromResult(expected));

            ValidateModelState(createMaintenanceRequest, controller);
            var response = await controller.Post(Guid.NewGuid(), createMaintenanceRequest);

            var okResult = Assert.IsType<CreatedResult>(response);
            var maintenance = Assert.IsAssignableFrom<MaintenanceDTO>(okResult.Value);
            Assert.Equal(expected, maintenance);
        }

        [Fact]
        public async void Post_CreateMaintenance_ShouldReturnBadRequest()
        {
            var createMaintenanceRequest = new CreateMaintenanceRequest();
            var mediatorMock = new Mock<IMediator>();
            var logger = new Mock<ILogger<CarController>>();
            var controller = new CarController(mediatorMock.Object, logger.Object);

            ValidateModelState(createMaintenanceRequest, controller);
            var response = await controller.Post(Guid.NewGuid(), createMaintenanceRequest);

            var badRequestResult = Assert.IsType<BadRequestObjectResult>(response);
            var errors = Assert.IsAssignableFrom<SerializableError>(badRequestResult.Value);
            Assert.True(errors.ContainsKey(nameof(createMaintenanceRequest.Service)));            
        }
    }
}
