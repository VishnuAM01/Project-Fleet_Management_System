package com.example.controller;

import com.example.models.AuthRoles;
import com.example.service.AuthRolesService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/roles")
public class AuthRolesController {

    @Autowired
    private AuthRolesService authRolesService;

    // Get all roles
    @GetMapping
    public ResponseEntity<List<AuthRoles>> getAllRoles() {
        List<AuthRoles> roles = authRolesService.getAllRoles();
        return ResponseEntity.ok(roles);
    }

    // Get role by ID
    @GetMapping("/{id}")
    public ResponseEntity<AuthRoles> getRoleById(@PathVariable int id) {
        AuthRoles role = authRolesService.getById(id);
        if (role != null) {
            return ResponseEntity.ok(role);
        } else {
            return ResponseEntity.notFound().build();
        }
    }
}