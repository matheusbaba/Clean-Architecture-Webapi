using MediatR;
using System;

namespace CarRentalDDD.API.Customers.Commands
{
    public class CustomerByIdQuery : IRequest<CustomerDTO>
    {
        public Guid Id { get; set; }
        public CustomerByIdQuery(Guid id)
        {
            this.Id = id;
        }
    }
}
