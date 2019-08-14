using CarRentalDDD.Domain.SeedWork;

namespace CarRentalDDD.Domain.Models.Rentals.DomainEvents
{
    public class RentalCreatedDomainEvent : DomainEvent
    {
        public Rental Rental { get; set; }

        public RentalCreatedDomainEvent(Rental rental)
        {
            this.Rental = rental;
        }
        
    }
}
