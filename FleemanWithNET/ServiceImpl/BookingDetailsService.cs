using fleeman_with_dot_net.DTO;
using fleeman_with_dot_net.Models;
using fleeman_with_dot_net.Repositories;
using fleeman_with_dot_net.Services;
using System.Collections.Generic;
using System.Linq;

namespace fleeman_with_dot_net.ServiceImpl
{
    public class BookingDetailsService : IBookingDetailsService
    {
        private readonly FleetDBContext context;

        public BookingDetailsService(FleetDBContext context)
        {
            this.context = context;
        }

        public IEnumerable<BookingDetails> GetAllBookings()
        {
            return context.booking_details.ToList();
        }

        public BookingDetails GetBookingById(int id)
        {
            return context.booking_details.FirstOrDefault(b => b.BookingId == id);
        }

        public BookingDetails AddBooking(BookingRequestDTO request)
        {
         
            
                // Create new member from UserDetailsDTO
              var  member = new MemberDetails
                {
                    Address = request.UserDetails.Address1 + " " + request.UserDetails.Address2,
                    City = request.UserDetails.City,
                    Email = request.UserDetails.Email,
                    DrivingLicenseId = request.UserDetails.DrivingLicense,
                    MemberFirstName = request.UserDetails.FirstName,
                    MemberLastName = request.UserDetails.LastName,
                    MobileNumber = long.TryParse(request.UserDetails.Cell, out var cellNum) ? cellNum : 0,
                    Dob = request.UserDetails.BirthDate,
                    PassportNo = request.UserDetails.PassportNo,
                    PassportIssuedBy = request.UserDetails.PassportIssuedBy,
                    PassportValidate = request.UserDetails.PassportValid,
                    State = request.UserDetails.City, // or other appropriate field
                    ZipCode = int.TryParse(request.UserDetails.Zip, out var zip) ? zip : (int?)null,
                    DrivingLicenseIssuedBy = ParseDateOrNull(request.UserDetails.DlIssuedBy),
                    DrivingLicenseValidThru = request.UserDetails.DlValidThru,
                    Idp = request.UserDetails.IdpNo,
                    IdpIssuedBy = ParseDateOrNull(request.UserDetails.IdpIssuedBy),

                    IdpValidThru = request.UserDetails.IdpValidThru,
                    CreditCard = ParseCreditCardType(request.UserDetails.CreditCardType)
                };

                context.member_details.Add(member);
                context.SaveChanges();

            var pickupId = context.location_details.FirstOrDefault(u => u.LocationName == request.SelectedHub);
            var dropId = context.location_details.FirstOrDefault(u => u.LocationName == request.SelectedReturnHub);
            // Create booking entity
            var booking = new BookingDetails
            {
                BookingDate = DateTime.Now,
                PickupDate = request.PickupDate,
               
                PickupLocation = pickupId.Location_Id,
                ReturnDate = request.ReturnDate,
                DropLocation = dropId.Location_Id,
                VehicleId = request.SelectedVehicle,
                MemberId = member.Member_Id
            };

            context.booking_details.Add(booking);
            context.SaveChanges();

            // Add selected AddOns
            if (request.SelectedAddons != null)
            {
                foreach (var addOnId in request.SelectedAddons)
                {
                    var addOnBook = new AddOnBook
                    {
                        BookingId = booking.BookingId,
                        add_on_id = addOnId
                    };
                    context.add_on_book.Add(addOnBook);
                }
                context.SaveChanges();
            }

            return booking;
        }

        // Helper methods:

        private DateTime? ParseDateOrNull(string dateString)
        {
            if (DateTime.TryParse(dateString, out var date))
                return date;
            return null;
        }

        private CreditCardType? ParseCreditCardType(string creditCardType)
        {
            if (Enum.TryParse(typeof(CreditCardType), creditCardType, true, out var cc))
                return (CreditCardType)cc;
            return null;
        }





        public BookingDetails UpdateBooking(BookingDetails booking)
        {
            var existingBooking = context.booking_details.FirstOrDefault(b => b.BookingId == booking.BookingId);
            if (existingBooking == null)
                return null;

            existingBooking.BookingDate = booking.BookingDate;
            existingBooking.DropLocation = booking.DropLocation;
            existingBooking.PickupDate = booking.PickupDate;
            existingBooking.PickupLocation = booking.PickupLocation;
        
            existingBooking.ReturnDate = booking.ReturnDate;
            existingBooking.VehicleId = booking.VehicleId;
            existingBooking.MemberId = booking.MemberId;

            context.SaveChanges();
            return existingBooking;
        }

