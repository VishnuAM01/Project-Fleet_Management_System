package com.example.service;

import com.example.dto.BookingConfirmationDTO;
import com.example.models.BookingConfirmationDetails;
import java.util.List;
import java.util.Optional;

public interface BookingConfirmationDetailsService {
   // BookingConfirmationDetails create(BookingConfirmationDetails bookingConfirmation);
    public BookingConfirmationDetails create(BookingConfirmationDTO dto);

    Optional<BookingConfirmationDetails> getById(Long bookingId);
    List<BookingConfirmationDetails> getAll();
    BookingConfirmationDetails update(Long bookingId, BookingConfirmationDetails updated);
    void delete(Long bookingId);
}
