package com.example.service;

import com.example.dto.InvoiceRequestDTO;
import com.example.dto.InvoiceResponseDTO;
import com.example.models.AddonDetails;
import com.example.models.BookingDetails;
import com.example.models.InvoiceHeaderTable;
import com.example.models.VehicleDetails;
import com.example.repository.AddOnDetailsRepository;
import com.example.repository.BookingDetailsRepository;
import com.example.repository.InvoiceHeaderTableRepository;
import com.example.repository.LocationDetailsRepository;
import com.example.repository.VehicleDetailsRepository;
import com.example.utils.RateCalculator;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import java.sql.Timestamp;

@Service
public class InvoiceHeaderTableServiceImpl implements InvoiceHeaderTableService {

    @Autowired
    private BookingDetailsRepository bookingRepo;

    @Autowired
    private VehicleDetailsRepository vehicleRepo;

    @Autowired
    private AddOnDetailsRepository addonRepo;

    @Autowired
    private LocationDetailsRepository dropLocationRepo;

    @Autowired
    private InvoiceHeaderTableRepository repo;

    @Override
    @Transactional
    public InvoiceResponseDTO create(InvoiceRequestDTO request) {
        // Fetch booking details
        BookingDetails booking = bookingRepo.findById(request.getBookingId())
                .orElseThrow(() -> new RuntimeException("Booking not found"));

        // Fetch vehicle details
        VehicleDetails vehicle = vehicleRepo.findById(request.getCarId().longValue())
                .orElseThrow(() -> new RuntimeException("Car not found"));

        // Calculate rental base rate
        float baseRate = RateCalculator.calculateRate(
                booking.getPickupDate().toInstant().atZone(java.time.ZoneId.systemDefault()).toLocalDate(),
                booking.getReturnDate().toInstant().atZone(java.time.ZoneId.systemDefault()).toLocalDate(),
                vehicle.getDailyRate(),
                vehicle.getWeeklyRate(),
                vehicle.getMonthlyRate()
        );

        // Calculate addon total
        float addonTotal = 0f;
        if (request.getSelectedAddons() != null) {
            for (String addonIdStr : request.getSelectedAddons()) {
                Short addonId = Short.parseShort(addonIdStr);
                AddonDetails addon = addonRepo.findById(addonId)
                        .orElseThrow(() -> new RuntimeException("Addon not found: " + addonId));
                addonTotal += addon.getAddOnPrice();
            }
        }

        // Create invoice entity and set fields
        InvoiceHeaderTable invoice = new InvoiceHeaderTable();
        invoice.setBookingId(request.getBookingId());
        invoice.setCarId(request.getCarId());
        invoice.setDropLocation(
                dropLocationRepo.findById(request.getDropLocationId())
                        .orElseThrow(() -> new RuntimeException("Drop location not found"))
                        .getLocation_Name()
        );
        invoice.setInvoiceDate(new Timestamp(System.currentTimeMillis()));
        invoice.setActualReturnDate(new java.sql.Date(booking.getReturnDate().getTime()));
        invoice.setRentalAmount(baseRate);
        invoice.setAddonrentalAmount(addonTotal);

        // Save invoice
        InvoiceHeaderTable savedInvoice = repo.save(invoice);

        // Build and return the DTO with full nested objects
        InvoiceResponseDTO response = new InvoiceResponseDTO();
        response.setInvoiceId(savedInvoice.getInvoiceId());
        response.setInvoiceDate(savedInvoice.getInvoiceDate());
        response.setBooking(booking);
        response.setCar(vehicle);
        response.setActualReturnDate(savedInvoice.getActualReturnDate());
        response.setDropLocation(savedInvoice.getDropLocation());
        response.setRentalAmount(savedInvoice.getRentalAmount());
        response.setAddonrentalAmount(savedInvoice.getAddonrentalAmount());

        return response;
    }
    
    @Override
    public InvoiceResponseDTO getByBookingId(Integer bookingId) {
        InvoiceHeaderTable invoice = repo.findByBookingId(bookingId)
                .orElseThrow(() -> new RuntimeException("Invoice not found for booking id: " + bookingId));

        // Build InvoiceResponseDTO like in getById method
        BookingDetails booking = bookingRepo.findById(invoice.getBookingId())
                .orElseThrow(() -> new RuntimeException("Booking not found"));

        VehicleDetails vehicle = vehicleRepo.findById(invoice.getCarId().longValue())
                .orElseThrow(() -> new RuntimeException("Car not found"));

        InvoiceResponseDTO response = new InvoiceResponseDTO();
        response.setInvoiceId(invoice.getInvoiceId());
        response.setInvoiceDate(invoice.getInvoiceDate());
        response.setBooking(booking);
        response.setCar(vehicle);
        response.setActualReturnDate(invoice.getActualReturnDate());
        response.setDropLocation(invoice.getDropLocation());
        response.setRentalAmount(invoice.getRentalAmount());
        response.setAddonrentalAmount(invoice.getAddonrentalAmount());

        return response;
    }

    
    
    public InvoiceResponseDTO getById(Integer invoiceId) {
        InvoiceHeaderTable invoice = repo.findById(invoiceId)
            .orElseThrow(() -> new RuntimeException("Invoice not found"));

        BookingDetails booking = bookingRepo.findById(invoice.getBookingId())
            .orElseThrow(() -> new RuntimeException("Booking not found"));

        VehicleDetails vehicle = vehicleRepo.findById(invoice.getCarId().longValue())
            .orElseThrow(() -> new RuntimeException("Car not found"));

        InvoiceResponseDTO response = new InvoiceResponseDTO();
        response.setInvoiceId(invoice.getInvoiceId());
        response.setInvoiceDate(invoice.getInvoiceDate());
        response.setBooking(booking);
        response.setCar(vehicle);
        response.setActualReturnDate(invoice.getActualReturnDate());
        response.setDropLocation(invoice.getDropLocation());
        response.setRentalAmount(invoice.getRentalAmount());
        response.setAddonrentalAmount(invoice.getAddonrentalAmount());

        return response;
    }


    // ... Other methods (getById, update, delete, etc.) unchanged ...
}
