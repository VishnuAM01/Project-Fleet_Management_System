using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace fleeman_with_dot_net.Models
{
    public class InvoiceHeaderTable
    {
        [Key]
        public int InvoiceId { get; set; }

        public DateTime InvoiceDate { get; set; }

        public int BookingId { get; set; }

        public DateTime? ActualReturnDate { get; set; } 

       
        public int DropLocation { get; set; }

        public int CarId { get; set; }

        public double CarRentalAmount { get; set; }

        public double AddonRentalAmount { get; set; }
    }
}
