using fleeman_with_dot_net.DTO;
using fleeman_with_dot_net.Models;
using fleeman_with_dot_net.Repositories;
using Microsoft.EntityFrameworkCore;

namespace fleeman_with_dot_net.Services
{
    public class BookingStatusService : IBookingStatusService
    {
        private readonly FleetDBContext _context;

        public BookingStatusService(FleetDBContext context)
        {
            _context = context;
        }

        public async Task<List<BookingStatusDTO>> GetAllBookingStatusesAsync()
        {
            var bookingStatuses = new List<BookingStatusDTO>();

            // Get all bookings
            var bookings = await _context.booking_details.ToListAsync();

            foreach (var booking in bookings)
            {
                var status = await DetermineBookingStatusAsync(booking.BookingId);
                var bookingStatus = await CreateBookingStatusDTOAsync(booking, status);
                bookingStatuses.Add(bookingStatus);
            }

            return bookingStatuses.OrderByDescending(b => b.CreatedDate).ToList();
        }

        public async Task<List<BookingStatusDTO>> GetBookingStatusesByMemberIdAsync(int memberId)
        {
            var bookingStatuses = new List<BookingStatusDTO>();

            // Get bookings for specific member
            var bookings = await _context.booking_details
                .Where(b => b.MemberId == memberId)
                .ToListAsync();

            foreach (var booking in bookings)
            {
                var status = await DetermineBookingStatusAsync(booking.BookingId);
                var bookingStatus = await CreateBookingStatusDTOAsync(booking, status);
                bookingStatuses.Add(bookingStatus);
            }

            return bookingStatuses.OrderByDescending(b => b.CreatedDate).ToList();
        }

        public async Task<BookingStatusDTO?> GetBookingStatusByIdAsync(int bookingId)
        {
            var booking = await _context.booking_details
                .FirstOrDefaultAsync(b => b.BookingId == bookingId);

            if (booking == null)
                return null;

            var status = await DetermineBookingStatusAsync(booking.BookingId);
            return await CreateBookingStatusDTOAsync(booking, status);
        }

        private async Task<string> DetermineBookingStatusAsync(int bookingId)
        {
            // Check if booking exists in invoice table (highest priority - completed)
            var hasInvoice = await _context.invoice_header_table
                .AnyAsync(i => i.BookingId == bookingId);

            if (hasInvoice)
                return "Completed";

            // Check if booking exists in confirmation table (in progress)
            var hasConfirmation = await _context.booking_confirmation_details
                .AnyAsync(c => c.BookingId == bookingId);

            if (hasConfirmation)
                return "In Progress";

            // If only exists in booking_details table (pending)
            return "Pending";
        }

        private async Task<BookingStatusDTO> CreateBookingStatusDTOAsync(BookingDetails booking, string status)
        {
            // Get member details
            var member = await _context.member_details
                .FirstOrDefaultAsync(m => m.Member_Id == booking.MemberId);

            // Get car details (using VehicleId to find car)
            var car = await _context.car_details
                .FirstOrDefaultAsync(c => c.VehicleId == booking.VehicleId);

            var bookingStatus = new BookingStatusDTO
            {
                BookingId = booking.BookingId,
                MemberId = booking.MemberId ?? 0,
                MemberName = member != null ? $"{member.MemberFirstName} {member.MemberLastName}" : "Unknown",
                Email = member?.Email ?? "",
                CarId = car?.Car_Id ?? 0,
                CarName = car?.CarName ?? "Unknown Car",
                PickupDate = booking.PickupDate ?? DateTime.UtcNow,
                ReturnDate = booking.ReturnDate ?? DateTime.UtcNow,
                Status = status,
                CreatedDate = booking.BookingDate ?? DateTime.UtcNow
            };

            // Get confirmation ID if exists
            if (status == "In Progress" || status == "Completed")
            {
                var confirmation = await _context.booking_confirmation_details
                    .FirstOrDefaultAsync(c => c.BookingId == booking.BookingId);
                bookingStatus.ConfirmationId = confirmation?.BookingConfirmationDetailsId;
            }

            // Get invoice ID if exists
            if (status == "Completed")
            {
                var invoice = await _context.invoice_header_table
                    .FirstOrDefaultAsync(i => i.BookingId == booking.BookingId);
                bookingStatus.InvoiceId = invoice?.InvoiceId;
                bookingStatus.TotalAmount = invoice != null ? (decimal)(invoice.CarRentalAmount + invoice.AddonRentalAmount) : null;
            }

            return bookingStatus;
        }
    }
}
