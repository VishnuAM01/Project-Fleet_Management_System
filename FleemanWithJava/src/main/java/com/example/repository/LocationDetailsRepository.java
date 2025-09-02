package com.example.repository;

import java.util.List;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Modifying;
import org.springframework.data.jpa.repository.Query;
import org.springframework.data.repository.query.Param;
import org.springframework.stereotype.Repository;
//import org.springframework.transaction.annotation.Transactional;

//import com.example.models.AgentDetails;
import com.example.models.LocationDetails;

@Repository
public interface LocationDetailsRepository extends JpaRepository<LocationDetails, Integer> {

//	 @Query(value = "SELECT * FROM location_details l left join city_details c on l.city_id=c.city_id  WHERE c.city_name = :city", nativeQuery = true)
//	    List<LocationDetails> findByCity(String city);

	@Query(value = "SELECT l.location_id,l.address,l.location_name,l.mobile_number,c.city_id,l.state_id,c.city_name FROM location_details l JOIN city_details c ON l.city_id = c.city_id WHERE c.city_name = :city", nativeQuery = true)
	List<LocationDetails> findByCity(@Param("city") String city);

	@Query(value = "SELECT l.* FROM location_details l JOIN airport_details a ON l.airport_id = a.airport_id WHERE a.airport_code = :airportCode", nativeQuery = true)
	List<LocationDetails> findByAirport(@Param("airportCode") String airportCode);
	
	 @Query(value = "SELECT city_id FROM city_details WHERE city_name = :cityName", nativeQuery = true)
	    Integer findCityIdByName(@Param("cityName") String cityName);

	    @Query(value = "SELECT state_id FROM state_details WHERE state_name = :stateName", nativeQuery = true)
	    Integer findStateIdByName(@Param("stateName") String stateName);

	    @Query(value = "SELECT airport_id FROM airport_details WHERE airport_code = :airportCode", nativeQuery = true)
	    Integer findAirportIdByCode(@Param("airportCode") String airportCode);

}
