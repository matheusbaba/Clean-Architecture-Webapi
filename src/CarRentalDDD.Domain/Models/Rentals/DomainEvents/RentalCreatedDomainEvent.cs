using CarRentalDDD.Domain.SeedWork;

namespace CarRentalDDD.Domain.Models.Rentals.DomainEvents
{
    /// <summary>
    /// Domain event to say that a rental was created
    /// </summary>
    public class RentalCreatedDomainEvent : DomainEvent
    {
        public Rental Rental { get; set; }

        public RentalCreatedDomainEvent(Rental rental)
        {
            this.Rental = rental;
        }
        
    }
}
