using AutoMapper;
using CarRentalDDD.Domain.Models.Customers;
using CarRentalDDD.Domain.SeedWork;
using CarRentalDDD.Domain.SeedWork.Repository;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CarRentalDDD.API.Customers.Commands
{
    public class CustomerByFiltersQuery : IRequest<IEnumerable<CustomerDTO>>
    {
        public string Name { get; set; }
        public string DriverLicense { get; set; }

        public CustomerByFiltersQuery(string name, string driverLicense)
        {
            this.Name = name ?? string.Empty;
            this.DriverLicense = driverLicense ?? string.Empty;
        }


        public class Handler : IRequestHandler<CustomerByFiltersQuery, IEnumerable<CustomerDTO>>

        {
            private readonly ICustomerRepository _customerRepository;
            private readonly IMapper _mapper;

            public Handler(ICustomerRepository customerRepository, IMapper mapper)
            {
                _customerRepository = customerRepository;
                _mapper = mapper;
            }



            public async Task<IEnumerable<CustomerDTO>> Handle(CustomerByFiltersQuery request, CancellationToken cancellationToken)
            {
                var query = new QueryRepository<Customer>();
                query.AddSpecification(CustomerRepositoryHelper.Specifications.ByName(request.Name));
                query.AddSpecification(SpecificationType.And, CustomerRepositoryHelper.Specifications.ByDriverLicense(request.DriverLicense));
                IEnumerable<Customer> customers = await _customerRepository.FindAllAsync(query);
                return _mapper.Map<IEnumerable<CustomerDTO>>(customers);
            }
        }
    }
}
