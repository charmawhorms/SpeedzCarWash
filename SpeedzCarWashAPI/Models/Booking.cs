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
        [DisplayName("First Name")]
        public string CustomerFirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string CustomerLastName { get; set; }

        [Required]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }

        public string? Email { get; set; }

        [Required]
        [DisplayName("Date")]
        public DateTime DateBooked { get; set; }

        [Required]
        [DisplayName("Model")]
        public string VehicleModel { get; set; }

        [Required]
        [DisplayName("Color")]
        public string VehicleColor { get; set; }

        [Required]
        [DisplayName("License Plate Number")]
        public string VehicleLicensePlateNumber { get; set;}

        [Required]
        public double Price { get; set; }




        //Relationships
        [ForeignKey("PaymentMethodId")]
        public virtual PaymentMethod? PaymentMethod { get; set; }



        [ForeignKey("WasherId")]
        public virtual Washer? Washer { get; set; }



        [ForeignKey("VehicleId")]
        public virtual Vehicle? Vehicle { get; set; }
    }
}
