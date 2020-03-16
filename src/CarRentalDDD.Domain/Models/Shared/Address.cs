using CarRentalDDD.Domain.SeedWork;

namespace CarRentalDDD.Domain.Models.Shared
{
    public class Address
    {
        public string Street { get; private set; }
        public string City { get; private set; }
        public string ZipCode { get; private set; }

        public Address(string street, string city, string zipCode)
        {            
            if (string.IsNullOrEmpty(street))
                throw new OArgumentNullException(nameof(Street));

            if (string.IsNullOrEmpty(city))
                throw new OArgumentNullException(nameof(City));

            if (string.IsNullOrEmpty(zipCode))
                throw new OArgumentNullException(nameof(ZipCode));

            this.Street = street;
            this.City = city;
            this.ZipCode = zipCode;
        }

        public override string ToString()
        {
            return $"{Street}, {City} - {ZipCode}";
        }
    }
}
