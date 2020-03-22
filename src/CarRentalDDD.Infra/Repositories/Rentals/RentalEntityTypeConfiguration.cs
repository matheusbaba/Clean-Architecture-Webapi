using CarRentalDDD.Domain.Models.Rentals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

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
            builder.Property<Guid>("FK_CustomerId");
            builder.HasOne(t => t.Customer).WithMany(t => t.Rentals).HasForeignKey("FK_CustomerId");
            builder.Property<Guid>("FK_CarId");
            builder.HasOne(t => t.Car).WithMany(t => t.Rentals).HasForeignKey("FK_CarId");
        }
    }
}
