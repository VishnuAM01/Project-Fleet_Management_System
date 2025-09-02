using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace fleeman_with_dot_net.Models
{
    [Table("car_details")]
    public class CarDetails
    {
        [Key]
        public int Car_Id { get; set; }

        public string? CarName { get; set; }

        public string? FuelStatus { get; set; }

        public string? ImgPath { get; set; }

        public bool? IsAvailable { get; set; }

        public int? LocationId { get; set; }

        public DateTime? MaintenanceDate { get; set; }

        public string? RegistrationNumber { get; set; }

        public int? VehicleId { get; set; }


    }
}
