package com.example.models;

import java.sql.Date;
import java.sql.Timestamp;
import jakarta.persistence.*;

@Entity
@Table(name = "InvoiceHeaderTable")
public class InvoiceHeaderTable {
    
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Integer invoiceId;
    
    private Timestamp invoiceDate;
    
    private Integer bookingId;
    
    private Date actualReturnDate; // New column for actual return date
    
    @Column(length = 60)
    private String dropLocation;

    private Short carId;
    
    private Float CarrentalAmount;
    private Float AddonrentalAmount;

    /* Getters and Setters */
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

    public Integer getBookingId() {
        return bookingId;
    }
    public void setBookingId(Integer bookingId) {
        this.bookingId = bookingId;
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


    public Short getCarId() {
        return carId;
    }
    public void setCarId(Short carId) {
        this.carId = carId;
    }

    public Float getRentalAmount() {
        return CarrentalAmount;
    }
    public void setRentalAmount(Float rentalAmount) {
        this.CarrentalAmount = rentalAmount;
    }
	public Float getAddonrentalAmount() {
		return AddonrentalAmount;
	}
	public void setAddonrentalAmount(Float addonrentalAmount) {
		AddonrentalAmount = addonrentalAmount;
	}
}
