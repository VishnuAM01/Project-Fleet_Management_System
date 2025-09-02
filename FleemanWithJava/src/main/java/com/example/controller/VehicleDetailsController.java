package com.example.controller;

import com.example.models.VehicleDetails;
import com.example.service.VehicleDetailsService;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@CrossOrigin(origins ="*")
@RestController
@RequestMapping("/api/vehicles")
public class VehicleDetailsController {

    @Autowired
    private VehicleDetailsService vehicleService;

    @PostMapping
    public VehicleDetails addVehicle(@RequestBody VehicleDetails vehicle) {
        return vehicleService.addVehicle(vehicle);
    }

    @GetMapping
    public List<VehicleDetails> getAllVehicles() {
        return vehicleService.getAllVehicles();
    }

    @GetMapping("/{id}")
    public VehicleDetails getVehicleById(@PathVariable Long id) {
        return vehicleService.getVehicleById(id);
    }

    @GetMapping("/type/{type}")
    public List<VehicleDetails> getVehiclesByType(@PathVariable String type) {
        return vehicleService.getVehiclesByType(type);
    }

    @PutMapping("/{id}")
    public VehicleDetails updateVehicle(@PathVariable Long id, @RequestBody VehicleDetails vehicle) {
        return vehicleService.updateVehicle(id, vehicle);
    }

    @DeleteMapping("/{id}")
    public String deleteVehicle(@PathVariable Long id) {
        vehicleService.deleteVehicle(id);
        return "Vehicle with ID " + id + " deleted successfully.";
    }
}
