package com.example.service;

import java.util.List;
import com.example.dto.CarDetailsDTO;

public interface CarDetailsService {
	//List all the cars in the system
	List<CarDetailsDTO> getAllCars();
	//Get a particular car's details by it's ID
	CarDetailsDTO getCarById(Integer carId);
	//Add a new car into system
	CarDetailsDTO addCar(CarDetailsDTO carDto);
	//update an existing car using existing ID and new details
	CarDetailsDTO updateCar(Integer carId, CarDetailsDTO carDto);
	//Delete a car details from system
	void deleteCar(Integer carId);
	//Get all cars by vehicle ID
	List<CarDetailsDTO> getCarsByVehicleId(Long vehicleId);
	
	void updateFuelStatusByCarId(Integer carId, String fuelStatus);


	

}
