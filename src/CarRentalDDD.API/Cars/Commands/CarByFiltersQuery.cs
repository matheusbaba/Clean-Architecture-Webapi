using AutoMapper;
using CarRentalDDD.Domain.Models.Cars;
using CarRentalDDD.Domain.SeedWork.Repository;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CarRentalDDD.API.Cars.Commands
{
    public class CarByFiltersQuery : IRequest<IEnumerable<CarDTO>>
    {
        public string Model { get; set; }
        public string Make { get; set; }

        public CarByFiltersQuery(string model, string make)
        {
            this.Model = model ?? string.Empty;
            this.Make = make ?? string.Empty;
        }


        public class Handler : IRequestHandler<CarByFiltersQuery, IEnumerable<CarDTO>>
        {
            private readonly ICarRepository _carRepository;
            private readonly IMapper _mapper;
            public Handler(
                ICarRepository carRepository, IMapper mapper)
            {
                _carRepository = carRepository;
                _mapper = mapper;
            }

            public async Task<IEnumerable<CarDTO>> Handle(CarByFiltersQuery request, CancellationToken cancellationToken)
            {
                var query = new QueryRepository<Car>();
                query.AddSpecification(CarRepositoryHelper.Specifications.ByModel(request.Model));
                query.AddSpecification(SpecificationType.Or, CarRepositoryHelper.Specifications.ByMake(request.Make));
                query.AddInclusion(CarRepositoryHelper.Inclusions.Maintenances());

                IEnumerable<Car> cars = await _carRepository.FindAllAsync(query);
                return _mapper.Map<IEnumerable<CarDTO>>(cars);
            }
        }
    }
}
