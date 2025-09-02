using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fleeman_with_dot_net.Models
{
    [Table("add_on_book")]  // your join table name
    public class AddOnBook
    {
        [Key]
        public int Id { get; set; }  // Primary key for this join table

        public int BookingId { get; set; }  // Foreign key to booking

        public int add_on_id { get; set; }  // Foreign key to add-on
    }
}
