using fleeman_with_dot_net.DTO;
using fleeman_with_dot_net.Models;
using System.Collections.Generic;

namespace fleeman_with_dot_net.Services
{
    public interface IBookingDetailsService
    {
        IEnumerable<BookingDetails> GetAllBookings();
        BookingDetails GetBookingById(int id);
        BookingDetails AddBooking(BookingRequestDTO request);
        BookingDetails UpdateBooking(BookingDetails booking);
        bool DeleteBooking(int id);
        IEnumerable<UnconfirmedBookingDTO> GetUnconfirmedBookings();
        IEnumerable<UnconfirmedBookingDTO> GetUnconfirmedBookings(DateTime? fromDate, DateTime? toDate, int? memberId);
    }
}
