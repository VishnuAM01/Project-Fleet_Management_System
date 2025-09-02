using fleeman_with_dot_net.Models;
using fleeman_with_dot_net.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace fleeman_with_dot_net.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddOnBookController : ControllerBase
    {
        private readonly IAddOnBookService _service;

        public AddOnBookController(IAddOnBookService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AddOnBook>> GetAll()
        {
            var addons = _service.GetAllAddOnBooks();
            return Ok(addons);
        }

        [HttpGet("booking/{bookingId}")]
        public ActionResult<IEnumerable<AddOnBook>> GetByBookingId(int bookingId)
        {
            var addons = _service.GetAddOnBooksByBookingId(bookingId);
            return Ok(addons);
        }

        [HttpPost]
        public IActionResult Add([FromBody] AddOnBook addOnBook)
        {
            var created = _service.AddAddOnBook(addOnBook);
            return Ok(created);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _service.DeleteAddOnBook(id);
            if (deleted) return NoContent();
            return NotFound();
        }
    }
}
