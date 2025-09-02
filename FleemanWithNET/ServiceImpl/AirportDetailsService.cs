using fleeman_with_dot_net.Models;
using fleeman_with_dot_net.Repositories;
using fleeman_with_dot_net.Services;

namespace fleeman_with_dot_net.ServiceImpl
{
    public class AirportDetailsService : IAirportDetailsService
    {
        private readonly FleetDBContext context;

        public AirportDetailsService(FleetDBContext context) 
        {
            this.context = context;
        }

        public AirportDetails AddAirport(AirportDetails airport)
        {
            context.airport_details.Add(airport);
            context.SaveChanges();
            return airport;
        }

        public IEnumerable<AirportDetails> GetAllAirport()
        {
            return context.airport_details.ToList();
        }
        public AirportDetails GetAirportById(int id)
        {
            return context.airport_details.FirstOrDefault(a => a.Airport_Id == id);
        }

        public AirportDetails UpdateAirport(AirportDetails airport)
        {
            var existAirport = context.airport_details.FirstOrDefault(a => a.Airport_Id == airport.Airport_Id);
            if (existAirport == null)
            {
                return null;
            }

            existAirport.Airport_Code=airport.Airport_Code;
            existAirport.Airport_Name = airport.Airport_Name;

            context.SaveChanges();
            return existAirport;
        }

        public bool DeleteAirport(int id)
        {
            var airport=context.airport_details.FirstOrDefault(a=>a.Airport_Id==id);
            if(airport == null)
            {
                return false;
            }

            context.airport_details.Remove(airport);
            context.SaveChanges();
            return true;
        }
    }
}
