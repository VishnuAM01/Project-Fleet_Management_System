package com.example.controller;

import com.example.models.AirportDetails;
import com.example.service.AirportDetailsService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@CrossOrigin(origins ="*")
@RestController
@RequestMapping("/api/airports")
public class AirportDetailsController {

    @Autowired
    private AirportDetailsService airportService;

    // Get all airports
    @GetMapping
    public ResponseEntity<List<AirportDetails>> getAllAirports() {
        List<AirportDetails> airports = airportService.getAllAirports();
        return ResponseEntity.ok(airports);
    }

    // Get airport by ID
    @GetMapping("/{id}")
    public ResponseEntity<AirportDetails> getAirportById(@PathVariable int id) {
        AirportDetails airport = airportService.getById(id);
        if (airport != null) {
            return ResponseEntity.ok(airport);
        } else {
            return ResponseEntity.notFound().build();
        }
    }

    // Get airports by city ID
    @GetMapping("/city/{cityId}")
    public ResponseEntity<Object> getAirportsByCityId(@PathVariable int cityId) {
        return ResponseEntity.ok(airportService.getByCityId(cityId));
    }

    // Get airports by state ID
    @GetMapping("/state/{stateId}")
    public ResponseEntity<Object> getAirportsByStateId(@PathVariable int stateId) {
        return ResponseEntity.ok(airportService.getByStateId(stateId));
    }

    // Get airport by code
    @GetMapping("/code/{code}")
    public ResponseEntity<AirportDetails> getAirportByCode(@PathVariable String code) {
        AirportDetails airport = airportService.getByCode(code);
        if (airport != null) {
            return ResponseEntity.ok(airport);
        } else {
            return ResponseEntity.notFound().build();
        }
    }
}