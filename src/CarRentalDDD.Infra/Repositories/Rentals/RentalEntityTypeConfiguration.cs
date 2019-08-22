using CarRentalDDD.Domain.Models.Rentals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRentalDDD.Infra.Repositories.Rentals
{
    public class RentalEntityTypeConfiguration : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> builder)
        {
            builder.ToTable("Rentals");

            builder.HasKey(t => t.Id);
            builder.Property(t => t.PickUpDate).HasColumnName("PickUpDate").IsRequired();
            builder.Property(t => t.DropOffDate).HasColumnName("DropOffDate").IsRequired();            
            builder.HasOne(t => t.Customer).WithMany().HasForeignKey("CustomerId");
            builder.HasOne(t => t.Car).WithMany().HasForeignKey("CarId");
        }
    }
}
