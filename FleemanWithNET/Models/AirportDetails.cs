using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fleeman_with_dot_net.Models
{
    [Table("airport_details")]
    public class AirportDetails
    {
        [Key]
        public int Airport_Id { set; get; }

        public string Airport_Code { set; get; }
        public string Airport_Name { set; get; }

    }
}
