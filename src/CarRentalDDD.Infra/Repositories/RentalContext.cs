using CarRentalDDD.Domain.Models.Cars;
using CarRentalDDD.Domain.Models.Customers;
using CarRentalDDD.Domain.Models.Rentals;
using CarRentalDDD.Infra.Repositories.Cars;
using CarRentalDDD.Infra.Repositories.Customers;
using CarRentalDDD.Infra.Repositories.Rentals;
using Microsoft.EntityFrameworkCore;

namespace CarRentalDDD.Infra.Repositories
{
    public class RentalContext : DbContext
    {
        public RentalContext(DbContextOptions<RentalContext> options)
           : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CarEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RentalEntityTypeConfiguration());
        }
    }
}
