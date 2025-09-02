package com.example.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import com.example.models.CarDetails;
import java.util.List;

public interface CarDetailsRepository extends JpaRepository<CarDetails, Integer> {
	CarDetails findByCarName(String carName);
	List<CarDetails> findByVehicleDetails_VehicleId(Long vehicleId);

}
