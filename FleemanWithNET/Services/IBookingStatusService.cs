using fleeman_with_dot_net.DTO;

namespace fleeman_with_dot_net.Services
{
    public interface IBookingStatusService
    {
        Task<List<BookingStatusDTO>> GetAllBookingStatusesAsync();
        Task<List<BookingStatusDTO>> GetBookingStatusesByMemberIdAsync(int memberId);
        Task<BookingStatusDTO?> GetBookingStatusByIdAsync(int bookingId);
    }
}
