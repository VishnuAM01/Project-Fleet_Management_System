package com.example.service;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.example.models.AuthRoles;
import com.example.repository.AuthRolesRepository;
@Service
public class AuthRolesServiceImpl implements AuthRolesService {
	
	@Autowired
	private AuthRolesRepository repository;

	@Override
	public List<AuthRoles> getAllRoles() {
		return repository.findAll();
	}

	@Override
	public AuthRoles getById(int id) {
		 return repository.findByRoleId(id);
	}

	@Override
	public void updateRole(int id, String roleName) {
		repository.updateRoleName(roleName, id);
		
	}

	@Override
	public void deleteRole(int id) {
		 repository.deleteByRoleId(id);
	}

}
