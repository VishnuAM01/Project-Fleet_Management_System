package com.example.service;

import java.util.List;
import java.util.stream.Collectors;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.example.dto.LocationNameAddressDTO;
import com.example.dto.LocationRequestDTO;
import com.example.dto.LocationResponseDTO;
import com.example.models.AirportDetails;
import com.example.models.CityDetails;
import com.example.models.LocationDetails;
import com.example.models.StateDetails;
import com.example.repository.LocationDetailsRepository;

@Service
public class LocationDetailsServiceImpl implements LocationDetailsService {

	@Autowired
	private LocationDetailsRepository repository;

	@Override
	public LocationDetails getLocationById(int locationId) {
		  return repository.findById(locationId)
	                .orElseThrow(() -> new RuntimeException("Location with ID " + locationId + " not found."));
	}

	@Override
	public List<LocationDetails> getAllLocations() {
        return repository.findAll();

	}
	
	@Override
    public LocationResponseDTO getLocationsByCity(String city) {
        List<LocationDetails> locations = repository.findByCity(city);
        List<LocationNameAddressDTO> locationNames = locations.stream()
        	    .map(loc -> new LocationNameAddressDTO(
        	        loc.getLocation_Name(),
        	        loc.getAddress(),
        	        loc.getMobileNumber() // Assuming LocationDetails has getMobileNumber()
        	    ))
        	    .collect(Collectors.toList());

        return new LocationResponseDTO(city, null, locationNames);
    }

    @Override
    public LocationResponseDTO getLocationsByAirport(String airport) {
        List<LocationDetails> locations = repository.findByAirport(airport);
        List<LocationNameAddressDTO> locationNames = locations.stream()
        	    .map(loc -> new LocationNameAddressDTO(
        	        loc.getLocation_Name(),
        	        loc.getAddress(),
        	        loc.getMobileNumber() // Assuming LocationDetails has getMobileNumber()
        	    ))
        	    .collect(Collectors.toList());

        return new LocationResponseDTO(null, airport, locationNames);
    }
    
    
    @Override
    public LocationDetails addLocation(LocationRequestDTO dto) {
        LocationDetails loc = new LocationDetails();
        loc.setLocation_Name(dto.getLocationName());
        loc.setAddress(dto.getAddress());

        if (dto.getCityName() != null && dto.getStateName() != null) {
            Integer cityId = repository.findCityIdByName(dto.getCityName());
            Integer stateId = repository.findStateIdByName(dto.getStateName());

            if (cityId == null || stateId == null) {
                throw new RuntimeException("Invalid city or state name.");
            }

            CityDetails city = new CityDetails();
            city.setCity_Id(cityId);

            StateDetails state = new StateDetails();
            state.setState_Id(stateId);

            loc.setCity(city);
            loc.setState(state);

        } 
        else if (dto.getAirportCode() != null) {
            Integer airportId = repository.findAirportIdByCode(dto.getAirportCode());
            if (airportId == null) {
                throw new RuntimeException("Invalid airport code.");
            }

            AirportDetails airport = new AirportDetails();
            airport.setAirport_Id(airportId);
//            loc.setAirport(airport);

        } else {
            throw new RuntimeException("Provide either (city & state) or airport code.");
        }

        return repository.save(loc);
    }

}
