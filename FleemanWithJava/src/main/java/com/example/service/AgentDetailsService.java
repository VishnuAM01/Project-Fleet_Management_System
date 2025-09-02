package com.example.service;

import java.util.List;

import com.example.models.AgentDetails;

public interface AgentDetailsService {
	
	List<AgentDetails> getAllAgents();
    AgentDetails getById(int id);
    void deleteAgent(int id);
}
