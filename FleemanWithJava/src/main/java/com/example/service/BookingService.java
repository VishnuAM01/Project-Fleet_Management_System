package com.example.service;

import com.example.dto.BookingSummaryDTO;
import com.example.dto.UserBookingRequestDTO;
import com.example.models.BookingDetails;

import java.util.List;
import java.util.Optional;

public interface BookingService {

    void createBookingWithMemberId(String memberId, UserBookingRequestDTO requestDTO);

    BookingDetails saveBooking(BookingDetails bookingDetails);

//    Optional<BookingDetails> getBookingById(Integer bookingId);
    BookingDetails getBookById(Integer bookingId);

    List<BookingDetails> getAllBookings();

    BookingDetails updateBooking(Integer bookingId, BookingDetails bookingDetails);

    void deleteBooking(Integer bookingId);
}
