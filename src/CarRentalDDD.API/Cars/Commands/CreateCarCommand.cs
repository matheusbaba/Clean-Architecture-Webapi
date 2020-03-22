using AutoMapper;
using CarRentalDDD.Domain.Models.Cars;
using CarRentalDDD.Domain.SeedWork;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CarRentalDDD.API.Cars.Commands
{
    public class CreateCarCommand : IRequest<CarDTO>
    {
        public string Model { get; set; }

        public string Make { get; set; }

        public string Registration { get; set; }

        public int Odometer { get; set; }

        public int Year { get; set; }

        public CreateCarCommand(string model, string make, string registration, int odometer, int year)
        {
            this.Model = model;
            this.Make = make;
            this.Registration = registration;
            this.Odometer = odometer;
            this.Year = year;
        }

        public class Handler : IRequestHandler<CreateCarCommand, CarDTO>
        {
            private readonly IUnitOfWork _uow;
            private readonly ICarRepository _carRepository;
            private readonly IMapper _mapper;
            public Handler(ICarRepository carRepository, IUnitOfWork uow, IMapper mapper)
            {
                _carRepository = carRepository;
                _uow = uow;
                _mapper = mapper;
            }


            public async Task<CarDTO> Handle(CreateCarCommand command, CancellationToken cancellationToken)
            {
                Car car = new Car(command.Model, command.Make, command.Registration, command.Year, command.Odometer);
                _carRepository.Add(car);
                await _uow.CommitAsync(cancellationToken);
                return _mapper.Map<CarDTO>(car);
            }

        }
    }
}
