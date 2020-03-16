using CarRentalDDD.Domain.Models.Cars;
using CarRentalDDD.Domain.SeedWork;
using CarRentalDDD.Infra.Cars.Repositories;
using CarRentalDDD.Infra.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CarRentalDDD.Infra.Tests.Repositories.Cars
{
    public class CarRepositoryTests
    {

        private DbContextOptions<RentalContext> _dbContextOptions => new DbContextOptionsBuilder<RentalContext>()
                         .UseInMemoryDatabase(databaseName: "in_memory_db_tests")
                         .Options;

        private IList<Car> GetCarList() => new List<Car>()
        {
            new Car("model1", "make1", "reg1", 2000, 5000),
            new Car("model2", "make2", "reg2", 2100, 6000),
            new Car("model3", "make3", "reg3", 2200, 7000)
        };

        [Fact]
        public void Add_RunOnlyOnce()
        {
            var dbContextMock = new Mock<RentalContext>();
            ICarRepository rep = new CarRepository(dbContextMock.Object);

            rep.Add(GetCarList()[0]);

            dbContextMock.Verify(mock => mock.Add(It.IsAny<Car>()), Times.Once());
        }

        [Fact]
        public void Remove_RunOnlyOnce()
        {
            var dbContextMock = new Mock<RentalContext>();
            ICarRepository rep = new CarRepository(dbContextMock.Object);

            rep.Remove(GetCarList()[0]);

            dbContextMock.Verify(mock => mock.Remove(It.IsAny<Car>()), Times.Once());
        }

        [Fact]
        public void Update_RunOnlyOnce()
        {
            var dbContextMock = new Mock<RentalContext>();
            ICarRepository rep = new CarRepository(dbContextMock.Object);

            rep.Update(GetCarList()[0]);

            dbContextMock.Verify(mock => mock.Update(It.IsAny<Car>()), Times.Once());
        }


        [Fact]
        public async void FindAllAsync_ShouldWork()
        {
            // Insert seed data into the database using one instance of the context
            using (var dbContext = new RentalContext(_dbContextOptions))
            {
                GetCarList().ToList().ForEach(t => dbContext.Add(t));
                dbContext.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var dbContext = new RentalContext(_dbContextOptions))
            {
                var expected = GetCarList().Count;
                ICarRepository rep = new CarRepository(dbContext);
                var result = await rep.FindAllAsync();
                Assert.Equal(expected, result.Count());
            }
        }


        //[Fact]
        //public async void Add_ShouldWork()
        //{
        //    var car = _cars[0];

        //    // Run the test against one instance of the context
        //    using (var dbContext = new RentalContext(_dbContextOptions))
        //    {
        //        var mockMediatR = new Mock<IMediator>();
        //        mockMediatR.Setup(t => t.Publish(It.IsAny<object>(),It.IsAny<System.Threading.CancellationToken>())).Callback(() => { });
        //        var uow = new UnitOfWork(dbContext, mockMediatR.Object);

        //        ICarRepository rep = new CarRepository(dbContext);

        //        rep.Add(car);
        //        await uow.CommitAsync(System.Threading.CancellationToken.None);
        //    }

        //    // Use a separate instance of the context to verify correct data was saved to database
        //    using (var dbContext = new RentalContext(_dbContextOptions))
        //    {
        //        var inserted = dbContext.Cars.SingleOrDefault();
        //        Assert.NotNull(inserted);
        //        Assert.True(car.Id == inserted.Id);
        //    }
        //}

        //[Fact]
        //public async void FindAllAsync_ShouldWork()
        //{
        //    // Insert data into the database using one instance of the context
        //    using (var context = new RentalContext(_dbContextOptions))
        //    {
        //        for (int i = 0; i < _cars.Count -1; i++)
        //            context.Cars.Add(_cars[i]);                                
        //        context.SaveChanges();
        //    }          

        //    // Use a clean instance of the context to run the test
        //    using (var context = new RentalContext(_dbContextOptions))
        //    {
        //        ICarRepository rep = new CarRepository(context);
        //        var result = await rep.FindAllAsync(new CarsByFiltersSpecification("model1", ""));
        //        Assert.Equal(2, result.Count());
        //    }
        //} 


    }
}
