package com.example.service;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.example.models.AirportDetails;
import com.example.repository.AirportDetailsRepository;
@Service
public class AirportDetailsServiceImpl implements AirportDetailsService {

	@Autowired
	private AirportDetailsRepository repository;
	@Override
	public List<AirportDetails> getAllAirports() {
		 return repository.findAll();
	}

	@Override
	public AirportDetails getById(int id) {
		return repository.findByAirportId(id);
	}

	

	@Override
	public List<AirportDetails> getByStateId(int stateId) {
	    return repository.findByStateId(stateId);
	}

	@Override
	public AirportDetails getByCode(String code) {
	    return repository.findByAirportCode(code);
	}

	@Override
	public List<AirportDetails> getByCityId(int cityId) {
		return repository.findByCityId(cityId);
	}
	

}
