using CarRentalDDD.Domain.Models.Cars;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRentalDDD.Infra.Repositories.Cars
{
    public class CarEntityTypeConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.ToTable("Cars");

            builder.HasKey(t => t.Id);
            builder.Property(t => t.Model).HasColumnName("Model").IsRequired();
            builder.Property(t => t.Make).HasColumnName("Make").IsRequired();
            builder.Property(t => t.Registration).HasColumnName("Registration").IsRequired();
            builder.Property(t => t.Odometer).HasColumnName("Odometer").IsRequired();
            builder.Property(t => t.Year).HasColumnName("Year").IsRequired();
            builder.OwnsMany(t => t.Maintenances, x =>
            {
                x.ToTable("Maintenances");
                x.HasKey(z => z.Id);
                x.Property(t => t.Date).HasColumnName("Date").IsRequired();
                x.Property(t => t.Service).HasColumnName("Service").IsRequired();
                x.Property(t => t.Description).HasColumnName("Description");
                x.HasForeignKey("CarId");
            });
        }
    }
}