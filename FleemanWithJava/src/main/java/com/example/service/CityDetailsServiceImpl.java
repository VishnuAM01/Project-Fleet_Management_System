package com.example.service;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.example.dto.CityDTO;
import com.example.models.CityDetails;
import com.example.repository.CityDetailsRepository;

@Service
public class CityDetailsServiceImpl implements CityDetailsService {
	
	@Autowired
	private CityDetailsRepository repository;

	@Override
	public List<CityDetails> getAllCities() {
		  return repository.findAll();
	}

	@Override
	public CityDetails getById(int id) {
		  return repository.findByCityId(id);
	}

	@Override
	public List<CityDetails> getByStateId(int stateId) {
		return repository.findByStateId(stateId);
	}
	
	public CityDTO getCityDTO(CityDetails city) {
	    return new CityDTO(city.getCity_Name(), city.getState().getState_Name());
	}
	
	@Override
	public List<CityDTO> getAllCitiesDTO() {
	    List<CityDetails> cities = repository.findAll();
	    return cities.stream()
	        .map(city -> new CityDTO(city.getCity_Name(), city.getState().getState_Name()))
	        .toList();
	}


}
