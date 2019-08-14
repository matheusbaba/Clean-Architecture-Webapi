using MediatR;
using System;

namespace CarRentalDDD.API.Rentals.Commands
{
    public class CreateRentalCommand : IRequest<RentalDTO>
    {
        public DateTime PickUpDate { get; set; }
        public DateTime DropOffDate { get; set; }
        public Guid CustomerId { get; set; }
        public Guid CarId { get; set; }

        public CreateRentalCommand(DateTime pickupDate, DateTime dropoffDate, Guid customerId, Guid carId)
        {
            this.PickUpDate = pickupDate;
            this.DropOffDate = dropoffDate;
            this.CustomerId = customerId;
            this.CarId = carId;
        }
    }
}
