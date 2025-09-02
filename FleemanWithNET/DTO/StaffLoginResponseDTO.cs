namespace fleeman_with_dot_net.DTO
{
    public class StaffLoginResponseDTO
    {
        public string Token { get; set; } = string.Empty;
        public int StaffId { get; set; }
        public string Email { get; set; } = string.Empty;
        public int RoleId { get; set; }
        public int LocationId { get; set; }
        public string StaffFirstName { get; set; } = string.Empty;
        public string StaffLastName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
    }
}
