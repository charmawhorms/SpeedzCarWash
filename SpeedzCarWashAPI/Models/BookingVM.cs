using Microsoft.AspNetCore.Mvc.Rendering;

namespace SpeedzCarWashAPI.Models
{
    public class BookingVM
    {
        public Booking Booking { get; set; }

        public List<SelectListItem> PaymentMethod { get; set; }

        public List<SelectListItem> Washer { get; set; }

        public List<SelectListItem> Vehicle { get; set; }
    }
}
