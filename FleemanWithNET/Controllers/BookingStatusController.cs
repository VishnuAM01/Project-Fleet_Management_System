using fleeman_with_dot_net.DTO;
using fleeman_with_dot_net.Services;
using Microsoft.AspNetCore.Mvc;

namespace fleeman_with_dot_net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingStatusController : ControllerBase
    {
        private readonly IBookingStatusService _bookingStatusService;

        public BookingStatusController(IBookingStatusService bookingStatusService)
        {
            _bookingStatusService = bookingStatusService;
        }

        /// <summary>
        /// Get all bookings with their status
        /// </summary>
        /// <returns>List of all bookings with status (Pending, In Progress, Completed)</returns>
        [HttpGet("all")]
        public async Task<ActionResult<List<BookingStatusDTO>>> GetAllBookingStatuses()
        {
            try
            {
                var bookingStatuses = await _bookingStatusService.GetAllBookingStatusesAsync();
                return Ok(bookingStatuses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving booking statuses", error = ex.Message });
            }
        }

        /// <summary>
        /// Get booking statuses for a specific member
        /// </summary>
        /// <param name="memberId">Member ID</param>
        /// <returns>List of booking statuses for the member</returns>
        [HttpGet("member/{memberId}")]
        public async Task<ActionResult<List<BookingStatusDTO>>> GetBookingStatusesByMemberId(int memberId)
        {
            try
            {
                var bookingStatuses = await _bookingStatusService.GetBookingStatusesByMemberIdAsync(memberId);
                return Ok(bookingStatuses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving booking statuses", error = ex.Message });
            }
        }

        /// <summary>
        /// Get booking status by booking ID
        /// </summary>
        /// <param name="bookingId">Booking ID</param>
        /// <returns>Booking status details</returns>
        [HttpGet("{bookingId}")]
        public async Task<ActionResult<BookingStatusDTO>> GetBookingStatusById(int bookingId)
        {
            try
            {
                var bookingStatus = await _bookingStatusService.GetBookingStatusByIdAsync(bookingId);
                
                if (bookingStatus == null)
                    return NotFound(new { message = "Booking not found" });

                return Ok(bookingStatus);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving booking status", error = ex.Message });
            }
        }

        /// <summary>
        /// Get bookings by status
        /// </summary>
        /// <param name="status">Status filter (Pending, In Progress, Completed)</param>
        /// <returns>List of bookings with the specified status</returns>
        [HttpGet("status/{status}")]
        public async Task<ActionResult<List<BookingStatusDTO>>> GetBookingsByStatus(string status)
        {
            try
            {
                var allBookings = await _bookingStatusService.GetAllBookingStatusesAsync();
                var filteredBookings = allBookings.Where(b => b.Status.Equals(status, StringComparison.OrdinalIgnoreCase)).ToList();
                
                return Ok(filteredBookings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving bookings by status", error = ex.Message });
            }
        }
    }
}
