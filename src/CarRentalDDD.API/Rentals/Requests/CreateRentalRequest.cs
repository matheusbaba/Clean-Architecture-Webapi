using System;
using System.ComponentModel.DataAnnotations;

namespace CarRentalDDD.API.Rentals.Requests
{
    public class CreateRentalRequest
    {
        [Required]
        public DateTime PickUpDate { get; set; }
        [Required]
        public DateTime DropOffDate { get; set; }
        [Required]
        public Guid CustomerId { get; set; }
        [Required]
        public Guid CarId { get; set; }
    }
}
