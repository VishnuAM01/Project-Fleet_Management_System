package com.example.repository;

import java.util.List;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Modifying;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

import com.example.models.AgentDetails;

@Repository
public interface AgentDetailsRepository extends JpaRepository<AgentDetails,Integer> {

	 @Query(value = "SELECT * FROM agent_details WHERE agent_id = ?1", nativeQuery = true)
	    AgentDetails findByUserId(int userId);

	    @Query(value = "SELECT * FROM agent_details WHERE email = ?1", nativeQuery = true)
	    AgentDetails findByEmail(String email);

	    @Query(value = "SELECT * FROM agent_details WHERE role_id = ?1", nativeQuery = true)
	    List<AgentDetails> findByRoleId(int roleId);

	    @Query(value = "SELECT * FROM agent_details WHERE agent_user_name = ?1", nativeQuery = true)
	    AgentDetails findByUserName(String username);

	    @Modifying
	    @Transactional
	    @Query(value = "DELETE FROM agent_details WHERE role_id = ?1", nativeQuery = true)
	    void deleteByUserId(int userId);	
}
