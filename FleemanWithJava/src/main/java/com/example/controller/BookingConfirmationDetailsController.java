package com.example.controller;

import com.example.dto.BookingConfirmationDTO;
import com.example.models.BookingConfirmationDetails;
import com.example.service.BookingConfirmationDetailsService;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@CrossOrigin(origins ="*")
@RestController
@RequestMapping("/api/booking-confirmations")
public class BookingConfirmationDetailsController {

    private final BookingConfirmationDetailsService service;

    public BookingConfirmationDetailsController(BookingConfirmationDetailsService service) {
        this.service = service;
    }

    @PostMapping
    public ResponseEntity<BookingConfirmationDetails> create(@RequestBody BookingConfirmationDTO dto) {
        try {
            BookingConfirmationDetails saved = service.create(dto);
            return ResponseEntity.status(201).body(saved);
        } catch (RuntimeException ex) {
            // Optional: return proper error code & message
            return ResponseEntity.badRequest().body(null); // or create a custom error response
        }
    }


    @GetMapping("/{id}")
    public ResponseEntity<BookingConfirmationDetails> getById(@PathVariable("id") Long id) {
        return service.getById(id)
                .map(ResponseEntity::ok)
                .orElse(ResponseEntity.notFound().build());
    }


    @GetMapping
    public ResponseEntity<List<BookingConfirmationDetails>> getAll() {
        return ResponseEntity.ok(service.getAll());
    }

    @PutMapping("/{id}")
    public ResponseEntity<BookingConfirmationDetails> update(@PathVariable Long id, @RequestBody BookingConfirmationDetails updated) {
        BookingConfirmationDetails saved = service.update(id, updated);
        return ResponseEntity.ok(saved);
    }

    @DeleteMapping("/{id}")
    public ResponseEntity<Void> delete(@PathVariable Long id) {
        service.delete(id);
        return ResponseEntity.noContent().build();
    }
}
