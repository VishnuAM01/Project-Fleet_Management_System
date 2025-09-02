namespace fleeman_with_dot_net.DTO
{
    public class AvailableCarDTO
    {
        public int CarId { get; set; }
        public string CarName { get; set; }
        public string FuelStatus { get; set; }
        public string ImgPath { get; set; }
        public bool IsAvailable { get; set; }
        public int? LocationId { get; set; }
        public DateTime? MaintenanceDate { get; set; }
        public string RegistrationNumber { get; set; }
        public int? VehicleId { get; set; }
        
        // Vehicle details
        public string VehicleType { get; set; }
        public double DailyRate { get; set; }
        public double WeeklyRate { get; set; }
        public double MonthlyRate { get; set; }
        
        // Location details
        public string LocationName { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        
        // Status
        public string Status { get; set; } = "Available";
    }
}
