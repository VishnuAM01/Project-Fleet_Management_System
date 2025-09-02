package com.example.controller;

import com.example.dto.LocationRequestDTO;
import com.example.dto.LocationResponseDTO;
import com.example.models.LocationDetails;
import com.example.service.LocationDetailsService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.stream.Collectors;

@CrossOrigin(origins ="*")
@RestController
@RequestMapping("/api/locations")
public class LocationDetailsController {

	 @Autowired
	    private LocationDetailsService locationService;

	    @GetMapping("/{id}")
	    public ResponseEntity<LocationDetails> getLocationById(@PathVariable int id) {
	        LocationDetails location = locationService.getLocationById(id);
	        return ResponseEntity.ok(location);
	    }

	    @GetMapping
	    public ResponseEntity<List<LocationDetails>> getAllLocations() {
	        List<LocationDetails> locations = locationService.getAllLocations();
	        return ResponseEntity.ok(locations);
	    }
   
	    @GetMapping("/city/{cityName}")
	    public ResponseEntity<LocationResponseDTO> getByCity(@PathVariable("cityName") String cityName) {
	        return ResponseEntity.ok(locationService.getLocationsByCity(cityName));
	    }

	    @GetMapping("/airport/{airportCode}")
	    public ResponseEntity<LocationResponseDTO> getByAirport(@PathVariable("airportCode") String airportCode) {
	        return ResponseEntity.ok(locationService.getLocationsByAirport(airportCode));
	    }
	    
	    @PostMapping
	    public ResponseEntity<LocationDetails> addLocation(@RequestBody LocationRequestDTO dto) {
	        LocationDetails saved = locationService.addLocation(dto);
	        return ResponseEntity.ok(saved);
	    }
}