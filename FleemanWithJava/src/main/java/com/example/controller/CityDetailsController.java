package com.example.controller;

import com.example.dto.CityDTO;
import com.example.models.CityDetails;
import com.example.service.CityDetailsService;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@CrossOrigin(origins ="*")
@RestController
@RequestMapping("/api/cities")
public class CityDetailsController {

    @Autowired
    private CityDetailsService cityService;

    // GET all cities
   
   

    // GET city by ID
    @GetMapping("/{id}")
    public ResponseEntity<CityDetails> getCityById(@PathVariable int id) {
        CityDetails city = cityService.getById(id);
        if (city != null) {
            return ResponseEntity.ok(city);
        } else {
            return ResponseEntity.notFound().build();
        }
    }

    // GET cities by State ID
    @GetMapping("/bystate")
    public ResponseEntity<List<CityDetails>> getCitiesByStateId(@RequestParam int stateId) {
        List<CityDetails> cities = cityService.getByStateId(stateId);
        if (cities.isEmpty()) {
            return ResponseEntity.noContent().build();
        } else {
            return ResponseEntity.ok(cities);
        }
    }
    
    @GetMapping("/city/{id}")
    public ResponseEntity<CityDTO> getCity(@PathVariable int id) {
        CityDetails city = (CityDetails) cityService.getByStateId(id);
        CityDTO dto = new CityDTO(city.getCity_Name(), city.getState().getState_Name());
        return ResponseEntity.ok(dto);
    }
    
    @GetMapping("/all")
    public List<CityDTO> getAllCities() {
        return cityService.getAllCitiesDTO();
    }

}
