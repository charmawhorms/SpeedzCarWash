using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpeedzCarWashAPI.Data;
using SpeedzCarWashAPI.Models;

namespace SpeedzCarWashAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public VehicleController(ApplicationDbContext db)
        {
            _db = db;
        }



        //Method that gets all the vehicles from the database
        [HttpGet]
        public async Task<ActionResult<List<Vehicle>>> GetVehicles()
        {
            return Ok(await _db.Vehicles.ToListAsync());
        }



        //Method that gets a single vehicle from the database
        [HttpGet("{id}")]
        public async Task<ActionResult<Vehicle>> GetVehicle(int id)
        {
            var vehicle = await _db.Vehicles.FindAsync(id);
            if (vehicle == null)
                return BadRequest("Vehicle not found");
            return Ok(vehicle);
        }



        //Method that post vehicles to the database
        [HttpPost]
        public async Task<ActionResult<List<Vehicle>>> PostVehicles(Vehicle vehicle)
        {
            _db.Vehicles.Add(vehicle);
            await _db.SaveChangesAsync();
            return Ok(await _db.Vehicles.ToListAsync());
        }



        //Method that updates a vehicle based on the id in the database
        [HttpPut]
        public async Task<ActionResult<List<Vehicle>>> UpdateVehicle(Vehicle vehicle)
        {
            var vehicleFromDb = await _db.Vehicles.FindAsync(vehicle.Id);
            if (vehicleFromDb == null)
                return BadRequest("Vehicle not found");

            vehicleFromDb.VehicleType = vehicle.VehicleType;
            vehicleFromDb.Price = vehicle.Price;

            await _db.SaveChangesAsync();
            return Ok(await _db.Vehicles.ToListAsync());
        }



        //Method that deletes a vehicle based on the id from the database
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Vehicle>>> DeleteVehicle(int id)
        {
            var vehicleFromDb = await _db.Vehicles.FindAsync(id);
            if (vehicleFromDb == null)
                return BadRequest("Vehicle not found");

            _db.Vehicles.Remove(vehicleFromDb);
            _db.SaveChanges();
            return Ok(vehicleFromDb);
        }
    }
}
