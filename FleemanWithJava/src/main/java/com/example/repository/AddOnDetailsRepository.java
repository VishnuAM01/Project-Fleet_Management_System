package com.example.repository;


import java.util.Optional;
import org.springframework.data.jpa.repository.JpaRepository;

import com.example.models.AddonDetails;

public interface AddOnDetailsRepository extends JpaRepository<AddonDetails, Short> {
	
	/*
	 * Optional<T> is:
	 * A container that may or may not contain a non-null value.
	 * A safe and clean way to handle possibly missing data.
	 */
	Optional<AddonDetails> findByAddOnName(String name);
	
	boolean existsByAddOnName(String name);

}
