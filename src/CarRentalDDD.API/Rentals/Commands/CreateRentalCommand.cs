using AutoMapper;
using CarRentalDDD.Domain.Models.Cars;
using CarRentalDDD.Domain.Models.Customers;
using CarRentalDDD.Domain.Models.Rentals;
using CarRentalDDD.Domain.SeedWork;
using CarRentalDDD.Domain.SeedWork.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

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



        public class RentalCommandHandler: IRequestHandler<CreateRentalCommand, RentalDTO>
        {
            private readonly IUnitOfWork _uow;
            private readonly IRentalRepository _rentalRepository;
            private readonly ICarRepository _carRepository;
            private readonly ICustomerRepository _customerRepository;
            private readonly IMapper _mapper;

            public RentalCommandHandler(
                IUnitOfWork uow,
                IRentalRepository rentalRepository,
                ICarRepository carRepository,
                ICustomerRepository customerRepository,
                IMapper mapper)
            {
                _uow = uow;
                _rentalRepository = rentalRepository;
                _carRepository = carRepository;
                _customerRepository = customerRepository;
                _mapper = mapper;
            }

            public async Task<RentalDTO> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
            {                
                var carQuery = new QueryRepository<Car>();
                carQuery.AddSpecification(CarRepositoryHelper.Specifications.ById(request.CarId));
                Car car = await _carRepository.SingleAsync(carQuery);

                var customerQuery = new QueryRepository<Customer>();
                customerQuery.AddSpecification(CustomerRepositoryHelper.Specifications.ById(request.CustomerId));
                Customer customer = await _customerRepository.SingleAsync(customerQuery);

                Rental rental = new Rental(request.PickUpDate, request.DropOffDate, customer, car);
                _rentalRepository.Add(rental);
                await _uow.CommitAsync(cancellationToken);
                return _mapper.Map<RentalDTO>(rental);
            }
        }
    }
}
