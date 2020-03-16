using CarRentalDDD.Domain.Models.Cars;
using CarRentalDDD.Domain.Models.Customers;
using CarRentalDDD.Domain.Models.Rentals;
using CarRentalDDD.Infra.Rentals.Repositories;
using CarRentalDDD.Infra.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Linq;
using Xunit;
using CarRentalDDD.Domain.Models.Shared;

namespace CarRentalDDD.Infra.Tests.Repositories.Rentals
{
    public class RentalRepositoryTests
    {
        [Fact]
        public async void Add_ShouldWork()
        {
            var car = new Car("model", "make", "reg", 2000, 5000);
            var customer = new Customer("name", "123123", DateTime.Now.AddYears(-20), "askdoposkad@gmail.com", new Address("street", "city", "123231"), "12378213");

            var rental = new Rental(DateTime.Now.AddDays(-30), null, customer, car);

            var options = new DbContextOptionsBuilder<RentalContext>()
                         .UseInMemoryDatabase(databaseName: "in_memory_db_tests")
                         .Options;

            // Run the test against one instance of the context
            using (var dbContext = new RentalContext(options))
            {
                var mockMediatR = new Mock<IMediator>();
                var uow = new UnitOfWork(dbContext, mockMediatR.Object);

                IRentalRepository rep = new RentalRepository(dbContext);
                rep.Add(rental);
                await uow.CommitAsync(System.Threading.CancellationToken.None);
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var dbContext = new RentalContext(options))
            {
                var inserted = dbContext.Rentals.SingleOrDefault();

                Assert.NotNull(inserted);
                Assert.True(rental.Id == inserted.Id);
            }
        }


        // more....
    }
}
