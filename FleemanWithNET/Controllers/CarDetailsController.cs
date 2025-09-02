using fleeman_with_dot_net.Models;
using fleeman_with_dot_net.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using fleeman_with_dot_net.DTO;

namespace fleeman_with_dot_net.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarDetailsController : ControllerBase
    {
        private readonly ICarDetailsService service;

        public CarDetailsController(ICarDetailsService service)
        {
            this.service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CarDetails>> GetAllCars()
        {
            var cars = service.GetAllCars();
            return Ok(cars);
        }

        [HttpGet("{id}")]
        public ActionResult<CarDetails> GetCarById(int id)
        {
            var car = service.GetCarById(id);
            if (car == null) return NotFound();
            return Ok(car);
        }

        [HttpPost]
        public IActionResult AddCar(CarDetails car)
        {
            var createdCar = service.AddCar(car);
            return Ok(createdCar);
        }

        [HttpPut("{id}")]
        public ActionResult<CarDetails> UpdateCar(int id, CarDetails car)
        {
            if (id != car.Car_Id) return BadRequest();

            var updatedCar = service.UpdateCar(car);
            if (updatedCar == null) return NotFound();

            return Ok(updatedCar);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCar(int id)
        {
            if (!service.DeleteCar(id)) return NotFound();

            return NoContent();
        }

        [HttpGet("by-vehicle/{vehicleId}")]
        public ActionResult<IEnumerable<CarDetails>> GetCarsByVehicleId(int vehicleId)
        {
            var cars = service.GetCarsByVehicleId(vehicleId);
            if (cars == null || !cars.Any())
                return NotFound();

            return Ok(cars);
        }

        [HttpGet("available/by-vehicle/{vehicleId}")]
        public ActionResult<IEnumerable<CarDetails>> GetAvailableCarsByVehicleId(int vehicleId)
        {
            var availableCars = service.GetAvailableCarsByVehicleId(vehicleId);
            if (availableCars == null || !availableCars.Any())
                return NotFound(new { message = $"No available cars found for vehicle type ID: {vehicleId}" });

            return Ok(availableCars);
        }

        [HttpGet("available/with-details/by-vehicle/{vehicleId}")]
        public ActionResult<IEnumerable<AvailableCarDTO>> GetAvailableCarsWithDetailsByVehicleId(int vehicleId)
        {
            var availableCars = service.GetAvailableCarsWithDetailsByVehicleId(vehicleId);
            if (availableCars == null || !availableCars.Any())
                return NotFound(new { message = $"No available cars found for vehicle type ID: {vehicleId}" });

            return Ok(availableCars);
        }
    }
}
