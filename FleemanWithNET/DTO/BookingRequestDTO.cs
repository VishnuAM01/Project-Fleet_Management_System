using fleeman_with_dot_net.Models;

namespace fleeman_with_dot_net.DTO
{

    public class BookingRequestDTO
    {
        public DateTime? PickupDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public LocationDTO PickupLocation { get; set; }
        public LocationDTO ReturnLocation { get; set; }

        public string SelectedHub { get; set; }
        public string SelectedReturnHub { get; set; }

        public int SelectedVehicle { get; set; }
        public List<int> SelectedAddons { get; set; } = new();

        public UserDetailsDTO UserDetails { get; set; }

        public DateTime? SubmittedAt { get; set; }
    }
}
