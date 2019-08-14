using MediatR;
using System;

namespace CarRentalDDD.Domain.SeedWork
{
    public interface IDomainEvent : INotification
    {
        DateTime CreatedAt { get; }
    }
}
