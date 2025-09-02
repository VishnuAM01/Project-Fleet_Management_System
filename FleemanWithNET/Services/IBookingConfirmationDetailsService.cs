using fleeman_with_dot_net.DTO;
using fleeman_with_dot_net.Models;

namespace fleeman_with_dot_net.Services
{
    public interface IBookingConfirmationDetailsService
    {
        BookingConfirmationDetails Create(BookingConfirmationDTO dto);
        BookingConfirmationDetails GetById(int bookingId);
        IEnumerable<BookingConfirmationDetails> GetAll();
        IEnumerable<ConfirmedBookingDTO> GetConfirmedBookingsWithDetails();
        IEnumerable<ConfirmedBookingDTO> GetConfirmedBookingsWithDetails(int? carId, int? memberId, DateTime? fromDate, DateTime? toDate);
        void Delete(int bookingId);
    }
}
