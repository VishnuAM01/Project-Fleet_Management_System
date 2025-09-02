using fleeman_with_dot_net.DTO;
using fleeman_with_dot_net.Models;
using fleeman_with_dot_net.Repositories;
using fleeman_with_dot_net.Utils;
using Microsoft.EntityFrameworkCore;
using fleeman_with_dot_net.Services;
namespace fleeman_with_dot_net.ServiceImpl
{
    public class InvoiceService : IInvoiceService
    {
        private readonly FleetDBContext _context;

        public InvoiceService(FleetDBContext context)
        {
            _context = context;
        }

        public InvoiceResponseDTO CreateInvoice(InvoiceCreationRequestDTO request)
        {
            // Get current date and time
            var currentDateTime = DateTime.Now;
            
            // Validate booking exists and is confirmed - using separate queries instead of Include
            var bookingConfirmation = _context.booking_confirmation_details
                .FirstOrDefault(bcd => bcd.BookingId == request.BookingId && bcd.Car_Id == request.CarId)
                ?? throw new Exception($"No confirmed booking found for BookingId: {request.BookingId} and CarId: {request.CarId}");

            // Get related data using separate queries since there are no navigation properties
            var booking = _context.booking_details.FirstOrDefault(b => b.BookingId == request.BookingId)
                ?? throw new Exception($"Booking not found for BookingId: {request.BookingId}");
            
            var car = _context.car_details.FirstOrDefault(c => c.Car_Id == request.CarId)
                ?? throw new Exception($"Car not found for CarId: {request.CarId}");

            // Update car fuel status
            car.FuelStatus = request.FuelStatus;
            car.IsAvailable = true; // Mark car as available again

            // Calculate car rental amount using the RateCalculator
            var vehicleDetails = _context.vehicle_details.FirstOrDefault(v => v.VehicleId == booking.VehicleId)
                ?? throw new Exception($"Vehicle details not found for VehicleId: {booking.VehicleId}");

            var carRentalAmount = RateCalculator.CalculateRate(
                booking.PickupDate ?? DateTime.MinValue,
                currentDateTime,
                vehicleDetails.DailyRate,
                vehicleDetails.WeeklyRate,
                vehicleDetails.MonthlyRate
            );

            // Calculate addon rental amount
            double addonRentalAmount = 0;
            var addonBookings = _context.add_on_book
                .Where(a => a.BookingId == request.BookingId)
                .ToList();

            foreach (var addonBooking in addonBookings)
            {
                var addonDetails = _context.add_on_details.FirstOrDefault(a => a.addOnId == addonBooking.add_on_id);
                if (addonDetails != null)
                {
                    // Calculate addon amount using RateCalculator utility
                    addonRentalAmount += RateCalculator.CalculateAddonRate(
                        booking.BookingDate ?? currentDateTime,
                        currentDateTime,
                        addonDetails.addOnPrice
                    );
                }
            }

            // Create invoice
            var invoice = new InvoiceHeaderTable
            {
                InvoiceDate = currentDateTime,
                BookingId = request.BookingId,
                ActualReturnDate = currentDateTime,
                DropLocation = request.DropLocation,
                CarId = request.CarId,
                CarRentalAmount = carRentalAmount,
                AddonRentalAmount = addonRentalAmount
            };

            _context.invoice_header_table.Add(invoice);
            _context.SaveChanges();

            // Delete the booking confirmation entry since the booking is now complete and invoiced
            _context.booking_confirmation_details.Remove(bookingConfirmation);
            _context.SaveChanges();

            // Get additional details for response
            var member = _context.member_details.FirstOrDefault(m => m.Member_Id == booking.MemberId);
            var dropLocation = _context.location_details.FirstOrDefault(l => l.Location_Id == request.DropLocation);

            // Create response DTO
            var response = new InvoiceResponseDTO
            {
                InvoiceId = invoice.InvoiceId,
                InvoiceDate = invoice.InvoiceDate,
                BookingId = invoice.BookingId,
                ActualReturnDate = invoice.ActualReturnDate ?? DateTime.MinValue,
                DropLocation = invoice.DropLocation,
                CarId = invoice.CarId,
                CarRentalAmount = invoice.CarRentalAmount,
                AddonRentalAmount = invoice.AddonRentalAmount,
                TotalAmount = invoice.CarRentalAmount + invoice.AddonRentalAmount,
                FuelStatus = request.FuelStatus,
                CarName = car.CarName ?? "Unknown",
                MemberName = member != null ? $"{member.MemberFirstName} {member.MemberLastName}" : "Unknown",
                DropLocationName = dropLocation?.LocationName ?? "Unknown"
            };

            return response;
        }

