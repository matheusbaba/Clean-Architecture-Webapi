using System;
using System.Collections.Generic;

namespace CarRentalDDD.API.Cars
{
    public class CarWithMaintenancesDTO
    {
        public Guid Id { get; set; }
        public string Model { get; set; }
        public string Make { get; set; }
        public string Registration { get; set; }
        public int Odometer { get; set; }
        public int Year { get; set; }

        public IEnumerable<MaintenanceDTO> Maintenances { get; private set; }
    }
}
