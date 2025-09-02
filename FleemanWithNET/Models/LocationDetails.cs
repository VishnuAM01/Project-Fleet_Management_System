using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace fleeman_with_dot_net.Models
{
    [Table("location_details")]
    public class LocationDetails
    {
        [Key]
        public int Location_Id { get; set; }
        public string? Address { get; set; }
        public string? LocationName { get; set; }
        public string? MobileNumber { get; set; }

        public int? Airport_Id { get; set; }

        public string ZipCode { get; set; }
        public int? City_Id { get; set; }
        public int? State_Id { get; set; }

        // Navigation properties
        public CityDetails? City { get; set; }
        public StateDetails? State { get; set; }
    }
}
