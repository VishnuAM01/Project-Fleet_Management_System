package com.example.controller;

import com.example.models.StateDetails;
import com.example.service.StateDetailsService;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/states")
public class StateDetailsController {

    @Autowired
    private StateDetailsService stateService;

    @GetMapping
    public List<StateDetails> getAllStates() {
        return stateService.getAllStates();
    }

    @GetMapping("/{id}")
    public ResponseEntity<StateDetails> getStateById(@PathVariable int id) {
        StateDetails state = stateService.getById(id);
        if (state != null) {
            return ResponseEntity.ok(state);
        } else {
            return ResponseEntity.notFound().build();
        }
    }

    
    @GetMapping("/search")
    public ResponseEntity<StateDetails> getStateByName(@RequestParam String name) {
        StateDetails state = stateService.getByName(name);
        if (state != null) {
            return ResponseEntity.ok(state);
        } else {
            return ResponseEntity.notFound().build();
        }
    }
}
