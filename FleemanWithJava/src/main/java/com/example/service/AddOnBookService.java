package com.example.service;

import java.util.List;

import org.springframework.stereotype.Service;

import com.example.dto.AddOnBookDTO;

@Service
public interface AddOnBookService {
	
	List<AddOnBookDTO> getAddOnBooks();
	
	AddOnBookDTO getAddOnBookById(Integer bookingId);
	
	AddOnBookDTO addAddOnBook(AddOnBookDTO dto);
	
	AddOnBookDTO updateAddOnBook(Integer bookingId, AddOnBookDTO dto);
	
	void deleteAddOnBook(Integer bookingId);

}
