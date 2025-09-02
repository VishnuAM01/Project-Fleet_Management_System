using System.Collections.Generic;
using fleeman_with_dot_net.Models;
using fleeman_with_dot_net.DTO;

namespace fleeman_with_dot_net.Services
{
    public interface ICarDetailsService
    {
        IEnumerable<CarDetails> GetAllCars();
        CarDetails GetCarById(int id);
        CarDetails AddCar(CarDetails car);
        CarDetails UpdateCar(CarDetails car);
        bool DeleteCar(int id);
        IEnumerable<CarDetails> GetCarsByVehicleId(int vehicleId);
        IEnumerable<CarDetails> GetAvailableCarsByVehicleId(int vehicleId);
        IEnumerable<AvailableCarDTO> GetAvailableCarsWithDetailsByVehicleId(int vehicleId);
    }
}
