package com.example.repository;

import java.util.List;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Modifying;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

import com.example.dto.CityDTO;
import com.example.models.CityDetails;

@Repository
public interface CityDetailsRepository extends JpaRepository<CityDetails,Integer> {
	
	
	 @Query(value = "SELECT * FROM city_details WHERE city_id = :id", nativeQuery = true)
	    CityDetails findByCityId(@Param("id") int id);

	    @Query(value = "SELECT * FROM city_details WHERE state_id = ?1", nativeQuery = true)
	    List<CityDetails> findByStateId(int stateId);
	    
	    @Query("SELECT new com.example.dto.CityDTO(c.city_Name, s.state_Name) FROM CityDetails c JOIN c.state s")
	    List<CityDTO> getAllCityDTOs();
	    

	   
}
