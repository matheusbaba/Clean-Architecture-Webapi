using AutoMapper;
using CarRentalDDD.API.Rentals.Commands;
using CarRentalDDD.Domain.Models.Cars;
using CarRentalDDD.Domain.Models.Customers;
using CarRentalDDD.Domain.Models.Rentals;
using CarRentalDDD.Domain.SeedWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CarRentalDDD.API.Rentals
{
    public class RentalCommandHandler
        : IRequestHandler<CreateRentalCommand, RentalDTO>
        , IRequestHandler<RentalByFiltersQuery, IEnumerable<RentalDTO>>
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

        public async Task<IEnumerable<RentalDTO>> Handle(RentalByFiltersQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Rental> rentals = await _rentalRepository.FindAllAsync(new RentalsByFiltersSpecification(request.CustomerId, request.CarId));
            return _mapper.Map<IEnumerable<RentalDTO>>(rentals);
        }

        public async Task<RentalDTO> Handle(CreateRentalCommand request, CancellationToken cancellationToken)
        {
            Car car = await _carRepository.FindOneAsync(new CarByIdSpecification(request.CarId));
            Customer customer = await _customerRepository.FindOneAsync(new CustomerByIdSpecification(request.CustomerId));
            Rental rental = new Rental(request.PickUpDate, request.DropOffDate, customer, car);
            _rentalRepository.Add(rental);
            await _uow.CommitAsync(cancellationToken);
            return _mapper.Map<RentalDTO>(rental);
        }
    }
}
