using System;
using System.Collections.Generic;

namespace CarRentalDDD.Domain.SeedWork
{
    public abstract class Entity
    {
        public virtual Guid Id { get; private set; }

        private List<IDomainEvent> _domainEvents;

        /// <summary>
        /// These events are dispatched after EF dbcontext.SaveChangesAsync
        /// </summary>
        public List<IDomainEvent> DomainEvents => _domainEvents;


        /// <summary>
        /// Add Domain Event
        /// These events are dispatched after EF dbcontext.SaveChangesAsync
        /// Please see UnitOfWork > CommitAsync
        /// </summary>
        /// <param name="eventItem"></param>
        public void AddDomainEvent(IDomainEvent eventItem)
        {
            _domainEvents = _domainEvents ?? new List<IDomainEvent>();
            _domainEvents.Add(eventItem);
        }

        /// <summary>
        /// Remove domain event
        /// </summary>
        /// <param name="eventItem"></param>
        public void RemoveDomainEvent(IDomainEvent eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }
    }

 
}
