using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpeedzCarWashAPI.Data;
using SpeedzCarWashAPI.Models;

namespace SpeedzCarWashAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public PaymentMethodController(ApplicationDbContext db)
        {
            _db = db;
        }



        //Method that gets all the payment methods from the database
        [HttpGet]
        //public async Task<ActionResult<List<PaymentMethod>>> GetPaymentmethods()
        //{
        //    return Ok(await _db.PaymentMethods.ToListAsync());
        //}

        public async Task<ActionResult<List<PaymentMethod>>> GetPaymentMethods()
        {
            var paymentMethods = await _db.PaymentMethods.ToListAsync();
            return Ok(paymentMethods);
        }



        //Method that gets a single payment method from the database
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentMethod>> GetPaymentMethod(int id)
        {
            var paymentMethod = await _db.PaymentMethods.FindAsync(id);
            if (paymentMethod == null)
                return BadRequest("Payment method not found");
            return Ok(paymentMethod);
        }



        //Method that post payment methods to the database
        [HttpPost]
        public async Task<ActionResult<List<PaymentMethod>>> PostPaymentMethods(PaymentMethod paymentmethod)
        {
            _db.PaymentMethods.Add(paymentmethod);
            await _db.SaveChangesAsync();
            return Ok(await _db.PaymentMethods.ToListAsync());
        }



        //Method that updates a payment method based on the id in the database
        [HttpPut]
        public async Task<ActionResult<List<PaymentMethod>>> UpdatePaymentMethod(PaymentMethod paymentmethod)
        {
            var paymentmethodFromDb = await _db.PaymentMethods.FindAsync(paymentmethod);
            if (paymentmethodFromDb == null)
                return BadRequest("Payment method not found");

            paymentmethodFromDb.MethodName = paymentmethod.MethodName;

            await _db.SaveChangesAsync();
            return Ok(await _db.PaymentMethods.ToListAsync());
        }



        //Method that deletes a payment method based on the id from the database
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<PaymentMethod>>> DeletePaymentMethod(int id)
        {
            var paymentmethodFromDb = await _db.PaymentMethods.FindAsync(id);
            if (paymentmethodFromDb == null)
                return BadRequest("Payment method not found");

            _db.PaymentMethods.Remove(paymentmethodFromDb);
            _db.SaveChanges();
            return Ok(paymentmethodFromDb);
        }
    }
}
