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
        public IActionResult GetVehicles()
        {
            var vehicles = _db.Vehicles.ToList();
            if (vehicles == null)
            {
                return NotFound();
            }

            return Ok(vehicles);
        }



        //Method that gets a single vehicle from the database
        [HttpGet("{id}")]
        public IActionResult GetVehicleById(int id)
        {
            var vehicle = _db.Vehicles.FirstOrDefault(x => x.Id == id);
            if (vehicle == null)
            {
                return NotFound("Vehicle not found");
            }
                
            return Ok(vehicle);
        }



        //Method that post vehicles to the database
        [HttpPost]
        public IActionResult PostVehicles(Vehicle vehicle)
        {
            _db.Vehicles.Add(vehicle);
            _db.SaveChanges();
            return CreatedAtAction(nameof(GetVehicleById), new { id = vehicle.Id }, vehicle);
        }



        //Method that updates a vehicle based on the id in the database
        [HttpPut("{id}")]
        public IActionResult UpdateVehicle(int id, [FromBody] Vehicle vehicle)
        {
            if (id != vehicle.Id)
            {
                return NotFound();
            }

            _db.SaveChanges();
            return CreatedAtAction(nameof(GetVehicleById), new { id = vehicle.Id }, vehicle);
        }



        //Method that deletes a vehicle based on the id from the database
        [HttpDelete("{id}")]
        public IActionResult DeleteVehicle(int id)
        {
            var vehicleFromDb = _db.Vehicles.FirstOrDefault(x => x.Id == id);
            if (vehicleFromDb == null)
            {
                return NotFound();
            }

            _db.Vehicles.Remove(vehicleFromDb);
            _db.SaveChanges();

            return Ok(vehicleFromDb);
        }
    }
}
