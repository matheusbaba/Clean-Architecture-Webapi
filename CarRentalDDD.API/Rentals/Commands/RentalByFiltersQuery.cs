using MediatR;
using System;
using System.Collections.Generic;

namespace CarRentalDDD.API.Rentals.Commands
{
    public class RentalByFiltersQuery : IRequest<IEnumerable<RentalDTO>>
    {
        public Guid? CarId { get; set; }
        public Guid? CustomerId { get; set; }

        public RentalByFiltersQuery(Guid? carId, Guid? customerId)
        {
            this.CarId = carId;
            this.CustomerId = customerId;
        }
    }
}
