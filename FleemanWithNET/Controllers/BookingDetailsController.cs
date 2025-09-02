using fleeman_with_dot_net.DTO;
using fleeman_with_dot_net.Models;
using fleeman_with_dot_net.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace fleeman_with_dot_net.Controllers
{
    [ApiController]
    [Route("api/bookings")]
    public class BookingDetailsController : ControllerBase
    {
        private readonly IBookingDetailsService _service;

        public BookingDetailsController(IBookingDetailsService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookingDetails>> GetAllBookings()
        {
            var bookings = _service.GetAllBookings();
            return Ok(bookings);
        }

        [HttpGet("unconfirmed")]
        public ActionResult<IEnumerable<UnconfirmedBookingDTO>> GetUnconfirmedBookings()
        {
            var unconfirmedBookings = _service.GetUnconfirmedBookings();
            return Ok(unconfirmedBookings);
        }

        [HttpGet("unconfirmed/filtered")]
        public ActionResult<IEnumerable<UnconfirmedBookingDTO>> GetFilteredUnconfirmedBookings(
            [FromQuery] DateTime? fromDate,
            [FromQuery] DateTime? toDate,
            [FromQuery] int? memberId)
        {
            var unconfirmedBookings = _service.GetUnconfirmedBookings(fromDate, toDate, memberId);
            return Ok(unconfirmedBookings);
        }

        [HttpGet("{id}")]
        public ActionResult<BookingDetails> GetBookingById(int id)
        {
            var booking = _service.GetBookingById(id);
            if (booking == null)
                return NotFound();

            return Ok(booking);
        }

        [HttpPost]
        public IActionResult CreateBooking([FromBody] BookingRequestDTO bookingRequest)
        {
            var booking = _service.AddBooking(bookingRequest);
            return Ok(booking);
        }



        [HttpPut("{id}")]
        public ActionResult<BookingDetails> UpdateBooking(int id, BookingDetails booking)
        {
            if (id != booking.BookingId)
                return BadRequest();

            var updatedBooking = _service.UpdateBooking(booking);
            if (updatedBooking == null)
                return NotFound();

            return Ok(updatedBooking);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBooking(int id)
        {
            if (_service.DeleteBooking(id))
                return NoContent();

            return NotFound();
        }
    }
}