        public bool DeleteBooking(int id)
        {
            var booking = context.booking_details.FirstOrDefault(b => b.BookingId == id);
            if (booking == null)
                return false;

            context.booking_details.Remove(booking);
            context.SaveChanges();
            return true;
        }

        public IEnumerable<UnconfirmedBookingDTO> GetUnconfirmedBookings()
        {
            // Get all booking IDs that exist in booking_confirmation_details
            var confirmedBookingIds = context.booking_confirmation_details
                .Select(bcd => bcd.BookingId)
                .ToList();

            // Get all bookings that are NOT in the confirmed list with related data using joins
            var unconfirmedBookings = (from bd in context.booking_details
                                     join md in context.member_details on bd.MemberId equals md.Member_Id into memberJoin
                                     from member in memberJoin.DefaultIfEmpty()
                                     join pickupLoc in context.location_details on bd.PickupLocation equals pickupLoc.Location_Id into pickupJoin
                                     from pickup in pickupJoin.DefaultIfEmpty()
                                     join dropLoc in context.location_details on bd.DropLocation equals dropLoc.Location_Id into dropJoin
                                     from drop in dropJoin.DefaultIfEmpty()
                                     join vd in context.vehicle_details on bd.VehicleId equals vd.VehicleId into vehicleJoin
                                     from vehicle in vehicleJoin.DefaultIfEmpty()
                                     where !confirmedBookingIds.Contains(bd.BookingId)
                                     select new UnconfirmedBookingDTO
                                     {
                                         BookingId = bd.BookingId,
                                         BookingDate = bd.BookingDate,
                                         PickupDate = bd.PickupDate,
                                         ReturnDate = bd.ReturnDate,
                                         PickupLocation = bd.PickupLocation,
                                         DropLocation = bd.DropLocation,
                                         VehicleId = bd.VehicleId,
                                         MemberId = bd.MemberId,
                                         AddonBookId = bd.AddonBookId,
                                         
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
                                         
                                         Status = "Unconfirmed"
                                     }).ToList();

            return unconfirmedBookings;
        }

        public IEnumerable<UnconfirmedBookingDTO> GetUnconfirmedBookings(DateTime? fromDate, DateTime? toDate, int? memberId)
        {
            // Get all booking IDs that exist in booking_confirmation_details
            var confirmedBookingIds = context.booking_confirmation_details
                .Select(bcd => bcd.BookingId)
                .ToList();

            // Build the base query
            var query = from bd in context.booking_details
                       join md in context.member_details on bd.MemberId equals md.Member_Id into memberJoin
                       from member in memberJoin.DefaultIfEmpty()
                       join pickupLoc in context.location_details on bd.PickupLocation equals pickupLoc.Location_Id into pickupJoin
                       from pickup in pickupJoin.DefaultIfEmpty()
                       join dropLoc in context.location_details on bd.DropLocation equals dropLoc.Location_Id into dropJoin
                       from drop in dropJoin.DefaultIfEmpty()
                       join vd in context.vehicle_details on bd.VehicleId equals vd.VehicleId into vehicleJoin
                       from vehicle in vehicleJoin.DefaultIfEmpty()
                       where !confirmedBookingIds.Contains(bd.BookingId)
                       select new UnconfirmedBookingDTO
                       {
                           BookingId = bd.BookingId,
                           BookingDate = bd.BookingDate,
                           PickupDate = bd.PickupDate,
                           ReturnDate = bd.ReturnDate,
                           PickupLocation = bd.PickupLocation,
                           DropLocation = bd.DropLocation,
                           VehicleId = bd.VehicleId,
                           MemberId = bd.MemberId,
                           AddonBookId = bd.AddonBookId,
                           
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
                           
                           Status = "Unconfirmed"
                       };

            // Apply filters
            if (fromDate.HasValue)
            {
                query = query.Where(b => b.BookingDate >= fromDate.Value);
            }

            if (toDate.HasValue)
            {
                query = query.Where(b => b.BookingDate <= toDate.Value);
            }

            if (memberId.HasValue)
            {
                query = query.Where(b => b.MemberId == memberId.Value);
            }

            return query.ToList();
        }
    }
}
