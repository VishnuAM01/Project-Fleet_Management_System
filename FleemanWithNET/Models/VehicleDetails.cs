using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fleeman_with_dot_net.Models
{
    [Table("vehicle_details")]
    public class VehicleDetails
    {
        [Key]
        public int VehicleId {  get; set; }
        public string VehicleType { get; set; }
        public double DailyRate { get; set; }
        public double MonthlyRate { get; set; }
        public double WeeklyRate { get; set; }
        
        public string ImgPath { get; set; }

    }
}
