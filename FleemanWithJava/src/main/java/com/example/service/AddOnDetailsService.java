package com.example.service;

import java.util.List;

import com.example.dto.AddOnDetailsDTO;

public interface AddOnDetailsService {
	
	AddOnDetailsDTO createAddOn(AddOnDetailsDTO dto);
	
	List<AddOnDetailsDTO> getAllAddOns();
	
	AddOnDetailsDTO getAddOnById(Short id);
	
	void deleteAddOnById(Short id);

}
