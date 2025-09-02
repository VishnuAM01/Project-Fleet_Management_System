package com.example.service;

import java.util.List;

import com.example.models.StateDetails;

public interface StateDetailsService {
	
	List<StateDetails> getAllStates();
    StateDetails getById(int id);
    StateDetails getByName(String name);
}
