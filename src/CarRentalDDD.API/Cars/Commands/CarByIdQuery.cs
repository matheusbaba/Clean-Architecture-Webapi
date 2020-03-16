using AutoMapper;
using CarRentalDDD.Domain.Models.Cars;
using CarRentalDDD.Domain.SeedWork.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace CarRentalDDD.API.Cars.Commands
{
    public class CarByIdQuery : IRequest<CarWithMaintenancesDTO>
    {
        public Guid Id { get; set; }
        public CarByIdQuery(Guid id)
        {
            this.Id = id;
        }
        

        public class Handler : IRequestHandler<CarByIdQuery, CarWithMaintenancesDTO>
        {
            private readonly ICarRepository _carRepository;
            private readonly IMapper _mapper;
            public Handler(ICarRepository carRepository, IMapper mapper)
            {
                _carRepository = carRepository;
                _mapper = mapper;
            }


            public async Task<CarWithMaintenancesDTO> Handle(CarByIdQuery request, CancellationToken cancellationToken)
            {
                var query = new QueryRepository<Car>();
                query.AddSpecification(CarRepositoryHelper.Specifications.ById(request.Id));
                Car car = await _carRepository.SingleAsync(query);
                return _mapper.Map<CarWithMaintenancesDTO>(car);
            }

        }
    }
}
