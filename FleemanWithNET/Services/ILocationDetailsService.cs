using fleeman_with_dot_net.DTO;
using fleeman_with_dot_net.Models;
using System.Collections.Generic;

namespace fleeman_with_dot_net.Services
{
    public interface ILocationDetailsService
    {

        public object GetLocationsByCity(string cityName);
        public object GetLocationsByAirport(string airport);


    }
}
