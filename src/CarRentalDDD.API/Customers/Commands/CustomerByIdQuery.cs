using AutoMapper;
using CarRentalDDD.Domain.Models.Customers;
using CarRentalDDD.Domain.SeedWork;
using CarRentalDDD.Domain.SeedWork.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CarRentalDDD.API.Customers.Commands
{
    public class CustomerByIdQuery : IRequest<CustomerDTO>
    {
        public Guid Id { get; set; }
        public CustomerByIdQuery(Guid id)
        {
            this.Id = id;
        }

        public class Handler : IRequestHandler<CustomerByIdQuery, CustomerDTO>
        {
            private readonly ICustomerRepository _customerRepository;
            private readonly IMapper _mapper;

            public Handler(ICustomerRepository customerRepository, IMapper mapper)
            {
                _customerRepository = customerRepository;
                _mapper = mapper;
            }
            public async Task<CustomerDTO> Handle(CustomerByIdQuery request, CancellationToken cancellationToken)
            {
                var query = new QueryRepository<Customer>();
                query.AddSpecification(CustomerRepositoryHelper.Specifications.ById(request.Id));
                Customer customer = await _customerRepository.SingleAsync(query);
                return _mapper.Map<CustomerDTO>(customer);
            }


        }
    }
}
