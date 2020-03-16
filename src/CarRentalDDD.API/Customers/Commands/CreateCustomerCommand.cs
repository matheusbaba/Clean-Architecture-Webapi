using AutoMapper;
using CarRentalDDD.Domain.Models.Customers;
using CarRentalDDD.Domain.Models.Shared;
using CarRentalDDD.Domain.SeedWork;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CarRentalDDD.API.Customers.Commands
{
    public class CreateCustomerCommand : IRequest<CustomerDTO>
    {
        public string Name { get; set; }
        public string DriverLicense { get; set; }
        public DateTime DOB { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public CreateCustomerCommand(string name, string driverLicense, DateTime DOB, string street, string city, string zipCode, string email, string phone)
        {
            this.Name = name;
            this.DriverLicense = driverLicense;
            this.DOB = DOB;
            this.Street = street;
            this.City = city;
            this.ZipCode = zipCode;
            this.Phone = phone;
            this.Email = email;
        }


        public class Handler : IRequestHandler<CreateCustomerCommand, CustomerDTO>
        {
            private readonly IUnitOfWork _uow;
            private readonly ICustomerRepository _customerRepository;
            private readonly IMapper _mapper;

            public Handler(ICustomerRepository customerRepository, IUnitOfWork uow, IMapper mapper)
            {
                _customerRepository = customerRepository;
                _uow = uow;
                _mapper = mapper;
            }
            
            public async Task<CustomerDTO> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
            {
                Address address = new Address(request.Street, request.City, request.ZipCode);
                Phone phone = request.Phone;
                Email email = request.Email;
                Customer customer = new Customer(request.Name, request.DriverLicense, request.DOB, email, address, phone);
                _customerRepository.Add(customer);
                await _uow.CommitAsync(cancellationToken);
                return _mapper.Map<CustomerDTO>(customer);
            }

        }
    }

}
