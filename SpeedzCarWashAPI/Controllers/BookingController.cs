using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpeedzCarWashAPI.Data;
using SpeedzCarWashAPI.Models;

namespace SpeedzCarWashAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public BookingController(ApplicationDbContext db)
        {
            _db = db;
        }



        //Method that gets all the bookings from the database
        [HttpGet]
        public async Task<ActionResult<List<Booking>>> GetBookings()
        {
            return Ok(await _db.Bookings.ToListAsync());
        }



        //Method that gets a single booking from the database
        [HttpGet("{id}")]
        public async Task<ActionResult<Washer>> GetBooking(int id)
        {
            var booking = await _db.Bookings.FindAsync(id);
            if (booking == null)
                return BadRequest("Booking not found");
            return Ok(booking);
        }



        //Method that post bookings to the database
        [HttpPost]
        public async Task<ActionResult<List<Booking>>> PostBookings(Booking booking)
        {
            _db.Bookings.Add(booking);
            await _db.SaveChangesAsync();
            return Ok(await _db.Bookings.ToListAsync());
        }



        //Method that updates a booking based on the id in the database
        [HttpPut]
        public async Task<ActionResult<List<Booking>>> UpdateBooking(Booking booking)
        {
            var bookingFromDb = await _db.Bookings.FindAsync(booking);
            if (bookingFromDb == null)
                return BadRequest("Booking not found");

            bookingFromDb.CustomerFirstName = booking.CustomerLastName;
            bookingFromDb.CustomerLastName = booking.CustomerLastName;
            bookingFromDb.PhoneNumber = booking.PhoneNumber;
            bookingFromDb.Email = booking.Email;
            bookingFromDb.DateBooked = booking.DateBooked;
            bookingFromDb.VehicleMake = booking.VehicleMake;
            bookingFromDb.VehicleModel = booking.VehicleModel;
            bookingFromDb.VehicleColor = booking.VehicleColor;
            bookingFromDb.VehicleLicensePlateNumber = booking.VehicleLicensePlateNumber;
            bookingFromDb.PaymentMethodId = booking.PaymentMethodId;
            bookingFromDb.WasherId = booking.WasherId;
            bookingFromDb.VehicleId = booking.VehicleId;

            await _db.SaveChangesAsync();
            return Ok(await _db.Bookings.ToListAsync());
        }



        //Method that deletes a booking based on the id from the database
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Booking>>> DeleteBooking(int id)
        {
            var bookingFromDb = await _db.Bookings.FindAsync(id);
            if (bookingFromDb == null)
                return BadRequest("Booking not found");

            _db.Bookings.Remove(bookingFromDb);
            _db.SaveChanges();
            return Ok(bookingFromDb);
        }
    }
}
