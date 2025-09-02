using fleeman_with_dot_net.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fleeman_with_dot_net.Models
{
    [Table("member_details")]
    public class MemberDetails
    {
        [Key]
        public int Member_Id { get; set; }

        public string? Address { get; set; }
        public string? City { get; set; }
       
        public CreditCardType? CreditCard { get; set; }
        public DateTime? Dob { get; set; }

        [Required]
        [StringLength(20)]
        public string DrivingLicenseId { get; set; } = null!;

        public DateTime? DrivingLicenseIssuedBy { get; set; }
        public DateTime? DrivingLicenseValidThru { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(60)]
        public string MemberFirstName { get; set; } = null!;

        public string? Idp { get; set; }
        public DateTime? IdpIssuedBy { get; set; }
        public DateTime? IdpValidThru { get; set; }

        [Required]
        [StringLength(60)]
        public string MemberLastName { get; set; } = null!;

        [Required]
        public long MobileNumber { get; set; }

        public string? PassportIssuedBy { get; set; }
        public string? PassportNo { get; set; }
        public DateTime? PassportValidate { get; set; }
        public string? State { get; set; }
        public int? ZipCode { get; set; }

      
    }

    public enum CreditCardType
    {
        AMEX = 0,
        DISCOVER = 1,
        MASTERCARD = 2,
        VISA = 3
    }
}
