using MediatR;
using System;

namespace CarRentalDDD.API.Cars.Commands
{
    public class CreateMaintenanceCommand : IRequest<MaintenanceDTO>
    {        
        public Guid CarId { get; set; }        
        public DateTime Date { get; set; }        
        public string Service { get; set; }
        public string Description { get; set; }

        public CreateMaintenanceCommand(Guid carId, DateTime date, string service, string description)
        {
            this.CarId = carId;
            this.Date = date;
            this.Service = service;
            this.Description = description;
        }
    }
}
