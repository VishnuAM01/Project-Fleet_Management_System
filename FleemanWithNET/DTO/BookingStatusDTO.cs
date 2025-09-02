namespace fleeman_with_dot_net.DTO
{
    public class BookingStatusDTO
    {
        public int BookingId { get; set; }
        public int MemberId { get; set; }
        public string MemberName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int CarId { get; set; }
        public string CarName { get; set; } = string.Empty;
        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string Status { get; set; } = string.Empty; // "Pending", "In Progress", "Completed"
        public decimal? TotalAmount { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ConfirmationId { get; set; }
        public int? InvoiceId { get; set; }
    }
}
