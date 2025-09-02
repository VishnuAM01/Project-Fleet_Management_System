package com.example.repository;



import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import com.example.models.*;

import java.util.List;
import java.util.Optional;

@Repository
public interface VehicleDetailsRepository extends JpaRepository<VehicleDetails, Long> {
    
    List<VehicleDetails> findByVehicleType(String vehicleType);

}

