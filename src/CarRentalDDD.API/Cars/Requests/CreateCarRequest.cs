using System.ComponentModel.DataAnnotations;

namespace CarRentalDDD.API.Cars.Requests
{
    public class CreateCarRequest
    {
        [Required]
        public string Model { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Registration { get; set; }

        public int Odmometer { get; set; }

        [Required]
        public int Year { get; set; }
    }
}
