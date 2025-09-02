using fleeman_with_dot_net.Models;
using fleeman_with_dot_net.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace fleeman_with_dot_net.Controllers
{
    [ApiController]
    [Route("api/vehicles")]
    public class VehicleDetailsController : ControllerBase
    {
        private readonly IVehicleDetailsService service;

        public VehicleDetailsController(IVehicleDetailsService service)
        {
            this.service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<VehicleDetails>> GetAllVehicles()
        {
            var vehicles = service.GetAllVehicles();
            return Ok(vehicles);
        }

        [HttpGet("{id}")]
        public ActionResult<VehicleDetails> GetVehicleById(int id)
        {
            var vehicle = service.GetVehicleById(id);
            if (vehicle == null) return NotFound();
            return Ok(vehicle);
        }

        [HttpPost]
        public IActionResult AddVehicle(VehicleDetails vehicle)
        {
            var createdVehicle = service.AddVehicle(vehicle);
            return Ok(createdVehicle);
        }

        [HttpPut("{id}")]
        public ActionResult<VehicleDetails> UpdateVehicle(int id, VehicleDetails vehicle)
        {
            if (id != vehicle.VehicleId) return BadRequest();

            var updatedVehicle = service.UpdateVehicle(vehicle);
            if (updatedVehicle == null) return NotFound();

            return Ok(updatedVehicle);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteVehicle(int id)
        {
            if (!service.DeleteVehicle(id)) return NotFound();

            return NoContent();
        }
    }
}
