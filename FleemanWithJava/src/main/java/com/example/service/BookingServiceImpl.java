package com.example.service;

import com.example.dto.BookingSummaryDTO;
import com.example.dto.UserBookingRequestDTO;
import com.example.dto.UserDetailsDTO;
import com.example.models.*;
import com.example.repository.*;
import com.example.utils.RateCalculator;
import com.example.utils.RateType;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.sql.Date;
import java.sql.Timestamp;
import java.time.LocalDate;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.List;
import java.util.Optional;

@Service
public class BookingServiceImpl implements BookingService {

    @Autowired
    private MemberDetailsRepository memberRepo;

    @Autowired
    private BookingDetailsRepository bookingRepo;

    @Autowired
    private VehicleDetailsRepository vehicleRepo;

    @Autowired
    private AddOnDetailsRepository addonRepo;

    private final DateTimeFormatter dateFormatter = DateTimeFormatter.ofPattern("yyyy-MM-dd");
    private final DateTimeFormatter dateTimeFormatter = DateTimeFormatter.ofPattern("yyyy-MM-dd'T'HH:mm");

    @Override
    public void createBookingWithMemberId(String memberId, UserBookingRequestDTO requestDTO) {
        // Step 1: Validate member
        MemberDetails member = memberRepo.findById(memberId)
                .orElseThrow(() -> new RuntimeException("Member not found with ID: " + memberId));

        // Step 2: Validate and extract user info
        UserDetailsDTO user = requestDTO.getUserDetails();
        if (user == null) {
            throw new RuntimeException("User details are required to create booking");
        }

        // Step 3: Parse pickup and return dates
        LocalDate pickupDate = convertToLocalDate(requestDTO.getPickupDate());
        LocalDate returnDate = convertToLocalDate(requestDTO.getReturnDate());

        // Step 4: Get Vehicle and calculate base rate
        Long vehicleId = requestDTO.getVehicleId();
        if (vehicleId == null) {
            throw new RuntimeException("Vehicle ID is required");
        }

        VehicleDetails vehicle = vehicleRepo.findById(vehicleId)
                .orElseThrow(() -> new RuntimeException("Vehicle not found with ID: " + vehicleId));

        float baseRate = RateCalculator.calculateRate(
                pickupDate,
                returnDate,
                vehicle.getDailyRate(),
                vehicle.getWeeklyRate(),
                vehicle.getMonthlyRate()
        );

        // Step 5: Add-on charges
        float addonTotal = 0f;
        if (requestDTO.getSelectedAddons() != null) {
            for (String addonId : requestDTO.getSelectedAddons()) {
                AddonDetails addon = addonRepo.findById(Short.parseShort(addonId))
                        .orElseThrow(() -> new RuntimeException("Addon not found: " + addonId));
                addonTotal += addon.getAddOnPrice();
            }
        }

        float totalRate = baseRate + addonTotal;

        // Step 6: Create and save booking
        BookingDetails booking = new BookingDetails();
        booking.setBookingDate(Timestamp.valueOf(LocalDateTime.now()));
        booking.setPickupLocation(user.getAddress1());
        booking.setDropLocation(user.getAddress2());
        booking.setPickupDate(Date.valueOf(pickupDate));
        booking.setReturnDate(Date.valueOf(returnDate));
        booking.setVehicleId(vehicleId);
        booking.setRate(totalRate);
        booking.setRateType(RateType.valueOf(requestDTO.getRateType().toUpperCase()));
        booking.setMember(member);

        bookingRepo.save(booking);
    }

    private LocalDate convertToLocalDate(String input) {
        if (input == null || input.isBlank()) return null;

        try {
            if (input.contains("T")) {
                return LocalDateTime.parse(input, dateTimeFormatter).toLocalDate();
            } else {
                return LocalDate.parse(input, dateFormatter);
            }
        } catch (Exception e) {
            throw new RuntimeException("Invalid date format: " + input, e);
        }
    }

    @Override
    public BookingDetails saveBooking(BookingDetails bookingDetails) {
        return bookingRepo.save(bookingDetails);
    }

    public BookingDetails getBookById(Integer bookingId) {
        return bookingRepo.findById(bookingId)
                .orElseThrow(() -> new RuntimeException("Booking not found with ID: " + bookingId));
    }

    @Override
    public List<BookingDetails> getAllBookings() {
        return bookingRepo.findAll();
    }

    @Override
    public BookingDetails updateBooking(Integer bookingId, BookingDetails bookingDetails) {
        return bookingRepo.findById(bookingId).map(existing -> {
            bookingDetails.setBookingId(bookingId);
            return bookingRepo.save(bookingDetails);
        }).orElseThrow(() -> new RuntimeException("Booking not found with ID: " + bookingId));
    }

    @Override
    public void deleteBooking(Integer bookingId) {
        if (!bookingRepo.existsById(bookingId)) {
            throw new RuntimeException("Booking not found with ID: " + bookingId);
        }
        bookingRepo.deleteById(bookingId);
    }
}
