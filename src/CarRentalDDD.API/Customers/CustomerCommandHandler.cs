using AutoMapper;
using CarRentalDDD.API.Customers.Commands;
using CarRentalDDD.Domain.Models.Customers;
using CarRentalDDD.Domain.Models.Shared;
using CarRentalDDD.Domain.SeedWork;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace CarRentalDDD.API.Customers
{
    public class CustomerCommandHandler
        : IRequestHandler<CreateCustomerCommand, CustomerDTO>
        , IRequestHandler<CustomerByIdQuery, CustomerDTO>
        , IRequestHandler<CustomerByFiltersQuery, IEnumerable<CustomerDTO>>

    {
        private readonly IUnitOfWork _uow;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerCommandHandler(ICustomerRepository customerRepository, IUnitOfWork uow, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _uow = uow;
            _mapper = mapper;
        }


        public async Task<CustomerDTO> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            Address address = new Address(request.Street, request.City, request.ZipCode);
            Phone phone = new Phone(request.Phone);
            Email email = Email.FromString(request.Email);
            Customer customer = new Customer(request.Name, request.DriverLicense, request.DOB, email, address, phone);
            _customerRepository.Add(customer);
            await _uow.CommitAsync(cancellationToken);
            return _mapper.Map<CustomerDTO>(customer);
        }

        public async Task<CustomerDTO> Handle(CustomerByIdQuery request, CancellationToken cancellationToken)
        {
            Customer customer = await _customerRepository.FindOneAsync(new CustomerByIdSpecification(request.Id));
            return _mapper.Map<CustomerDTO>(customer);
        }

        public async Task<IEnumerable<CustomerDTO>> Handle(CustomerByFiltersQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Customer> customers = await _customerRepository.FindAllAsync(new CustomersByFiltersSpecification(request.Name ?? string.Empty, request.DriverLicense ?? string.Empty));
            return _mapper.Map<IEnumerable<CustomerDTO>>(customers);
        }
    }
}
