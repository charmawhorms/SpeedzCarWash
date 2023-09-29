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
        public IActionResult GetPaymentMethods()
        {
            var paymentMethods = _db.PaymentMethods.ToList();
            if (paymentMethods == null)
            {
                return NotFound();
            }

            return Ok(paymentMethods);
        }



        //Method that gets a single payment method from the database
        [HttpGet("{id}")]
        public IActionResult GetPaymentMethodById(int id)
        {
            var paymentMethod = _db.PaymentMethods.FirstOrDefault(x => x.Id == id);
            if (paymentMethod == null)
            {
                return NotFound();
            }

            return Ok(paymentMethod);
        }



        //Method that post payment methods to the database
        [HttpPost]
        public IActionResult PostPaymentMethods(PaymentMethod paymentmethod)
        {
            _db.PaymentMethods.Add(paymentmethod);
            _db.SaveChanges();
            return CreatedAtAction(nameof(GetPaymentMethodById), new { id =  paymentmethod.Id }, paymentmethod);
        }



        //Method that updates a payment method based on the id in the database
        [HttpPut("{id}")]
        public IActionResult UpdatePaymentMethod(int id, [FromBody] PaymentMethod paymentmethod)
        {
            if (id != paymentmethod.Id)
            {
                return NotFound();
            }

            _db.SaveChanges();
            return CreatedAtAction(nameof(GetPaymentMethodById), new { id = paymentmethod.Id }, paymentmethod);
        }



        //Method that deletes a payment method based on the id from the database
        [HttpDelete("{id}")]
        public IActionResult DeletePaymentMethod(int id)
        {
            var paymentmethodFromDb = _db.PaymentMethods.FirstOrDefault(x => x.Id == id);
            if (paymentmethodFromDb == null)
            {
                return NotFound();
            }

            _db.PaymentMethods.Remove(paymentmethodFromDb);
            _db.SaveChanges();

            return Ok(paymentmethodFromDb);
        }
    }
}
