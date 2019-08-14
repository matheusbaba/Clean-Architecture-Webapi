using System;
namespace CarRentalDDD.API.Customers
{
    public class CustomerDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }        
        public string DriverLicense { get; set; }        
        public DateTime DOB { get; set; }        
        public string Street { get; set; }        
        public string City { get; set; }        
        public string ZipCode { get; set; }        
        public string Phone { get; set; }
    }
}
