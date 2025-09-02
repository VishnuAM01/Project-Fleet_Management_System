using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fleeman_with_dot_net.Models
{
    public class BookingConfirmationDetails
    {
        [Key]
        public int BookingConfirmationDetailsId { get; set; }
        public int BookingId { get; set; }


        [ForeignKey(nameof(BookingId))]
        public virtual BookingDetails Booking { get; set; }

        public int Car_Id { get; set; }

        [ForeignKey(nameof(Car_Id))]
        public virtual CarDetails Car { get; set; }
    }
}

