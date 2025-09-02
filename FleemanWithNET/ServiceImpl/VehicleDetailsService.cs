using System.Collections.Generic;
using System.Linq;
using fleeman_with_dot_net.Models;
using fleeman_with_dot_net.Repositories;
using fleeman_with_dot_net.Services;

namespace fleeman_with_dot_net.ServiceImpl
{
    public class VehicleDetailsService : IVehicleDetailsService
    {
        private readonly FleetDBContext context;

        public VehicleDetailsService(FleetDBContext context)
        {
            this.context = context;
        }

        public IEnumerable<VehicleDetails> GetAllVehicles()
        {
            return context.vehicle_details.ToList();
        }

        public VehicleDetails GetVehicleById(int id)
        {
            return context.vehicle_details.FirstOrDefault(v => v.VehicleId == id);
        }

        public VehicleDetails AddVehicle(VehicleDetails vehicle)
        {
            context.vehicle_details.Add(vehicle);
            context.SaveChanges();
            return vehicle;
        }

        public VehicleDetails UpdateVehicle(VehicleDetails vehicle)
        {
            var existingVehicle = context.vehicle_details.FirstOrDefault(v => v.VehicleId == vehicle.VehicleId);
            if (existingVehicle == null) return null;

            existingVehicle.VehicleType = vehicle.VehicleType;
            existingVehicle.DailyRate = vehicle.DailyRate;
            existingVehicle.MonthlyRate = vehicle.MonthlyRate;
            existingVehicle.WeeklyRate = vehicle.WeeklyRate;
            existingVehicle.ImgPath = vehicle.ImgPath;

            context.SaveChanges();
            return existingVehicle;
        }

        public bool DeleteVehicle(int id)
        {
            var vehicle = context.vehicle_details.FirstOrDefault(v => v.VehicleId == id);
            if (vehicle == null) return false;

            context.vehicle_details.Remove(vehicle);
            context.SaveChanges();
            return true;
        }
    }
}
