using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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



        //Method that gets all the dropdown lists
        [HttpGet("Upsert")]
        public ActionResult<BookingVM> GetBookingVM()
        {
            BookingVM bookingVM = new BookingVM();
            bookingVM.Booking = new Booking();
            bookingVM.PaymentMethod = _db.PaymentMethods.Select(iquery => new SelectListItem()
            {
                Text = iquery.MethodName,
                Value = iquery.Id.ToString()
            }).ToList();

            bookingVM.Washer = _db.Washers.Select(iquery => new SelectListItem()
            {
                Text = $"{iquery.FirstName} {iquery.LastName}",
                Value = iquery.Id.ToString()
            }).ToList();

            bookingVM.Vehicle = _db.Vehicles.Select(iquery => new SelectListItem()
            {
                Text = iquery.VehicleType,
                Value = iquery.Id.ToString()
            }).ToList();

            return Ok(bookingVM);
        }


        [HttpGet("{id}")]
        //Method that gets a single booking from the database

        public async Task<ActionResult<BookingVM>> GetBooking(int id)
        {
            BookingVM bookingVM = new BookingVM();
            bookingVM.Booking = await _db.Bookings.FindAsync(id);

            bookingVM.PaymentMethod = _db.PaymentMethods.Select(iquery => new SelectListItem()
            {
                Text = iquery.MethodName,
                Value = iquery.Id.ToString()
            }).ToList();

            bookingVM.Washer = _db.Washers.Select(iquery => new SelectListItem()
            {
                Text = $"{iquery.FirstName} {iquery.LastName}",
                Value = iquery.Id.ToString()
            }).ToList();

            bookingVM.Vehicle = _db.Vehicles.Select(iquery => new SelectListItem()
            {
                Text = iquery.VehicleType,
                Value = iquery.Id.ToString()
            }).ToList();

            return Ok(bookingVM);
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
        [HttpPut("{id}")]
        public async Task<ActionResult<List<BookingVM>>> UpdateBooking(int id, BookingVM bookingVM)
        {

            var bookingFromDb = await _db.Bookings.FindAsync(id);
            if (bookingFromDb == null)
            {
                return BadRequest("Booking not found");
            }
                

            bookingFromDb.CustomerFirstName = bookingVM.Booking.CustomerFirstName;
            bookingFromDb.CustomerLastName = bookingVM.Booking.CustomerFirstName;
            bookingFromDb.PhoneNumber = bookingVM.Booking.PhoneNumber;
            bookingFromDb.Email = bookingVM.Booking.Email;
            bookingFromDb.DateBooked = bookingVM.Booking.DateBooked;
            //bookingFromDb.VehicleMake = bookingVM.Booking.VehicleMake;
            bookingFromDb.VehicleModel = bookingVM.Booking.VehicleModel;
            bookingFromDb.VehicleColor = bookingVM.Booking.VehicleColor;
            bookingFromDb.VehicleLicensePlateNumber = bookingVM.Booking.VehicleLicensePlateNumber;
            //bookingFromDb.PaymentMethodId = bookingVM.Booking.PaymentMethodId;
            //bookingFromDb.WasherId = bookingVM.Booking.WasherId;
            //bookingFromDb.VehicleId = bookingVM.Booking.VehicleId;


            await _db.SaveChangesAsync();
            return Ok(bookingFromDb);
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
