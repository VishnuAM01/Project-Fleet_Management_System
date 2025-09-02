using fleeman_with_dot_net.DTO;
using fleeman_with_dot_net.Models;

namespace fleeman_with_dot_net.Services
{
    public interface IUser
    {
        Task Register(MemberRegistrationRequest user);
        Task<bool> ValidateUser(AuthenticationTable user);
        Task<AuthenticationTable> GetUserByMemberId(string memberId);
        Task<LoginResponseDTO> GetUserDetailsForLogin(string email);
        
        // Staff methods
        Task<bool> ValidateStaff(StaffLoginRequestDTO staffLogin);
        Task<StaffLoginResponseDTO> GetStaffDetailsForLogin(string email);
        Task RegisterStaff(StaffRegistrationRequestDTO staffRegistration);
    }
}
