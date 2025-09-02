using fleeman_with_dot_net.DTO;
using fleeman_with_dot_net.Models;
using fleeman_with_dot_net.Services;
using Microsoft.AspNetCore.Mvc;

namespace fleeman_with_dot_net.Controllers
{
    [Route("api/cities")]
    [ApiController]
    public class CityDetailsController : ControllerBase
    {
        private readonly ICityDetailsService service;

        public CityDetailsController(ICityDetailsService service)
        {
            this.service = service;
        }

        [HttpGet]
        public IActionResult GetAllCities()
        {
            return Ok(service.GetAllCities());
        }

        [HttpGet("{id}")]
        public IActionResult GetCityById(int id)
        {
            var city = service.GetCityById(id);
            if (city == null) return NotFound();
            return Ok(city);
        }

        [HttpPost]
        public IActionResult AddCity([FromBody] CityDetails city)
        {
            var createdCity = service.AddCity(city);
            return CreatedAtAction(nameof(GetCityById), new { id = createdCity.City_Id }, createdCity);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCity(int id, [FromBody] CityDetails city)
        {
            if (id != city.City_Id) return BadRequest();

            var updatedCity = service.UpdateCity(city);
            if (updatedCity == null) return NotFound();

            return Ok(updatedCity);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCity(int id)
        {
            var deleted = service.DeleteCity(id);
            if (!deleted) return NotFound();

            return NoContent();
        }

        [HttpGet("by-state/{stateName}")]
        public IActionResult GetCitiesByStateName(string stateName)
        {
            var cities = service.GetCitiesByStateName(stateName);

            if (cities == null)
            {
                return NotFound();
            }

            return Ok(cities);
        }
        [HttpGet("all")]
        public ActionResult<IEnumerable<CityDTO>> GetAllCityWithState()
        {
            var cities = service.GetAllCity();
            return Ok(cities);
        }
    }
}
