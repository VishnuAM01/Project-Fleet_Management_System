package com.example.service;

import java.util.List;

import com.example.dto.LocationRequestDTO;
import com.example.dto.LocationResponseDTO;
import com.example.models.LocationDetails;

public interface LocationDetailsService {
	
	 LocationDetails getLocationById(int locationId);
        List<LocationDetails> getAllLocations();
	    LocationResponseDTO getLocationsByCity(String city);
	    LocationResponseDTO getLocationsByAirport(String airport);
		LocationDetails addLocation(LocationRequestDTO dto);
	   
}
