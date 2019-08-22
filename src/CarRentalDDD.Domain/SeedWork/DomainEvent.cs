using System;

namespace CarRentalDDD.Domain.SeedWork
{
    public class DomainEvent : IDomainEvent
    {
        public DateTime CreatedAt { get; } = DateTime.Now;
    }
}
