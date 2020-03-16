using CarRentalDDD.Domain.Models.Cars;
using CarRentalDDD.Infra.Repositories;

namespace CarRentalDDD.Infra.Cars.Repositories
{
    public class CarRepository : RepositoryBase<Car>, ICarRepository
    {
        public CarRepository(RentalContext context) : base(context)
        {
        }
    }
}
