package com.example.service;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.example.models.AgentDetails;
import com.example.repository.AgentDetailsRepository;

@Service
public class AgentDetailsServiceImpl implements AgentDetailsService {

	@Autowired
    private AgentDetailsRepository repository;
	@Override
	public List<AgentDetails> getAllAgents() {
		return repository.findAll();
	}

	@Override
	public AgentDetails getById(int id) {
		return repository.findByUserId(id);
	}

	@Override
	public void deleteAgent(int id) {
		 repository.deleteByUserId(id);
	}

}
