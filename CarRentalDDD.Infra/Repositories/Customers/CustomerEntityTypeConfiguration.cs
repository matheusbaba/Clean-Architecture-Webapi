using CarRentalDDD.Domain.Models.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRentalDDD.Infra.Repositories.Customers
{
    public class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customers");

            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name).HasColumnName("Name").IsRequired();
            builder.Property(t => t.DriverLicense).HasColumnName("DriverLicense").IsRequired();
            builder.Property(t => t.DOB).HasColumnName("DOB").IsRequired();

            builder.OwnsOne(t => t.Phone, t =>
            {
                t.Property(x => x.Value).HasColumnName("Phone").IsRequired();
            });

            builder.OwnsOne(t => t.Email, t =>
            {
                t.Property(x => x.Value).HasColumnName("Email").IsRequired();
            });

            builder.OwnsOne(t => t.Address, t =>
            {
                t.Property(x => x.Street).HasColumnName("Street").IsRequired();
                t.Property(x => x.City).HasColumnName("City").IsRequired();
                t.Property(x => x.ZipCode).HasColumnName("ZipCode");
            });
        }
    }
}
