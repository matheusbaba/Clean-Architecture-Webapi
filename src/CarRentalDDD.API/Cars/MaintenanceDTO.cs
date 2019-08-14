using System;

namespace CarRentalDDD.API.Cars
{
    public class MaintenanceDTO
    {
        public Guid Id { get; set; }
        public DateTime Date { get; private set; }
        public string Service { get; private set; }
        public string Description { get; private set; }
    }
}
