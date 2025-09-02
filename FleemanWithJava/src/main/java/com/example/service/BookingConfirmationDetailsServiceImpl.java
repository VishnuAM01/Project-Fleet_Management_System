package com.example.service;

import com.example.dto.BookingConfirmationDTO;
import com.example.models.BookingConfirmationDetails;
import com.example.models.CarDetails;
import com.example.repository.BookingConfirmationDetailsRepository;
import com.example.repository.BookingDetailsRepository;
import com.example.repository.CarDetailsRepository;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.util.List;
import java.util.Optional;

@Service
public class BookingConfirmationDetailsServiceImpl implements BookingConfirmationDetailsService {

    private final BookingConfirmationDetailsRepository repo;

    @Autowired
    private CarDetailsRepository carDetailsRepo;
    
    @Autowired
    private BookingDetailsRepository bookingDetailsRepository;


    @Autowired
    public BookingConfirmationDetailsServiceImpl(BookingConfirmationDetailsRepository repo) {
        this.repo = repo;
    }

    @Override
    @Transactional
    public BookingConfirmationDetails create(BookingConfirmationDTO dto) {
        // Get car by ID
        CarDetails car = carDetailsRepo.findById(dto.getCarId())
                .orElseThrow(() -> new RuntimeException("Car not found with ID: " + dto.getCarId()));

        // Check if already booked
        if (Boolean.FALSE.equals(car.getIsAvailable())) {
            throw new RuntimeException("Car with ID " + dto.getCarId() + " is already booked.");
        }

        // Set car availability to false
        car.setIsAvailable(false);
        carDetailsRepo.save(car);

        // Create and save booking confirmation
        BookingConfirmationDetails confirmation = new BookingConfirmationDetails();
        confirmation.setBookingId(dto.getBookingId());
        confirmation.setCar(car);

        BookingConfirmationDetails savedConfirmation = repo.save(confirmation);

        // 🆕 Delete the booking from booking_details table
       // bookingDetailsRepository.deleteById(dto.getBookingId().intValue());

        return savedConfirmation;
    }


    @Override
    public Optional<BookingConfirmationDetails> getById(Long bookingId) {
        return repo.findById(bookingId);
    }

    @Override
    public List<BookingConfirmationDetails> getAll() {
        return repo.findAll();
    }

    @Override
    @Transactional
    public BookingConfirmationDetails update(Long bookingId, BookingConfirmationDetails updated) {
        return repo.findById(bookingId).map(existing -> {
            existing.setCar(updated.getCar());
            return repo.save(existing);
        }).orElseThrow(() -> new RuntimeException("Booking confirmation not found with id: " + bookingId));
    }

    @Override
    @Transactional
    public void delete(Long bookingId) {
        Optional<BookingConfirmationDetails> confirmationOpt = repo.findById(bookingId);

        confirmationOpt.ifPresent(confirmation -> {
            CarDetails car = confirmation.getCar();
            if (car != null) {
                car.setIsAvailable(true); // Restore availability
                carDetailsRepo.save(car);
            }
        });

        repo.deleteById(bookingId);
    }
}
