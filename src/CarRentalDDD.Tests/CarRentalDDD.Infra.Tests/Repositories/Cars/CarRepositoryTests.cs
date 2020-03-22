using CarRentalDDD.Domain.Models.Cars;
using CarRentalDDD.Domain.SeedWork.Repository;
using CarRentalDDD.Infra.Cars.Repositories;
using CarRentalDDD.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CarRentalDDD.Infra.Tests.Repositories.Cars
{
    public class CarRepositoryTests
    {

        IList<Car> _cars;

        public CarRepositoryTests()
        {
            _cars = new List<Car>();
            for (int i = 1; i <= 5; i++)
                _cars.Add(new Car("model" + i, "make" + i, "reg" + i, 2000, 2000));
        }


        [Fact]
        public void Add_ShouldWork()
        {
            // Arrange
            var car = _cars[0];
            var dbContextMock = new Mock<RentalContext>();
            var dbSetMock = new Mock<DbSet<Car>>();
            dbContextMock.Setup(t => t.Set<Car>()).Returns(dbSetMock.Object);

            // Act
            var carRepository = new CarRepository(dbContextMock.Object);
            carRepository.Add(car);

            // Assert
            dbContextMock.Verify(t => t.Set<Car>(), Times.Once);
            dbSetMock.Verify(t => t.Add(It.IsAny<Car>()), Times.Once);
        }

        [Fact]
        public void Update_ShouldWork()
        {
            // Arrange
            var car = _cars[0];

            var dbContextMock = new Mock<RentalContext>();
            var dbSetMock = new Mock<DbSet<Car>>();
            dbContextMock.Setup(x => x.Set<Car>()).Returns(dbSetMock.Object);

            // Act
            var carRepository = new CarRepository(dbContextMock.Object);
            carRepository.Update(car);

            // Assert
            dbContextMock.Verify(x => x.Set<Car>(), Times.Once);
            dbSetMock.Verify(mock => mock.Update(It.IsAny<Car>()), Times.Once());
        }

        [Fact]
        public void Remove_ShouldWork()
        {
            // Arrange
            var car = _cars[0];
            var dbContextMock = new Mock<RentalContext>();
            var dbSetMock = new Mock<DbSet<Car>>();
            dbContextMock.Setup(t => t.Set<Car>()).Returns(dbSetMock.Object);

            // Act
            var carRepository = new CarRepository(dbContextMock.Object);
            carRepository.Remove(car);

            // Assert
            dbContextMock.Verify(t => t.Set<Car>(), Times.Once);
            dbSetMock.Verify(t => t.Remove(It.IsAny<Car>()), Times.Once);
        }


        [Fact]
        public async void GetAllAsync_ShouldWork()
        {
            // Arrange
            var dbContextMock = new Mock<RentalContext>();
            var mockDbSet = _cars.AsQueryable().BuildMockDbSet();
            dbContextMock.Setup(x => x.Set<Car>()).Returns(mockDbSet.Object);

            // Act
            var carRepository = new CarRepository(dbContextMock.Object);
            var result = await carRepository.GetAllAsync();

            // Assert
            dbContextMock.Verify(x => x.Set<Car>(), Times.Once);
            Assert.Equal(_cars.Count, result.Count());
        }

        [Fact]
        public async void FirstAsync_ShouldReturnFirstOne()
        {
            // Arrange
            var expected = _cars[0];
            var dbContextMock = new Mock<RentalContext>();
            var mockDbSet = _cars.AsQueryable().BuildMockDbSet();
            dbContextMock.Setup(x => x.Set<Car>()).Returns(mockDbSet.Object);

            // Act
            var carRepository = new CarRepository(dbContextMock.Object);
            var result = await carRepository.FirstAsync();

            // Assert
            dbContextMock.Verify(x => x.Set<Car>(), Times.Once);            
            Assert.Equal(expected, result);
        }


        [Fact]
        public async void FirstAsync_ShouldReturnNull()
        {
            // Arrange
            var dbContextMock = new Mock<RentalContext>();
            var mockDbSet = _cars.AsQueryable().BuildMockDbSet();
            dbContextMock.Setup(x => x.Set<Car>()).Returns(mockDbSet.Object);

            // Act
            var carRepository = new CarRepository(dbContextMock.Object);
            var query = new QueryRepository<Car>();
            query.AddSpecification(CarRepositoryHelper.Specifications.ByMake("NOTEXIST"));
            var result = await carRepository.FirstAsync(query);

            // Assert
            dbContextMock.Verify(x => x.Set<Car>(), Times.Once);
            Assert.Null(result);
        }


        [Fact]
        public async void SingleAsync_ShouldReturnOne()
        {
            // Arrange
            var dbContextMock = new Mock<RentalContext>();
            var mockDbSet = _cars.AsQueryable().BuildMockDbSet();
            dbContextMock.Setup(x => x.Set<Car>()).Returns(mockDbSet.Object);

            // Act
            var carRepository = new CarRepository(dbContextMock.Object);
            var query = new QueryRepository<Car>();
            query.AddSpecification(CarRepositoryHelper.Specifications.ByMake("make1"));
            var result = await carRepository.SingleAsync(query);

            // Assert
            dbContextMock.Verify(x => x.Set<Car>(), Times.Once);
            Assert.NotNull(result);
        }

        [Fact]
        public async void SingleAsync_ShouldReturnNull()
        {
            // Arrange
            var dbContextMock = new Mock<RentalContext>();
            var mockDbSet = _cars.AsQueryable().BuildMockDbSet();
            dbContextMock.Setup(x => x.Set<Car>()).Returns(mockDbSet.Object);

            // Act
            var carRepository = new CarRepository(dbContextMock.Object);
            var query = new QueryRepository<Car>();
            query.AddSpecification(CarRepositoryHelper.Specifications.ByMake("NOTEXIST"));
            var result = await carRepository.SingleAsync(query);

            // Assert
            dbContextMock.Verify(x => x.Set<Car>(), Times.Once);
            Assert.Null(result);
        }



        [Fact]
        public void SingleAsync_ReturnsMoreThanOne_ThrowsException()
        {
            // Arrange
            var dbContextMock = new Mock<RentalContext>();
            var mockDbSet = _cars.AsQueryable().BuildMockDbSet();
            dbContextMock.Setup(x => x.Set<Car>()).Returns(mockDbSet.Object);

            // Act
            var carRepository = new CarRepository(dbContextMock.Object);
            var query = new QueryRepository<Car>();
            query.AddSpecification(CarRepositoryHelper.Specifications.ByMake("make"));
            Car result = null;
            Assert.ThrowsAsync<System.Exception>(async () => result = await carRepository.SingleAsync(query));

            // Assert
            dbContextMock.Verify(x => x.Set<Car>(), Times.Once);
            
        }


    }
}
