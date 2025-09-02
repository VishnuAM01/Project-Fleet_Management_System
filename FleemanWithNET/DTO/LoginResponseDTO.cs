namespace fleeman_with_dot_net.DTO
{
    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public int MemberId { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
    }
}
