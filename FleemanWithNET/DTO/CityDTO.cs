namespace fleeman_with_dot_net.DTO
{
    public class CityDTO
    {
        public string City_Name { get; set; }
        public string State_Name { get; set; }

        public CityDTO(string city_Name, string state_Name)
        {
            City_Name = city_Name;
            State_Name = state_Name;
        }
    }

}
