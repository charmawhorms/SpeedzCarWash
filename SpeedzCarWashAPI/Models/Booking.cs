using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpeedzCarWashAPI.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string CustomerFirstName { get; set; }

        [Required]
        public string CustomerLastName { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public string? Email { get; set; }

        [Required]
        public DateTime DateBooked { get; set; }

        [Required]
        public string VehicleMake { get; set; }

        [Required]
        public string VehicleModel { get; set; }

        [Required]
        public string VehicleColor { get; set; }

        [Required]
        public string VehicleLicensePlateNumber { get; set;}


        //Relationships
        public int? PaymentMethodId { get; set; }
        [ForeignKey("PaymentMethodId")]
        public virtual PaymentMethod? PaymentMethod { get; set; }



        public int? WasherId { get; set; }
        [ForeignKey("WasherId")]
        public virtual Washer? Washer { get; set; }



        public int? VehicleId { get; set; }
        [ForeignKey("VehicleId")]
        public virtual Vehicle? Vehicle { get; set; }
    }
}
