using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace fleeman_with_dot_net.Models
{
    [Table("city_details")]
    public class CityDetails
    {
        [Key]
        public int City_Id { set; get; }
        public string City_Name { set; get; }

        [ForeignKey("State")] // This points to the navigation property
        public int State_Id { get; set; }

        [JsonIgnore]
        public StateDetails State { get; set; }

    }
}
