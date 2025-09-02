package com.example.service;

import com.example.dto.BookingSummaryDTO;

import com.example.dto.UserBookingRequestDTO;
import com.example.models.BookingDetails;

import java.util.List;
import java.util.Optional;

import org.springframework.data.jpa.repository.Query;

public interface BookingDetailsService {
    BookingDetails saveBooking(BookingDetails bookingDetails);
//    Optional<BookingDetails> getBookingById(Integer bookingId);
    BookingSummaryDTO getBookingById(Integer bookingId);
    List<BookingDetails> getAllBookings();
    BookingDetails updateBooking(Integer bookingId, BookingDetails bookingDetails);
    void deleteBooking(Integer bookingId);
    void createBooking(UserBookingRequestDTO request);
   
   List<BookingDetails> getUnconfirmedBookings();
    List<BookingDetails> getConfirmedBookings();
}
