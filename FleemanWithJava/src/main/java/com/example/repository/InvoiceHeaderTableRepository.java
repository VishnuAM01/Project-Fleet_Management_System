package com.example.repository;

import com.example.models.InvoiceHeaderTable;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.List;
import java.util.Optional;

public interface InvoiceHeaderTableRepository extends JpaRepository<InvoiceHeaderTable, Integer> {

    // Find invoices by bookingId
   // List<InvoiceHeaderTable> findByBookingId(Integer bookingId);
    
    List<InvoiceHeaderTable> findByBookingIdIn(List<Integer> bookingIds);
    Optional<InvoiceHeaderTable> findByBookingId(Integer bookingId);

}
