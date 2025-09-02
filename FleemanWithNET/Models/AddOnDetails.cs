using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fleeman_with_dot_net.Models
{

    [Table("add_on_details")]
    public class AddOnDetails
    {
        [Key]
        public int addOnId { get; set; }
        public string addOnName {  get; set; }
        public double addOnPrice { get; set; }


    }
}
