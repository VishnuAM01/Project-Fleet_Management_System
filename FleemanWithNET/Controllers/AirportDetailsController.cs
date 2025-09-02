using fleeman_with_dot_net.Models;
using fleeman_with_dot_net.Services;
using Microsoft.AspNetCore.Mvc;
namespace fleeman_with_dot_net.Controllers
{

    [ApiController]

    [Route("api/airports")]

    public class AirportDetailsController:ControllerBase
    {
        private readonly IAirportDetailsService service;

        public AirportDetailsController(IAirportDetailsService service)
        {
            this.service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AirportDetails>> GetAllAirport()
        {
            var airports=service.GetAllAirport();
            return Ok(airports);
        }

        [HttpGet("{id}")]
        public ActionResult<AirportDetails> GetAirportById(int id)
        {
            var airport = service.GetAirportById(id);
            if (airport == null)
            {
                return NotFound();
            }
            return Ok(airport);
        }

        [HttpPost]
        public IActionResult AddAirport(AirportDetails airport)
        {
            var createdAirport = service.AddAirport(airport);
            return Ok(createdAirport); 
        }

        [HttpPut("{id}")]
        public ActionResult<AirportDetails> UpdateAirport(int id,AirportDetails airport)
        {
            if (id != airport.Airport_Id)
            {
                return NotFound();
            }

            var updatedAirport = service.UpdateAirport(airport);
            return Ok(updatedAirport);

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAirport(int id)
        {
            if (service.DeleteAirport(id))
                return NoContent();

            return NotFound();
        }



    }
}
