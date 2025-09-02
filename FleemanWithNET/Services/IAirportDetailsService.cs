using fleeman_with_dot_net.Models;
using System.Collections.Generic;
namespace fleeman_with_dot_net.Services
{
    public interface IAirportDetailsService
    {
        IEnumerable<AirportDetails> GetAllAirport();
        AirportDetails GetAirportById(int id);
        AirportDetails AddAirport(AirportDetails airport);
        AirportDetails UpdateAirport(AirportDetails airport);
        bool DeleteAirport(int id);


    }
}
