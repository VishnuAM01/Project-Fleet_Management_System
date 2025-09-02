package com.example.service;

import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import com.example.dto.AddOnBookDTO;
import com.example.models.AddonBook;
import com.example.repository.AddOnBookRepository;

@Service
public class AddOnBookServiceImpl implements AddOnBookService {
	
	@Autowired
	public AddOnBookRepository addOnBookRepo;
	
	private AddOnBookDTO toDTO(AddonBook addOnBook) {
		AddOnBookDTO dto = new AddOnBookDTO();
		dto.setBookingId(addOnBook.getBookingId());
		dto.setAddonId(addOnBook.getAddonId());
		return dto;
	}
	
	private AddonBook toEntity(AddOnBookDTO dto) {
		AddonBook addOnBook = new AddonBook();
		addOnBook.setBookingId(dto.getBookingId());
		addOnBook.setAddonId(dto.getAddonId());
		return addOnBook;
	}

	@Override
	public List<AddOnBookDTO> getAddOnBooks() {
		return addOnBookRepo.findAll().stream().map(this::toDTO).collect(Collectors.toList());
	}

	@Override
	public AddOnBookDTO getAddOnBookById(Integer bookingId) {
		Optional<AddonBook> addonBook = addOnBookRepo.findById(bookingId);
		return addonBook.map(this::toDTO).orElse(null);
	}

	@Override
	public AddOnBookDTO addAddOnBook(AddOnBookDTO dto) {
		AddonBook addOnBook = toEntity(dto);
		return toDTO(addOnBookRepo.save(addOnBook));
	}

	@Override
	public AddOnBookDTO updateAddOnBook(Integer bookingId, AddOnBookDTO dto) {
		if(addOnBookRepo.existsById(bookingId)) {
			AddonBook addOnBook =toEntity(dto);
			addOnBook.setBookingId(bookingId);
			return toDTO(addOnBookRepo.save(addOnBook));
		}
		return null;
	}

	@Override
	public void deleteAddOnBook(Integer bookingId) {
		addOnBookRepo.deleteById(bookingId);
	}
}
