package com.example.controller;

import com.example.dto.BookingSummaryDTO;
import com.example.dto.UserBookingRequestDTO;
import com.example.models.BookingDetails;
import com.example.service.BookingDetailsService;
import com.example.service.BookingService;


import org.springframework.beans.factory.annotation.Autowired;

import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@CrossOrigin(origins ="*")
@RestController
@RequestMapping("/api/bookings")
public class BookingDetailsController {

    
    @Autowired
    private BookingDetailsService bookingService;
    
    @Autowired
    private BookingService booking;

    @PostMapping
    public ResponseEntity<String> createBooking(@RequestBody UserBookingRequestDTO request) {
        bookingService.createBooking(request);
        return ResponseEntity.ok("Booking created successfully!");
    }
    
    @PostMapping("/{memberId}")
    public String createBooking(@PathVariable("memberId") String memberId,
            @RequestBody UserBookingRequestDTO requestDTO)
 {
    	booking.createBookingWithMemberId(memberId, requestDTO);
        return "Booking successfully created for member ID: " + memberId;
    }
    @GetMapping("/new/{id}")
    public BookingSummaryDTO getBookingById(@PathVariable("id") Integer bookingId) {
        return bookingService.getBookingById(bookingId);
    }
    
    @GetMapping("/summary/{id}")
    public ResponseEntity<BookingDetails> getBookById(@PathVariable("id") Integer bookingId) {
        BookingDetails book = booking.getBookById(bookingId);
        return ResponseEntity.ok(book);
    }
    
    @GetMapping("/unconfirmed")
    public ResponseEntity<List<BookingDetails>> getUnconfirmedBookings() {
        return ResponseEntity.ok(bookingService.getUnconfirmedBookings());
    }
    
    @GetMapping("/confirmed")
    public ResponseEntity<List<BookingDetails>> getConfirmedBookings() {
        return ResponseEntity.ok(bookingService.getConfirmedBookings());
    }

    
    @GetMapping
    public List<BookingDetails> getAllBookings() {
        return bookingService.getAllBookings();
    }
}
