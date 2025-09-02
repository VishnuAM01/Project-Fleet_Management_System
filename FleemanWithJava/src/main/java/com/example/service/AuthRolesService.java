package com.example.service;

import java.util.List;

import com.example.models.AuthRoles;

public interface AuthRolesService {
	List<AuthRoles> getAllRoles();
    AuthRoles getById(int id);
    void updateRole(int id, String roleName);
    void deleteRole(int id);
}
