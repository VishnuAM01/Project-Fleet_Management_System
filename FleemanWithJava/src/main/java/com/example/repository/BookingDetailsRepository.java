package com.example.repository;

import com.example.models.BookingDetails;

import java.util.List;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;

@Repository
public interface BookingDetailsRepository extends JpaRepository<BookingDetails, Integer> {
    // You can define custom query methods here if needed

    // Example: List<BookingDetails> findByPickupLocation(String pickupLocation);
	// Using JPQL
	@Query("SELECT b FROM BookingDetails b WHERE b.bookingId NOT IN (SELECT c.bookingId FROM BookingConfirmationDetails c)")
	List<BookingDetails> findUnconfirmedBookings();
	
	@Query("SELECT b FROM BookingDetails b WHERE b.bookingId  IN (SELECT c.bookingId FROM BookingConfirmationDetails c)")
	List<BookingDetails> findConfirmedBookings();
	
	List<BookingDetails> findByMember_MemberId(String memberId);

}
