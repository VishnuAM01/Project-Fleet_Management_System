package com.example.controller;

import com.example.models.AgentDetails;
import com.example.service.AgentDetailsService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/agents")
public class AgentDetailsController {

    @Autowired
    private AgentDetailsService agentService;

    // Get all agents
    @GetMapping
    public ResponseEntity<List<AgentDetails>> getAllAgents() {
        return ResponseEntity.ok(agentService.getAllAgents());
    }

    // Get agent by ID
    @GetMapping("/{id}")
    public ResponseEntity<AgentDetails> getAgentById(@PathVariable int id) {
        AgentDetails agent = agentService.getById(id);
        if (agent != null) {
            return ResponseEntity.ok(agent);
        } else {
            return ResponseEntity.notFound().build();
        }
    }

    // Delete agent by ID
    @DeleteMapping("/{id}")
    public ResponseEntity<Void> deleteAgent(@PathVariable int id) {
        agentService.deleteAgent(id);
        return ResponseEntity.noContent().build();
    }
}
