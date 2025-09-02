using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace fleeman_with_dot_net.Models
{
    [Table("state_details")]
    public class StateDetails
    {
        [Key]
        public int State_Id { get; set; }
        public string State_Name { get; set; }
       

    }
}
