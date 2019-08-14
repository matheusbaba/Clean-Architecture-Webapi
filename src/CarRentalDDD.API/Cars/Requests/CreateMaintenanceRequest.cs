using System;
using System.ComponentModel.DataAnnotations;


namespace CarRentalDDD.API.Cars.Requests
{
    public class CreateMaintenanceRequest
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string Service { get; set; }
        public string Description { get; set; }
    }
}
