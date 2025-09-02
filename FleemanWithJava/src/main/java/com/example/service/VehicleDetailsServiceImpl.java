package com.example.service;


import com.example.repository.VehicleDetailsRepository;
import com.example.service.VehicleDetailsService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.example.models.VehicleDetails;

import java.util.List;

@Service
public class VehicleDetailsServiceImpl implements VehicleDetailsService {

    @Autowired
    private VehicleDetailsRepository vehicleRepository;

    @Override
    public VehicleDetails addVehicle(VehicleDetails vehicle) {
        return vehicleRepository.save(vehicle);
    }

    @Override
    public VehicleDetails getVehicleById(Long vehicleId) {
        return vehicleRepository.findById(vehicleId).orElse(null);
    }

    @Override
    public List<VehicleDetails> getAllVehicles() {
        return vehicleRepository.findAll();
    }

    @Override
    public List<VehicleDetails> getVehiclesByType(String vehicleType) {
        return vehicleRepository.findByVehicleType(vehicleType);
    }

    @Override
    public VehicleDetails updateVehicle(Long vehicleId, VehicleDetails updatedVehicle) {
        VehicleDetails existing = vehicleRepository.findById(vehicleId).orElse(null);
        if (existing != null) {
            existing.setVehicleType(updatedVehicle.getVehicleType());
            existing.setDailyRate(updatedVehicle.getDailyRate());
            existing.setWeeklyRate(updatedVehicle.getWeeklyRate());
            existing.setMonthlyRate(updatedVehicle.getMonthlyRate());
            existing.setImgPath(updatedVehicle.getImgPath());
            return vehicleRepository.save(existing);
        }
        return null;
    }

    @Override
    public void deleteVehicle(Long vehicleId) {
        vehicleRepository.deleteById(vehicleId);
    }
}
