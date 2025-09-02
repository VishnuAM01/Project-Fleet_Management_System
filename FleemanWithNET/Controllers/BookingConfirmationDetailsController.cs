using fleeman_with_dot_net.DTO;
using fleeman_with_dot_net.Models;
using fleeman_with_dot_net.Services;
using Microsoft.AspNetCore.Mvc;

namespace fleeman_with_dot_net.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingConfirmationDetailsController : ControllerBase
    {
        private readonly IBookingConfirmationDetailsService _service;

        public BookingConfirmationDetailsController(IBookingConfirmationDetailsService service)
        {
            _service = service;
        }

        [HttpPost]
        public ActionResult<BookingConfirmationDetails> Create([FromBody] BookingConfirmationDTO dto)
        {
            try
            {
                var saved = _service.Create(dto);
                return CreatedAtAction(nameof(GetById), new { bookingId = saved.BookingId }, saved);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{bookingId}")]
        public ActionResult<BookingConfirmationDetails> GetById(int bookingId)
        {
            try
            {
                var result = _service.GetById(bookingId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookingConfirmationDetails>> GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("confirmed-with-details")]
        public ActionResult<IEnumerable<ConfirmedBookingDTO>> GetConfirmedBookingsWithDetails()
        {
            var confirmedBookings = _service.GetConfirmedBookingsWithDetails();
            return Ok(confirmedBookings);
        }

        [HttpGet("confirmed-with-details/filtered")]
        public ActionResult<IEnumerable<ConfirmedBookingDTO>> GetFilteredConfirmedBookingsWithDetails(
            [FromQuery] int? carId,
            [FromQuery] int? memberId,
            [FromQuery] DateTime? fromDate,
            [FromQuery] DateTime? toDate)
        {
            var confirmedBookings = _service.GetConfirmedBookingsWithDetails(carId, memberId, fromDate, toDate);
            return Ok(confirmedBookings);
        }

        //[HttpPut("{bookingId}")]
        //public ActionResult<BookingConfirmationDetails> Update(int bookingId, [FromBody] BookingConfirmationDetails updated)
        //{
        //    try
        //    {
        //        var saved = _service.Update(bookingId, updated);
        //        return Ok(saved);
        //    }
        //    catch (Exception ex)
        //    {
        //        return NotFound(new { message = ex.Message });
        //    }
        //}

        [HttpDelete("{bookingId}")]
        public IActionResult Delete(int bookingId)
        {
            _service.Delete(bookingId);
            return NoContent();
        }
    }
}
