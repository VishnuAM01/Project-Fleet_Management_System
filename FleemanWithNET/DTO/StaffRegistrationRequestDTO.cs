namespace fleeman_with_dot_net.DTO
{
    public class StaffRegistrationRequestDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int LocationId { get; set; }
        public string StaffFirstName { get; set; } = string.Empty;
        public string StaffLastName { get; set; } = string.Empty;
        public long MobileNumber { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public int? ZipCode { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Department { get; set; }
        public string? Position { get; set; }
        public decimal? Salary { get; set; }
    }
}
