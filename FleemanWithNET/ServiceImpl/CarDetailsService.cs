using System.Collections.Generic;
using System.Linq;
using fleeman_with_dot_net.Models;
using fleeman_with_dot_net.Repositories;
using fleeman_with_dot_net.Services;
using fleeman_with_dot_net.DTO;
namespace fleeman_with_dot_net.ServiceImpl
{
    public class CarDetailsService : ICarDetailsService
    {
        private readonly FleetDBContext context;

        public CarDetailsService(FleetDBContext context)
        {
            this.context = context;
        }

        public IEnumerable<CarDetails> GetAllCars()
        {
            return context.car_details.ToList();
        }

        public CarDetails GetCarById(int id)
        {
            return context.car_details.FirstOrDefault(c => c.Car_Id == id);
        }

        public CarDetails AddCar(CarDetails car)
        {
            context.car_details.Add(car);
            context.SaveChanges();
            return car;
        }

        public CarDetails UpdateCar(CarDetails car)
        {
            var existingCar = context.car_details.FirstOrDefault(c => c.Car_Id == car.Car_Id);
            if (existingCar == null) return null;

            existingCar.CarName = car.CarName;
            existingCar.FuelStatus = car.FuelStatus;
            existingCar.ImgPath = car.ImgPath;
            existingCar.IsAvailable = car.IsAvailable;
            existingCar.LocationId = car.LocationId;
            existingCar.MaintenanceDate = car.MaintenanceDate;
            existingCar.RegistrationNumber = car.RegistrationNumber;
            existingCar.VehicleId = car.VehicleId;

            context.SaveChanges();
            return existingCar;
        }

        public bool DeleteCar(int id)
        {
            var car = context.car_details.FirstOrDefault(c => c.Car_Id == id);
            if (car == null) return false;

            context.car_details.Remove(car);
            context.SaveChanges();
            return true;
        }
        public IEnumerable<CarDetails> GetCarsByVehicleId(int vehicleId)
        {
            return context.car_details.Where(c => c.VehicleId == vehicleId).ToList();
        }

        public IEnumerable<CarDetails> GetAvailableCarsByVehicleId(int vehicleId)
        {
            return context.car_details
                .Where(c => c.VehicleId == vehicleId && c.IsAvailable == true)
                .ToList();
        }

        public IEnumerable<AvailableCarDTO> GetAvailableCarsWithDetailsByVehicleId(int vehicleId)
        {
            var availableCars = (from car in context.car_details
                                join vehicle in context.vehicle_details on car.VehicleId equals vehicle.VehicleId
                                join location in context.location_details on car.LocationId equals location.Location_Id into locationJoin
                                from loc in locationJoin.DefaultIfEmpty()
                                join city in context.city_details on loc.City_Id equals city.City_Id into cityJoin
                                from cty in cityJoin.DefaultIfEmpty()
                                join state in context.state_details on loc.State_Id equals state.State_Id into stateJoin
                                from st in stateJoin.DefaultIfEmpty()
                                where car.VehicleId == vehicleId && car.IsAvailable == true
                                select new AvailableCarDTO
                                {
                                    CarId = car.Car_Id,
                                    CarName = car.CarName,
                                    FuelStatus = car.FuelStatus,
                                    ImgPath = car.ImgPath,
                                    IsAvailable = car.IsAvailable ?? false,
                                    LocationId = car.LocationId,
                                    MaintenanceDate = car.MaintenanceDate,
                                    RegistrationNumber = car.RegistrationNumber,
                                    VehicleId = car.VehicleId,
                                    
                                    // Vehicle details
                                    VehicleType = vehicle.VehicleType,
                                    DailyRate = vehicle.DailyRate,
                                    WeeklyRate = vehicle.WeeklyRate,
                                    MonthlyRate = vehicle.MonthlyRate,
                                    
                                    // Location details
                                    LocationName = loc.LocationName,
                                    CityName = cty.City_Name,
                                    StateName = st.State_Name,
                                    
                                    Status = "Available"
                                }).ToList();

            return availableCars;
        }
    }
}
