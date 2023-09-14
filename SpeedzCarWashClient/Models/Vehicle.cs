using System.ComponentModel.DataAnnotations;

namespace SpeedzCarWashClient.Models
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string VehicleType { get; set; }

        [Required]
        public double Price { get; set; }
    }
}