        public InvoiceResponseDTO GetInvoiceById(int invoiceId)
        {
            var invoice = _context.invoice_header_table
                .FirstOrDefault(i => i.InvoiceId == invoiceId)
                ?? throw new Exception($"Invoice not found with ID: {invoiceId}");

            // Get related data using separate queries since there are no navigation properties
            var booking = _context.booking_details.FirstOrDefault(b => b.BookingId == invoice.BookingId);
            var car = _context.car_details.FirstOrDefault(c => c.Car_Id == invoice.CarId);
            var member = booking != null ? _context.member_details.FirstOrDefault(m => m.Member_Id == booking.MemberId) : null;
            var dropLocation = _context.location_details.FirstOrDefault(l => l.Location_Id == invoice.DropLocation);

            return new InvoiceResponseDTO
            {
                InvoiceId = invoice.InvoiceId,
                InvoiceDate = invoice.InvoiceDate,
                BookingId = invoice.BookingId,
                ActualReturnDate = invoice.ActualReturnDate ?? DateTime.MinValue,
                DropLocation = invoice.DropLocation,
                CarId = invoice.CarId,
                CarRentalAmount = invoice.CarRentalAmount,
                AddonRentalAmount = invoice.AddonRentalAmount,
                TotalAmount = invoice.CarRentalAmount + invoice.AddonRentalAmount,
                FuelStatus = car?.FuelStatus ?? "Unknown",
                CarName = car?.CarName ?? "Unknown",
                MemberName = member != null ? $"{member.MemberFirstName} {member.MemberLastName}" : "Unknown",
                DropLocationName = dropLocation?.LocationName ?? "Unknown"
            };
        }

        public IEnumerable<InvoiceResponseDTO> GetAllInvoices()
        {
            var invoices = _context.invoice_header_table.ToList();

            var response = new List<InvoiceResponseDTO>();
            foreach (var invoice in invoices)
            {
                // Get related data using separate queries since there are no navigation properties
                var booking = _context.booking_details.FirstOrDefault(b => b.BookingId == invoice.BookingId);
                var car = _context.car_details.FirstOrDefault(c => c.Car_Id == invoice.CarId);
                var member = booking != null ? _context.member_details.FirstOrDefault(m => m.Member_Id == booking.MemberId) : null;
                var dropLocation = _context.location_details.FirstOrDefault(l => l.Location_Id == invoice.DropLocation);

                response.Add(new InvoiceResponseDTO
                {
                    InvoiceId = invoice.InvoiceId,
                    InvoiceDate = invoice.InvoiceDate,
                    BookingId = invoice.BookingId,
                    ActualReturnDate = invoice.ActualReturnDate ?? DateTime.MinValue,
                    DropLocation = invoice.DropLocation,
                    CarId = invoice.CarId,
                    CarRentalAmount = invoice.CarRentalAmount,
                    AddonRentalAmount = invoice.AddonRentalAmount,
                    TotalAmount = invoice.CarRentalAmount + invoice.AddonRentalAmount,
                    FuelStatus = car?.FuelStatus ?? "Unknown",
                    CarName = car?.CarName ?? "Unknown",
                    MemberName = member != null ? $"{member.MemberFirstName} {member.MemberLastName}" : "Unknown",
                    DropLocationName = dropLocation?.LocationName ?? "Unknown"
                });
            }

            return response;
        }

        public IEnumerable<InvoiceResponseDTO> GetInvoicesByBookingId(int bookingId)
        {
            var invoices = _context.invoice_header_table
                .Where(i => i.BookingId == bookingId)
                .ToList();

            var response = new List<InvoiceResponseDTO>();
            foreach (var invoice in invoices)
            {
                // Get related data using separate queries since there are no navigation properties
                var booking = _context.booking_details.FirstOrDefault(b => b.BookingId == invoice.BookingId);
                var car = _context.car_details.FirstOrDefault(c => c.Car_Id == invoice.CarId);
                var member = booking != null ? _context.member_details.FirstOrDefault(m => m.Member_Id == booking.MemberId) : null;
                var dropLocation = _context.location_details.FirstOrDefault(l => l.Location_Id == invoice.DropLocation);

                response.Add(new InvoiceResponseDTO
                {
                    InvoiceId = invoice.InvoiceId,
                    InvoiceDate = invoice.InvoiceDate,
                    BookingId = invoice.BookingId,
                    ActualReturnDate = invoice.ActualReturnDate ?? DateTime.MinValue,
                    DropLocation = invoice.DropLocation,
                    CarId = invoice.CarId,
                    CarRentalAmount = invoice.CarRentalAmount,
                    AddonRentalAmount = invoice.AddonRentalAmount,
                    TotalAmount = invoice.CarRentalAmount + invoice.AddonRentalAmount,
                    FuelStatus = car?.FuelStatus ?? "Unknown",
                    CarName = car?.CarName ?? "Unknown",
                    MemberName = member != null ? $"{member.MemberFirstName} {member.MemberLastName}" : "Unknown",
                    DropLocationName = dropLocation?.LocationName ?? "Unknown"
                });
            }

            return response;
        }
    }
}
