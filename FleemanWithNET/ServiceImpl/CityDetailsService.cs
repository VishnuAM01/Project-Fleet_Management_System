using fleeman_with_dot_net.DTO;
using fleeman_with_dot_net.Models;
using fleeman_with_dot_net.Repositories;
using fleeman_with_dot_net.Services;
using Microsoft.EntityFrameworkCore;

namespace fleeman_with_dot_net.ServiceImpl
{
    public class CityDetailsService : ICityDetailsService
    {
        private readonly FleetDBContext context;

        public CityDetailsService(FleetDBContext context)
        {
            this.context = context;
        }

        public CityDetails AddCity(CityDetails city)
        {
            context.city_details.Add(city);
            context.SaveChanges();
            return city;
        }

        public IEnumerable<CityDetails> GetAllCities()
        {
            return context.city_details.ToList();
        }

        public CityDetails GetCityById(int id)
        {
            return context.city_details.FirstOrDefault(c => c.City_Id == id);
        }

        public CityDetails UpdateCity(CityDetails city)
        {
            var existingCity = context.city_details.FirstOrDefault(c => c.City_Id == city.City_Id);
            if (existingCity == null)
            {
                return null;
            }

            existingCity.City_Name = city.City_Name;
            existingCity.State_Id = city.State_Id;

            context.SaveChanges();
            return existingCity;
        }

        public bool DeleteCity(int id)
        {
            var city = context.city_details.FirstOrDefault(c => c.City_Id == id);
            if (city == null)
            {
                return false;
            }

            context.city_details.Remove(city);
            context.SaveChanges();
            return true;
        }

        public IEnumerable<string> GetCitiesByStateName(string stateName)
        {
            return context.city_details
                .Join(
                    context.state_details,
                    city => city.State_Id,
                    state => state.State_Id,
                    (city, state) => new { city, state }
                )
                .Where(cs => cs.state.State_Name == stateName)
                .Select(cs => cs.city.City_Name)  
                .ToList();
        }

        public List<CityDTO> GetAllCity()
        {
            return context.city_details
                .Include(c => c.State)
                .Select(c => new CityDTO(
                    c.City_Name,
                    c.State.State_Name
                ))
                .ToList();
        }


    }
}
