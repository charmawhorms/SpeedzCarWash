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
        public async Task<ActionResult<List<Washer>>> GetWashers()
        {
            return Ok(await _db.Washers.ToListAsync());
        }



        //Method that gets a single washer from the database
        [HttpGet("{id}")]
        public async Task<ActionResult<Washer>> GetWasher(int id)
        {
            var washer = await _db.Washers.FindAsync(id);
            if (washer == null)
                return BadRequest("Washer not found");
            return Ok(washer);
        }


        
        //Method that post washers to the database
        [HttpPost]
        public async Task<ActionResult<List<Washer>>> PostWashers(Washer washer)
        {
            _db.Washers.Add(washer);
            await _db.SaveChangesAsync();
            return Ok(await _db.Washers.ToListAsync());
        }



        //Method that updates a washer based on the id in the database
        [HttpPut]
        public async Task<ActionResult<List<Washer>>> UpdateProduct(Washer washer)
        {
            var washerFromDb = await _db.Washers.FindAsync(washer.Id);
            if (washerFromDb == null)
                return BadRequest("Washer not found");

            washerFromDb.FirstName = washer.FirstName;
            washerFromDb.LastName = washer.FirstName;
            washerFromDb.Available = washer.Available;

            await _db.SaveChangesAsync();
            return Ok(await _db.Washers.ToListAsync());
        }



        //Method that deletes a washer based on the id from the database
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Washer>>> DeleteWasher(int id)
        {
            var washerFromDb = await _db.Washers.FindAsync(id);
            if (washerFromDb == null)
                return BadRequest("Washer not found");

            _db.Washers.Remove(washerFromDb);
            _db.SaveChanges();
            return Ok(washerFromDb);
        }
    }
}
