using fleeman_with_dot_net.DTO;
using fleeman_with_dot_net.Models;
using fleeman_with_dot_net.Repositories;
using fleeman_with_dot_net.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace fleeman_with_dot_net.ServiceImpl
{
    public class LocationDetailsService : ILocationDetailsService
    {
        private readonly FleetDBContext context;

        public LocationDetailsService(FleetDBContext context)
        {
            this.context = context;
        }



        public object GetLocationsByCity(string cityName)
        {
            // Get the city
            var city = context.city_details
                              .FirstOrDefault(c => c.City_Name == cityName);

            if (city == null)
                return null;

            // Get all locations for that city
            var locations = context.location_details
                                   .Where(l => l.City_Id == city.City_Id)
                                   .Select(l => new
                                   {
                                       name = l.LocationName,
                                       address = l.Address,
                                       mobileNumber = l.MobileNumber
                                   })
                                   .ToList();

            // Build final response
            var result = new
            {
                city = city.City_Name,
                locationNames = locations
            };

            return result;
        }

        public object GetLocationsByAirport(string airportCode)
        {
            // Get the city
            var airport = context.airport_details
                              .FirstOrDefault(c => c.Airport_Code == airportCode);

            if (airport == null)
                return null;

            // Get all locations for that city
            var locations = context.location_details
                                   .Where(l => l.Airport_Id == airport.Airport_Id)
                                   .Select(l => new
                                   {
                                       name = l.LocationName,
                                       address = l.Address,
                                       mobileNumber = l.MobileNumber
                                   })
                                   .ToList();

            // Build final response
            var result = new
            {
                  // now city is a string
                airport = airport.Airport_Code,        // hardcoded for example
                locationNames = locations
            };

            return result;
        }



        //public List<LocationDetails> GetLocationsByCity(string city)
        //{

        //    var city1 = context.city_details
        //    .FirstOrDefault(c => c.City_Name == city);
        //    var locations = context.location_details
        //    .Where(l => l.City_Id == city1.City_Id)
        //    .ToList();

        //    return locations;


        //}




    }
}
