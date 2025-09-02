namespace fleeman_with_dot_net.DTO
{
    public class UserDetailsDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public string HomePhone { get; set; }
        public string Cell { get; set; }
        public string DrivingLicense { get; set; }
        public string DlIssuedBy { get; set; }
        public DateTime? DlValidThru { get; set; }
        public string IdpNo { get; set; }
        public string IdpIssuedBy { get; set; }
        public DateTime? IdpValidThru { get; set; }
        public string PassportNo { get; set; }
        public string PassportIssuedBy { get; set; }
        public DateTime? PassportValid { get; set; }
        public DateTime? BirthDate { get; set; }
        public string CreditCardType { get; set; }  // as string, parse later to enum
        public string CreditCardNo { get; set; }    // if required
    }
     
    
}
