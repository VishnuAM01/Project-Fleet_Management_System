package com.example.service;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.example.models.StateDetails;
import com.example.repository.StateDetailsRepository;
@Service
public class StateDetailsServiceImpl implements StateDetailsService {

	@Autowired
	private StateDetailsRepository repository;
	@Override
	public List<StateDetails> getAllStates() {
	
		  return repository.findAll();
	}
 
	@Override
	public StateDetails getById(int id) {
		 return repository.findByStateId(id);
	}
    // hello
	@Override
	public StateDetails getByName(String name) {
		 return repository.findByStateName(name);
	}

}
