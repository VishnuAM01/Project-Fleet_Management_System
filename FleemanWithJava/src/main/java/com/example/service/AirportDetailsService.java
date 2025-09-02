package com.example.service;

import java.util.List;

import com.example.models.AirportDetails;

public interface AirportDetailsService {
	
	 List<AirportDetails> getAllAirports();
	    AirportDetails getById(int id);
	    List<AirportDetails> getByCityId(int cityId);
	    List<AirportDetails> getByStateId(int stateId);
		AirportDetails getByCode(String code);
		
	   
}
