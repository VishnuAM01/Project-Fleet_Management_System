using System.Collections.Generic;
using fleeman_with_dot_net.Models;

namespace fleeman_with_dot_net.Services
{
    public interface IVehicleDetailsService
    {
        IEnumerable<VehicleDetails> GetAllVehicles();
        VehicleDetails GetVehicleById(int id);
        VehicleDetails AddVehicle(VehicleDetails vehicle);
        VehicleDetails UpdateVehicle(VehicleDetails vehicle);
        bool DeleteVehicle(int id);
    }
}
