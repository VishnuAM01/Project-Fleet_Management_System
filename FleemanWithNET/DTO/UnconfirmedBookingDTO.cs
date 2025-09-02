using fleeman_with_dot_net.Models;

namespace fleeman_with_dot_net.DTO
{
    public class UnconfirmedBookingDTO
    {
        public int BookingId { get; set; }
        public DateTime? BookingDate { get; set; }
        public DateTime? PickupDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int? PickupLocation { get; set; }
        public int? DropLocation { get; set; }
        public int? VehicleId { get; set; }
        public int? MemberId { get; set; }
        public int? AddonBookId { get; set; }

        // Related member information
        public string MemberFirstName { get; set; }
        public string MemberLastName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }

        // Related location information
        public string PickupLocationName { get; set; }
        public string DropLocationName { get; set; }

        // Related vehicle information
        public string VehicleName { get; set; }
        public string VehicleModel { get; set; }

        // Status
        public string Status { get; set; } = "Unconfirmed";
    }
}
