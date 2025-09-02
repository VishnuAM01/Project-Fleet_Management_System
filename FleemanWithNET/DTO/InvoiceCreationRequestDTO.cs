using System.ComponentModel.DataAnnotations;

namespace fleeman_with_dot_net.DTO
{
    public class InvoiceCreationRequestDTO
    {
        [Required]
        public int CarId { get; set; }
        
        [Required]
        public string FuelStatus { get; set; }
        
        [Required]
        public int BookingId { get; set; }
        
        [Required]
        public int DropLocation { get; set; }
    }
}
