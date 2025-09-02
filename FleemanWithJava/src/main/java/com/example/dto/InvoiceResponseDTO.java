package com.example.dto;

import com.example.models.BookingDetails;
import com.example.models.VehicleDetails;

import java.sql.Timestamp;
import java.sql.Date;

public class InvoiceResponseDTO {
    private Integer invoiceId;
    private Timestamp invoiceDate;
    private BookingDetails booking;
    private Date actualReturnDate;
    private String dropLocation;
    private VehicleDetails car;
    private Float rentalAmount;
    private Float addonrentalAmount;

    // Getters and Setters

    public Integer getInvoiceId() {
        return invoiceId;
    }

    public void setInvoiceId(Integer invoiceId) {
        this.invoiceId = invoiceId;
    }

    public Timestamp getInvoiceDate() {
        return invoiceDate;
    }

    public void setInvoiceDate(Timestamp invoiceDate) {
        this.invoiceDate = invoiceDate;
    }

    public BookingDetails getBooking() {
        return booking;
    }

    public void setBooking(BookingDetails booking) {
        this.booking = booking;
    }

    public Date getActualReturnDate() {
        return actualReturnDate;
    }

    public void setActualReturnDate(Date actualReturnDate) {
        this.actualReturnDate = actualReturnDate;
    }

    public String getDropLocation() {
        return dropLocation;
    }

    public void setDropLocation(String dropLocation) {
        this.dropLocation = dropLocation;
    }

    public VehicleDetails getCar() {
        return car;
    }

    public void setCar(VehicleDetails car) {
        this.car = car;
    }

    public Float getRentalAmount() {
        return rentalAmount;
    }

    public void setRentalAmount(Float rentalAmount) {
        this.rentalAmount = rentalAmount;
    }

    public Float getAddonrentalAmount() {
        return addonrentalAmount;
    }

    public void setAddonrentalAmount(Float addonrentalAmount) {
        this.addonrentalAmount = addonrentalAmount;
    }
}
