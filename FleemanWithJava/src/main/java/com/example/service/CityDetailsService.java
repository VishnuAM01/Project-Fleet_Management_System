package com.example.service;

import java.util.List;

import com.example.dto.CityDTO;
import com.example.models.CityDetails;

public interface CityDetailsService {
	
	List<CityDetails> getAllCities();
    CityDetails getById(int id);
	List<CityDetails> getByStateId(int stateId);
	 CityDTO getCityDTO(CityDetails city);
	 List<CityDTO> getAllCitiesDTO();
}
