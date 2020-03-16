using AutoMapper;
using CarRentalDDD.Domain.Models.Cars;
using CarRentalDDD.Domain.SeedWork;
using CarRentalDDD.Domain.SeedWork.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CarRentalDDD.API.Cars.Commands
{
    public class CreateMaintenanceCommand : IRequest<MaintenanceDTO>
    {
        public Guid CarId { get; set; }
        public DateTime Date { get; set; }
        public string Service { get; set; }
        public string Description { get; set; }

        public CreateMaintenanceCommand(Guid carId, DateTime date, string service, string description)
        {
            this.CarId = carId;
            this.Date = date;
            this.Service = service;
            this.Description = description;
        }



        public class CarCommandHandler : IRequestHandler<CreateMaintenanceCommand, MaintenanceDTO>
        {
            private readonly IUnitOfWork _uow;
            private readonly ICarRepository _carRepository;
            private readonly IMapper _mapper;
            public CarCommandHandler(ICarRepository carRepository, IUnitOfWork uow, IMapper mapper)
            {
                _carRepository = carRepository;
                _uow = uow;
                _mapper = mapper;
            }

            public async Task<MaintenanceDTO> Handle(CreateMaintenanceCommand request, CancellationToken cancellationToken)
            {
                var query = new QueryRepository<Car>();
                query.AddSpecification(CarRepositoryHelper.Specifications.ById(request.CarId));
                Car car = await _carRepository.SingleAsync(query);
                Maintenance maintenance = new Maintenance(request.Date, request.Service, request.Description);
                car.AddMaintenance(maintenance);
                _carRepository.Update(car);
                await _uow.CommitAsync(cancellationToken);
                return _mapper.Map<MaintenanceDTO>(maintenance);
            }


        }
    }
}
