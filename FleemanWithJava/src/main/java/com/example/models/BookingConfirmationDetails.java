package com.example.models;

import jakarta.persistence.*;

@Entity
@Table(name = "Booking_Confirmation_Details")
public class BookingConfirmationDetails {

    @Id
    @Column(name = "booking_id")
    private Long bookingId; // still the primary key

    @OneToOne(fetch = FetchType.EAGER)
    @JoinColumn(name = "booking_id", referencedColumnName = "bookingId", insertable = false, updatable = false)
    private BookingDetails booking; // this is your object

    @OneToOne
    @JoinColumn(name = "car_id", nullable = false)
    private CarDetails car;

    // Getters & Setters
    public Long getBookingId() {
        return bookingId;
    }
    public void setBookingId(Long bookingId) {
        this.bookingId = bookingId;
    }

    public BookingDetails getBooking() {
        return booking;
    }
    public void setBooking(BookingDetails booking) {
        this.booking = booking;
    }

    public CarDetails getCar() {
        return car;
    }
    public void setCar(CarDetails car) {
        this.car = car;
    }
}
