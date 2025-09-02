using fleeman_with_dot_net.Models;
using fleeman_with_dot_net.Services;
using Microsoft.AspNetCore.Mvc;

namespace fleeman_with_dot_net.Controllers
{
    [Route("api/locations")]
    [ApiController]
    public class LocationDetailsController : ControllerBase
    {
        private readonly ILocationDetailsService _service;

        public LocationDetailsController(ILocationDetailsService service)
        {
            _service = service;
        }

        [HttpGet("city/{cityName}")]
        public IActionResult GetLocations(string cityName)
        {
            var locations = _service.GetLocationsByCity(cityName);
            return Ok(locations);
        }

        [HttpGet("airport/{airportCode}")]
        public IActionResult GetLocations1(string airportCode)
        {
            var locations = _service.GetLocationsByAirport(airportCode);
            return Ok(locations);
        }

    }
}
