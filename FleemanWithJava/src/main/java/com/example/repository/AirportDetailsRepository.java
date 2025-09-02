package com.example.repository;

import java.util.List;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Modifying;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

import com.example.models.AirportDetails;

@Repository
public interface AirportDetailsRepository extends JpaRepository<AirportDetails,Integer> {

	@Query(value = "SELECT * FROM airport_details WHERE airport_id = ?1", nativeQuery = true)
    AirportDetails findByAirportId(int airportId);

    @Query(value = "SELECT * FROM airport_details WHERE city_id = ?1", nativeQuery = true)
    List<AirportDetails> findByCityId(int cityId);

    @Query(value = "SELECT * FROM airport_details WHERE state_id = ?1", nativeQuery = true)
    List<AirportDetails> findByStateId(int stateId);

    @Query(value = "SELECT * FROM airport_details WHERE airport_code = ?1", nativeQuery = true)
    AirportDetails findByAirportCode(String code);

    @Modifying
    @Transactional
    @Query(value = "DELETE FROM airport_details WHERE airport_id = ?1", nativeQuery = true)
    void deleteByAirportId(int airportId);
}
