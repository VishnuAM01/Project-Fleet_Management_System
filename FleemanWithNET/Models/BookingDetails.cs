using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fleeman_with_dot_net.Models
{
    [Table("booking_details")]
    public class BookingDetails
    {
        [Key]
        public int BookingId { get; set; } 

        public DateTime? BookingDate { get; set; }

        public int? DropLocation { get; set; }

        public DateTime? PickupDate { get; set; }

        public int? PickupLocation { get; set; }

       

        public DateTime? ReturnDate { get; set; }

        public int? VehicleId { get; set; }

        public int? MemberId { get; set; }

        public int? AddonBookId { get; set; }

    
}
}
