using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpeedzCarWashAPI.Data;
using SpeedzCarWashAPI.Models;

namespace SpeedzCarWashAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WasherController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public WasherController(ApplicationDbContext db)
        {
            _db = db;
        }



        //Method that gets all the washers from the database
        [HttpGet]
        public IActionResult GetWashers()
        {
            var washers = _db.Washers.ToList();
            if (washers == null)
            {
                return BadRequest();
            }
            return Ok(washers);
        }



        //Method that gets a single washer from the database
        [HttpGet("{id}")]
        public IActionResult GetWasherById(int id)
        {
            var washer = _db.Washers.FirstOrDefault(x => x.Id == id);
            if (washer == null)
            {
                return NotFound("Washer not found");
            }
            return Ok(washer);
        }


        
        //Method that post washers to the database
        [HttpPost]
        public IActionResult PostWashers(Washer washer)
        {
            _db.Washers.Add(washer);
            _db.SaveChanges();
            return CreatedAtAction(nameof(GetWasherById), new { id = washer.Id }, washer);
        }



        //Method that updates a washer based on the id in the database
        [HttpPut("{id}")]
        public IActionResult UpdateWasher(int id, [FromBody] Washer washer)
        {
            if (id != washer.Id)
            {
                return NotFound();
            }

            _db.Washers.Update(washer);
            _db.SaveChanges();

            return CreatedAtAction(nameof(GetWasherById), new { id = washer.Id }, washer);
        }



        //Method that deletes a washer based on the id from the database
        [HttpDelete("{id}")]
        public IActionResult DeleteWasher(int id)
        {
            var washerFromDb = _db.Washers.FirstOrDefault(x => x.Id == id);
            if (washerFromDb == null)
            {
                return NotFound();
            }
                
            _db.Washers.Remove(washerFromDb);
            _db.SaveChanges();

            return Ok(washerFromDb);
        }
    }
}
