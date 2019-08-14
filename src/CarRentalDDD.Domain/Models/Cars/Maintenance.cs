using CarRentalDDD.Domain.SeedWork;
using System;

namespace CarRentalDDD.Domain.Models.Cars
{
    public class Maintenance : Entity
    {        
        public DateTime Date { get; private set; }
        public string Service { get; private set; }
        public string Description{ get; private set; }

        public Maintenance(DateTime date, string service, string description)
        {
            if (string.IsNullOrEmpty(service))
                throw CustomException.NullArgument(nameof(service));
            
            this.Date = date;
            this.Service = service;
            this.Description = description;
        }

        protected Maintenance()
        {

        }
    }
}
