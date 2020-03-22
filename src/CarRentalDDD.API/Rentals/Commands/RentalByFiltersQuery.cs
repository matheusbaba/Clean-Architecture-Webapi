using AutoMapper;
using CarRentalDDD.Domain.Models.Cars;
using CarRentalDDD.Domain.Models.Customers;
using CarRentalDDD.Domain.Models.Rentals;
using CarRentalDDD.Domain.SeedWork;
using CarRentalDDD.Domain.SeedWork.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

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



        public class Handler : IRequestHandler<RentalByFiltersQuery, IEnumerable<RentalDTO>>
        {
            private readonly IRentalRepository _rentalRepository;
            private readonly ICarRepository _carRepository;
            private readonly ICustomerRepository _customerRepository;
            private readonly IMapper _mapper;

            public Handler(
                IRentalRepository rentalRepository,
                ICarRepository carRepository,
                ICustomerRepository customerRepository,
                IMapper mapper)
            {
                _rentalRepository = rentalRepository;
                _carRepository = carRepository;
                _customerRepository = customerRepository;
                _mapper = mapper;
            }

            public async Task<IEnumerable<RentalDTO>> Handle(RentalByFiltersQuery request, CancellationToken cancellationToken)
            {
                var query = new QueryRepository<Rental>();
                if (request.CustomerId.HasValue)
                    query.AddSpecification(RentalRepositoryHelper.Specifications.ByCustomerId(request.CustomerId.Value));

                if (request.CarId.HasValue)
                {
                    if (query.HasSpecifications)
                        query.AddSpecification(SpecificationType.And, RentalRepositoryHelper.Specifications.ByCustomerId(request.CustomerId.Value));
                    else
                        query.AddSpecification(RentalRepositoryHelper.Specifications.ByCustomerId(request.CustomerId.Value));
                }

                query.AddInclusion(RentalRepositoryHelper.Inclusions.Cars());
                query.AddInclusion(RentalRepositoryHelper.Inclusions.Customers());

                IEnumerable<Rental> rentals = await _rentalRepository.GetAllAsync(query);
                return _mapper.Map<IEnumerable<RentalDTO>>(rentals);
            }


        }
    }
}
