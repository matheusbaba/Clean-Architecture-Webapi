using MediatR;
using System;

namespace CarRentalDDD.API.Cars.Commands
{
    public class CarByIdQuery : IRequest<CarWithMaintenancesDTO>
    {
        public Guid Id { get; set; }
        public CarByIdQuery(Guid id)
        {
            this.Id = id;
        }
    }
}
