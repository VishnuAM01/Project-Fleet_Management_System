using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fleeman_with_dot_net.Models
{
    [Table("staff_details")]
    public class StaffDetails
    {
        [Key]
        public int Staff_Id { get; set; }

        [Required]
        [StringLength(60)]
        public string StaffFirstName { get; set; } = null!;

        [Required]
        [StringLength(60)]
        public string StaffLastName { get; set; } = null!;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = null!;

        [Required]
        public long MobileNumber { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public int? ZipCode { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public DateTime HireDate { get; set; } = DateTime.UtcNow;

        public string? Department { get; set; }

        public string? Position { get; set; }

        public decimal? Salary { get; set; }

        public bool IsActive { get; set; } = true;

        public int LocationId { get; set; }

        // Navigation property for authentication
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
