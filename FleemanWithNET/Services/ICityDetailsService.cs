using fleeman_with_dot_net.DTO;
using fleeman_with_dot_net.Models;

namespace fleeman_with_dot_net.Services
{
    public interface ICityDetailsService
    {
        CityDetails AddCity(CityDetails city);
        IEnumerable<CityDetails> GetAllCities();
        CityDetails GetCityById(int id);
        CityDetails UpdateCity(CityDetails city);
        bool DeleteCity(int id);
        public IEnumerable<string> GetCitiesByStateName(string stateName);
        List<CityDTO> GetAllCity();
    }
}
