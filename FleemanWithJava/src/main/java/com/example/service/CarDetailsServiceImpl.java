package com.example.service;

import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.example.dto.CarDetailsDTO;
import com.example.models.CarDetails;
import com.example.models.VehicleDetails;
import com.example.repository.CarDetailsRepository;
import com.example.repository.VehicleDetailsRepository;

@Service
public class CarDetailsServiceImpl implements CarDetailsService {

	@Autowired
	private CarDetailsRepository carRepo;

	@Autowired
	private VehicleDetailsRepository vehicleRepo;

	private CarDetailsDTO toDTO(CarDetails car) {
		
		  if (!car.getIsAvailable()) {
		        return null;  // or you can choose to throw an exception if you prefer
		    }
		  
		CarDetailsDTO dto = new CarDetailsDTO();
		dto.setCarId(car.getCarId());
		dto.setCarName(car.getCarName());

		// Note: using method with typo
		dto.setRegistrationNumber(car.getRegistrationNUmber());

		dto.setFuelStatus(car.getFuelStatus());
		dto.setImgPath(car.getImgPath());
		dto.setIsAvailable(car.getIsAvailable());
		dto.setLocationId(car.getLocationId());
		dto.setMaintenanceDate(car.getMaintenanceDate());
		dto.setVehicle(car.getVehicleDetails());

		if (car.getVehicleDetails() != null) {
			dto.setVehicleId(car.getVehicleDetails().getVehicleId());
		}
		return dto;
	}

	private CarDetails toEntity(CarDetailsDTO dto) {
		CarDetails car = new CarDetails();
		car.setCarId(dto.getCarId());
		car.setCarName(dto.getCarName());

		// Note: using method with typo
		car.setRegistrationNUmber(dto.getRegistrationNumber());

		car.setFuelStatus(dto.getFuelStatus());
		car.setImgPath(dto.getImgPath());
		car.setIsAvailable(dto.getIsAvailable());
		car.setLocationId(dto.getLocationId());
		car.setMaintenanceDate(dto.getMaintenanceDate());

		if (dto.getVehicleId() != null) {
			Optional<VehicleDetails> vehicle = vehicleRepo.findById(dto.getVehicleId());
			vehicle.ifPresent(car::setVehicleDetails);
		}

		return car;
	}

	@Override
	public List<CarDetailsDTO> getAllCars() {
		return carRepo.findAll().stream()
				.map(this::toDTO)
				.collect(Collectors.toList());
	}

	@Override
	public CarDetailsDTO getCarById(Integer carId) {
		Optional<CarDetails> car = carRepo.findById(carId);
		return car.map(this::toDTO).orElse(null);
	}

	@Override
	public List<CarDetailsDTO> getCarsByVehicleId(Long vehicleId) {
		List<CarDetails> cars = carRepo.findByVehicleDetails_VehicleId(vehicleId);
		return cars.stream().map(this::toDTO).collect(Collectors.toList());
	}

	@Override
	public CarDetailsDTO addCar(CarDetailsDTO carDto) {
		CarDetails car = toEntity(carDto);
		car = carRepo.save(car);
		return toDTO(car);
	}
	
	@Override
	public void updateFuelStatusByCarId(Integer carId, String fuelStatus) {
	    CarDetails car = carRepo.findById(carId)
	        .orElseThrow(() -> new RuntimeException("Car not found with ID: " + carId));
	    
	    car.setFuelStatus(fuelStatus);
	    carRepo.save(car);
	}


	@Override
	public CarDetailsDTO updateCar(Integer carId, CarDetailsDTO carDto) {
		if (carRepo.existsById(carId)) {
			CarDetails car = toEntity(carDto);
			car.setCarId(carId);
			return toDTO(carRepo.save(car));
		}
		return null;
	}

	@Override
	public void deleteCar(Integer carId) {
		carRepo.deleteById(carId);
	}
}
