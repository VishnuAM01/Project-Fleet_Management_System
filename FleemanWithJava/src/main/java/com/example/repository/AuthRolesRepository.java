package com.example.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Modifying;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

import com.example.models.AuthRoles;

@Repository
public interface AuthRolesRepository extends JpaRepository<AuthRoles,Integer> {

	 @Query(value = "SELECT * FROM auth_role WHERE role_id = ?1", nativeQuery = true)
	    AuthRoles findByRoleId(int roleId);

	    @Query(value = "SELECT * FROM auth_role WHERE role_name = ?1", nativeQuery = true)
	    AuthRoles findByRoleName(String roleName);

	    @Modifying
	    @Transactional
	    @Query(value = "UPDATE auth_role SET role_name = ?1 WHERE role_id = ?2", nativeQuery = true)
	    void updateRoleName(String roleName, int roleId);

	    @Modifying
	    @Transactional
	    @Query(value = "DELETE FROM auth_role WHERE role_id = ?1", nativeQuery = true)
	    void deleteByRoleId(int roleId);
	
}
