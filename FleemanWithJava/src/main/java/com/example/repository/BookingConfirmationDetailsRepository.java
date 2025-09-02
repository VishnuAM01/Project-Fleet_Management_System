package com.example.repository;

import com.example.models.BookingConfirmationDetails;
import org.springframework.data.jpa.repository.JpaRepository;

public interface BookingConfirmationDetailsRepository extends JpaRepository<BookingConfirmationDetails, Long> {
}
