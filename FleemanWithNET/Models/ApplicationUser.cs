using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace fleeman_with_dot_net.Models
{
    public enum UserType
    {
        Member,
        Staff
    }
    [Table("application_user")]
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
        
        // Navigation properties
        public MemberDetails? MemberDetails { get; set; }
        public StaffDetails? StaffDetails { get; set; }
        
        // User type to distinguish between Member and Staff
        public UserType UserType { get; set; } = UserType.Member;
    }
}
