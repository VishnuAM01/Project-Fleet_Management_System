package com.example.service;



import java.util.List;

import com.example.models.VehicleDetails;

public interface VehicleDetailsService {

    VehicleDetails addVehicle(VehicleDetails vehicle);

    VehicleDetails getVehicleById(Long vehicleId);

    List<VehicleDetails> getAllVehicles();

    List<VehicleDetails> getVehiclesByType(String vehicleType);

    VehicleDetails updateVehicle(Long vehicleId, VehicleDetails updatedVehicle);

    void deleteVehicle(Long vehicleId);
}
