using System.ComponentModel.DataAnnotations;

namespace SpeedzCarWashClient.Models
{
    public class PaymentMethod
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string MethodName { get; set; }
    }
}
