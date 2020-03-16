using CarRentalDDD.Domain.Models.Rentals;
using CarRentalDDD.Infra.Repositories;

namespace CarRentalDDD.Infra.Rentals.Repositories
{
    public class RentalRepository : RepositoryBase<Rental>, IRentalRepository
    {
        public RentalRepository(RentalContext context) : base(context)
        {
        }
    }
}
