using fleeman_with_dot_net.Models;

namespace fleeman_with_dot_net.DTO
{
    public class ConfirmedBookingDTO
    {
        public int BookingId { get; set; }
        public int CarId { get; set; }
        
        // Booking details
        public DateTime? BookingDate { get; set; }
        public DateTime? PickupDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int? PickupLocation { get; set; }
        public int? DropLocation { get; set; }
        public int? VehicleId { get; set; }
        public int? MemberId { get; set; }
        
        // Car details
        public string CarName { get; set; }
        public string CarModel { get; set; }
        public string CarBrand { get; set; }
        public int? CarYear { get; set; }
        public string CarColor { get; set; }
        public string CarLicensePlate { get; set; }
        
        // Member details
        public string MemberFirstName { get; set; }
        public string MemberLastName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        
        // Location details
        public string PickupLocationName { get; set; }
        public string DropLocationName { get; set; }
        
        // Vehicle details
        public string VehicleName { get; set; }
        public string VehicleModel { get; set; }
        
        // Status
        public string Status { get; set; } = "Confirmed";
    }
}
