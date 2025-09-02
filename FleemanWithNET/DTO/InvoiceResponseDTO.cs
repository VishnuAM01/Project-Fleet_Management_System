namespace fleeman_with_dot_net.DTO
{
    public class InvoiceResponseDTO
    {
        public int InvoiceId { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int BookingId { get; set; }
        public DateTime ActualReturnDate { get; set; }
        public int DropLocation { get; set; }
        public int CarId { get; set; }
        public double CarRentalAmount { get; set; }
        public double AddonRentalAmount { get; set; }
        public double TotalAmount { get; set; }
        public string FuelStatus { get; set; }
        public string CarName { get; set; }
        public string MemberName { get; set; }
        public string DropLocationName { get; set; }
    }
}
