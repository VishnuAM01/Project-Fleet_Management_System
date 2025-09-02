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
import java.text.SimpleDateFormat;
import java.time.LocalDate;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.List;
import java.util.Optional;

@Service
public class BookingDetailsServiceImpl implements BookingDetailsService {

    @Autowired
    private MemberDetailsRepository memberRepo;

    @Autowired
    private BookingDetailsRepository bookingRepo;

    @Autowired
    private AuthRolesRepository roleRepo;

    @Autowired
    private VehicleDetailsRepository vehicleRepo;

    @Autowired
    private AddOnDetailsRepository addonRepo;

    private final DateTimeFormatter dateFormatter = DateTimeFormatter.ofPattern("yyyy-MM-dd");
    private final DateTimeFormatter dateTimeFormatter = DateTimeFormatter.ofPattern("yyyy-MM-dd'T'HH:mm:ss");

    @Override
    public void createBooking(UserBookingRequestDTO request) {
        UserDetailsDTO user = request.getUserDetails();
        String email = user.getEmail();
        MemberDetails member;

        Optional<MemberDetails> optionalMember = memberRepo.findByEmail(email);
        if (optionalMember.isPresent()) {
            member = optionalMember.get();
            if (!member.isRegistered()) {
                member.setRegistered(true);
                memberRepo.save(member);
            }
        } else {
            member = new MemberDetails();
            member.setMemberId(generateMemberId());
            member.setEmail(email);
            member.setUsername(email);
            member.setFirstName(user.getFirstName());
            member.setLastName(user.getLastName());
            member.setAddress(user.getAddress1());
            member.setCity(user.getCity());
            member.setDob(parseDate(user.getBirthDate()));
            member.setMobileNumber(Long.parseLong(user.getCell()));
            member.setDrivingLicenseId(user.getDrivingLicense());
            member.setDrivingLicenseIssuedBy(parseDate(user.getDlIssuedBy()));
            member.setDrivingLicenseValidThru(parseDate(user.getDlValidThru()));
            member.setIdp(user.getIdpNo());
            member.setIdpIssuedBy(parseDate(user.getIdpIssuedBy()));
            member.setIdpValidThru(parseDate(user.getIdpValidThru()));
            member.setPassportNo(user.getPassportNo());
            member.setPassportIssuedBy(user.getPassportIssuedBy());
            member.setPassportValidate(parseDate(user.getPassportValid()));
            member.setCreditCardType(MemberDetails.CreditCardType.valueOf(user.getCreditCardType().toUpperCase()));
            member.setZipCode(Integer.parseInt(user.getZip()));
            member.setState("Maharashtra");
            member.setRegistered(true);

            AuthRoles userRole = roleRepo.findById(2)
                    .orElseThrow(() -> new RuntimeException("Role not found"));
            member.setRole(userRole);

            memberRepo.save(member);
        }

        // Parse pickup and return dates
        LocalDate pickupDate = parseDate(request.getPickupDate()).toLocalDate();
        LocalDate returnDate = parseDate(request.getReturnDate()).toLocalDate();

        // Fetch vehicle details
        Long vehicleId = request.getVehicleId();
        if (vehicleId == null) {
            throw new RuntimeException("Vehicle ID cannot be null");
        }

        VehicleDetails vehicle = vehicleRepo.findById(vehicleId)
                .orElseThrow(() -> new RuntimeException("Vehicle not found"));

        // Calculate base rate
        float baseRate = RateCalculator.calculateRate(
                pickupDate,
                returnDate,
                vehicle.getDailyRate(),
                vehicle.getWeeklyRate(),
                vehicle.getMonthlyRate()
        );

        // Calculate addon total
        float addonTotal = 0f;
        if (request.getSelectedAddons() != null) {
            for (String addonId : request.getSelectedAddons()) {
                AddonDetails addon = addonRepo.findById(Short.parseShort(addonId))
                        .orElseThrow(() -> new RuntimeException("Addon not found: " + addonId));
                addonTotal += addon.getAddOnPrice();
            }
        }

        float totalRate = baseRate + addonTotal;

        // Save booking
        BookingDetails booking = new BookingDetails();
        booking.setBookingDate(Timestamp.valueOf(LocalDateTime.now()));
        booking.setPickupLocation(user.getSelectedHub());
        booking.setDropLocation(user.getSelectedReturnHub());
        booking.setPickupDate(Date.valueOf(pickupDate));
        booking.setReturnDate(Date.valueOf(returnDate));
        booking.setVehicleId(vehicleId);
        booking.setRate(totalRate);
        booking.setRateType(RateType.valueOf(request.getRateType().toUpperCase()));
        booking.setMember(member);

        bookingRepo.save(booking);
    }

    private Date parseDate(String input) {
        if (input == null || input.isBlank()) return null;
        try {
            if (input.contains("T")) {
                LocalDateTime dateTime = LocalDateTime.parse(input, dateTimeFormatter);
                return Date.valueOf(dateTime.toLocalDate());
            } else {
                LocalDate date = LocalDate.parse(input, dateFormatter);
                return Date.valueOf(date);
            }
        } catch (Exception e) {
            throw new RuntimeException("Failed to parse date: " + input, e);
        }
    }

    private String generateMemberId() {
        long count = memberRepo.count() + 1;
        return "M" + String.format("%05d", count);
    }

    @Override
    public BookingDetails saveBooking(BookingDetails bookingDetails) {
        return bookingRepo.save(bookingDetails);
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

    public List<BookingDetails> getUnconfirmedBookings() {
        return bookingRepo.findUnconfirmedBookings();
    }

    public List<BookingDetails> getConfirmedBookings() {
        return bookingRepo.findConfirmedBookings();
    }

    @Override
    public void deleteBooking(Integer bookingId) {
        if (!bookingRepo.existsById(bookingId)) {
            throw new RuntimeException("Booking not found with ID: " + bookingId);
        }
        bookingRepo.deleteById(bookingId);
    }

    public BookingSummaryDTO getBookingById(Integer bookingId) {
        BookingDetails booking = bookingRepo.findById(bookingId)
                .orElseThrow(() -> new RuntimeException("Booking not found"));

        String memberName = booking.getMember().getFirstName() + " " + booking.getMember().getLastName();
        String pickupDate = booking.getPickupDate() != null
                ? new SimpleDateFormat("yyyy-MM-dd").format(booking.getPickupDate())
                : null;

        Long vehicleId = booking.getVehicleId();
        String vehicleType = null;

        if (vehicleId != null) {
            VehicleDetails vehicle = vehicleRepo.findById(vehicleId).orElse(null);
            if (vehicle != null) {
                vehicleType = vehicle.getVehicleType();
            }
        }

        return new BookingSummaryDTO(
                booking.getBookingId(),
                memberName,
                booking.getPickupLocation(),
                pickupDate,
                vehicleId,
                vehicleType
        );
    }
}
