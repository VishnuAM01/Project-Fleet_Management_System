using fleeman_with_dot_net.DTO;
using fleeman_with_dot_net.Models;
using fleeman_with_dot_net.Repositories;
using Microsoft.EntityFrameworkCore;

namespace fleeman_with_dot_net.Services
{
    public class BookingConfirmationDetailsService : IBookingConfirmationDetailsService
    {
        private readonly FleetDBContext _context;

        public BookingConfirmationDetailsService(FleetDBContext context)
        {
            _context = context;
        }

        public BookingConfirmationDetails Create(BookingConfirmationDTO dto)
        {
            // Get car by ID
            var car = _context.car_details.FirstOrDefault(c => c.Car_Id == dto.CarId)
                ?? throw new Exception($"Car not found with ID: {dto.CarId}");

            // Check if already booked
            if (car.IsAvailable == false)
            {
                throw new Exception($"Car with ID {dto.CarId} is already booked.");
            }

            // Set car availability to false
            car.IsAvailable = false;
            _context.SaveChanges();

            // Create booking confirmation
            var confirmation = new BookingConfirmationDetails
            {
                BookingId = dto.BookingId,
                Car_Id = dto.CarId
            };

            _context.booking_confirmation_details.Add(confirmation);
            _context.SaveChanges();

            return confirmation;
        }

        public BookingConfirmationDetails GetById(int bookingId)
        {
            return _context.booking_confirmation_details
                .Include(b => b.Car)
                .Include(b => b.Booking)
                .FirstOrDefault(b => b.BookingId == bookingId)
                ?? throw new Exception("Booking confirmation not found");
        }

        public IEnumerable<BookingConfirmationDetails> GetAll()
        {
            return _context.booking_confirmation_details
                .Include(b => b.Car)
                .Include(b => b.Booking)
                .ToList();
        }

        public BookingConfirmationDetails Update(int bookingId, BookingConfirmationDetails updated)
        {
            var existing = _context.booking_confirmation_details
                .FirstOrDefault(b => b.BookingId == bookingId)
                ?? throw new Exception($"Booking confirmation not found with id: {bookingId}");

            existing.Car_Id = updated.Car_Id;
            _context.SaveChanges();

            return existing;
        }

        public void Delete(int bookingId)
        {
            var confirmation = _context.booking_confirmation_details
                .Include(b => b.Car)
                .FirstOrDefault(b => b.BookingId == bookingId);

            if (confirmation != null)
            {
                // Restore availability
                if (confirmation.Car != null)
                {
                    confirmation.Car.IsAvailable = true;
                }

                _context.booking_confirmation_details.Remove(confirmation);
                _context.SaveChanges();
            }
        }

        public IEnumerable<ConfirmedBookingDTO> GetConfirmedBookingsWithDetails()
        {
            var confirmedBookings = (from bcd in _context.booking_confirmation_details
                                   join bd in _context.booking_details on bcd.BookingId equals bd.BookingId
                                   join cd in _context.car_details on bcd.Car_Id equals cd.Car_Id
                                   join md in _context.member_details on bd.MemberId equals md.Member_Id into memberJoin
                                   from member in memberJoin.DefaultIfEmpty()
                                   join pickupLoc in _context.location_details on bd.PickupLocation equals pickupLoc.Location_Id into pickupJoin
                                   from pickup in pickupJoin.DefaultIfEmpty()
                                   join dropLoc in _context.location_details on bd.DropLocation equals dropLoc.Location_Id into dropJoin
                                   from drop in dropJoin.DefaultIfEmpty()
                                   join vd in _context.vehicle_details on bd.VehicleId equals vd.VehicleId into vehicleJoin
                                   from vehicle in vehicleJoin.DefaultIfEmpty()
                                   select new ConfirmedBookingDTO
                                   {
                                       // Primary keys
                                       BookingId = bcd.BookingId,
                                       CarId = bcd.Car_Id,
                                       
                                       // Booking details
                                       BookingDate = bd.BookingDate,
                                       PickupDate = bd.PickupDate,
                                       ReturnDate = bd.ReturnDate,
                                       PickupLocation = bd.PickupLocation,
                                       DropLocation = bd.DropLocation,
                                       VehicleId = bd.VehicleId,
                                       MemberId = bd.MemberId,
                                       
                                       // Car details
                                       CarName = cd.CarName,
                                       CarLicensePlate = cd.RegistrationNumber,
                                       
                                       // Member details
                                       MemberFirstName = member.MemberFirstName,
                                       MemberLastName = member.MemberLastName,
                                       Email = member.Email,
                                       MobileNumber = member.MobileNumber.ToString(),
                                       
                                       // Location details
                                       PickupLocationName = pickup.LocationName,
                                       DropLocationName = drop.LocationName,
                                       
                                       // Vehicle details
                                       VehicleName = vehicle.VehicleType,
                                       
                                       
                                       Status = "Confirmed"
                                   }).ToList();

            return confirmedBookings;
        }

        public IEnumerable<ConfirmedBookingDTO> GetConfirmedBookingsWithDetails(int? carId, int? memberId, DateTime? fromDate, DateTime? toDate)
        {
            var query = from bcd in _context.booking_confirmation_details
                       join bd in _context.booking_details on bcd.BookingId equals bd.BookingId
                       join cd in _context.car_details on bcd.Car_Id equals cd.Car_Id
                       join md in _context.member_details on bd.MemberId equals md.Member_Id into memberJoin
                       from member in memberJoin.DefaultIfEmpty()
                       join pickupLoc in _context.location_details on bd.PickupLocation equals pickupLoc.Location_Id into pickupJoin
                       from pickup in pickupJoin.DefaultIfEmpty()
                       join dropLoc in _context.location_details on bd.DropLocation equals dropLoc.Location_Id into dropJoin
                       from drop in dropJoin.DefaultIfEmpty()
                       join vd in _context.vehicle_details on bd.VehicleId equals vd.VehicleId into vehicleJoin
                       from vehicle in vehicleJoin.DefaultIfEmpty()
                       select new ConfirmedBookingDTO
                       {
                           // Primary keys
                           BookingId = bcd.BookingId,
                           CarId = bcd.Car_Id,
                           
                           // Booking details
                           BookingDate = bd.BookingDate,
                           PickupDate = bd.PickupDate,
                           ReturnDate = bd.ReturnDate,
                           PickupLocation = bd.PickupLocation,
                           DropLocation = bd.DropLocation,
                           VehicleId = bd.VehicleId,
                           MemberId = bd.MemberId,
                           
                           // Car details
                           CarName = cd.CarName,
                           CarLicensePlate = cd.RegistrationNumber,
                           
                           // Member details
                           MemberFirstName = member.MemberFirstName,
                           MemberLastName = member.MemberLastName,
                           Email = member.Email,
                           MobileNumber = member.MobileNumber.ToString(),
                           
                           // Location details
                           PickupLocationName = pickup.LocationName,
                           DropLocationName = drop.LocationName,
                           
                           // Vehicle details
                           VehicleName = vehicle.VehicleType,
                           
                           Status = "Confirmed"
                       };

            // Apply filters
            if (carId.HasValue)
            {
                query = query.Where(b => b.CarId == carId.Value);
            }

            if (memberId.HasValue)
            {
                query = query.Where(b => b.MemberId == memberId.Value);
            }

            if (fromDate.HasValue)
            {
                query = query.Where(b => b.BookingDate >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                query = query.Where(b => b.BookingDate <= toDate.Value);
            }

            return query.ToList();
        }
    }
}
