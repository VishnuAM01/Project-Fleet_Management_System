package com.example.controller;

import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.*;

import com.example.dto.AddOnBookDTO;
import com.example.service.AddOnBookService;


@RestController
@RequestMapping("/api/addonbook")
@CrossOrigin("*")
public class AddOnBookController {
	
	@Autowired
	private AddOnBookService addOnBookService;
	
	@GetMapping
	public List<AddOnBookDTO> getAllAddOnBooks(){
		return addOnBookService.getAddOnBooks();
	}
	
	@GetMapping("/{id}")
	public AddOnBookDTO getAddOnBookById(@PathVariable Integer id) {
		return addOnBookService.getAddOnBookById(id);
	}
	
	@PostMapping
	public AddOnBookDTO addAddOnBooK(@RequestBody AddOnBookDTO dto) {
		return addOnBookService.addAddOnBook(dto);
	}
	
	@PutMapping("/{id}")
	public AddOnBookDTO updateAddOnBook(@PathVariable Integer id, @RequestBody AddOnBookDTO dto) {
		return addOnBookService.updateAddOnBook(id, dto);
	}
	
	@DeleteMapping("/{id}")
	public void deleteAddOnBook(@PathVariable Integer id) {
		addOnBookService.deleteAddOnBook(id);
	}

}
