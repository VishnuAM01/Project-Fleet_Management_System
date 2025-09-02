package com.example.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Modifying;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

import com.example.models.StateDetails;

@Repository
public interface StateDetailsRepository extends JpaRepository<StateDetails,Integer> 
{
	@Query(value = "SELECT * FROM state_details WHERE state_id = :id", nativeQuery = true)
    StateDetails findByStateId(@Param("id") int id);

	@Query(value = "SELECT * FROM state_details WHERE state_name = :name", nativeQuery = true)
	StateDetails findByStateName(@Param("name") String name);
	   
}
