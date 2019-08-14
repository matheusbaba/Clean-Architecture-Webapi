using AutoMapper;
using CarRentalDDD.API.Cars.Commands;
using CarRentalDDD.Domain.Models.Cars;
using CarRentalDDD.Domain.SeedWork;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CarRentalDDD.API.Cars
{
    public class CarCommandHandler : IRequestHandler<CreateCarCommand, CarDTO>
        , IRequestHandler<CarByIdQuery, CarWithMaintenancesDTO>
        , IRequestHandler<CarByFiltersQuery, IEnumerable<CarDTO>>
        , IRequestHandler<CreateMaintenanceCommand, MaintenanceDTO>
    {
        private readonly IUnitOfWork _uow;
        private readonly ICarRepository _carRepository;
        private readonly IMapper _mapper;
        public CarCommandHandler(
            ICarRepository carRepository,
            IUnitOfWork uow,
            IMapper mapper)
        {
            _carRepository = carRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CarDTO>> Handle(CarByFiltersQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Car> cars = await _carRepository.FindAllAsync(new CarsByFiltersSpecification(request.Model ?? string.Empty, request.Make ?? string.Empty));
            return _mapper.Map<IEnumerable<CarDTO>>(cars);
        }

        public async Task<CarDTO> Handle(CreateCarCommand request, CancellationToken cancellationToken)
        {
            Car car = new Car(request.Model, request.Make, request.Registration, request.Year, request.Odmometer);
            _carRepository.Add(car);
            await _uow.CommitAsync(cancellationToken);
            return _mapper.Map<CarDTO>(car);
        }

        public async Task<MaintenanceDTO> Handle(CreateMaintenanceCommand request, CancellationToken cancellationToken)
        {
            Car car = await _carRepository.FindOneAsync(new CarByIdWithMaintenancesSpecification(request.CarId));
            Maintenance maintenance = new Maintenance(request.Date, request.Service, request.Description);
            car.AddMaintenance(maintenance);
            _carRepository.Update(car);
            await _uow.CommitAsync(cancellationToken);
            return _mapper.Map<MaintenanceDTO>(maintenance);
        }

        public async Task<CarWithMaintenancesDTO> Handle(CarByIdQuery request, CancellationToken cancellationToken)
        {
            Car car = await _carRepository.FindOneAsync(new CarByIdWithMaintenancesSpecification(request.Id));
            return _mapper.Map<CarWithMaintenancesDTO>(car);
        }

    }
}
