
package com.example.models;

import jakarta.persistence.*;
import java.util.Date;
import java.sql.Timestamp;

import com.example.utils.RateType;

@Entity
@Table(name = "Booking_Details")
public class BookingDetails {

	@Id
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	@Column(name = "booking_id")
	private Integer bookingId;


    @OneToOne
    @JoinColumn(name = "member_id", referencedColumnName = "member_id", foreignKey = @ForeignKey(ConstraintMode.NO_CONSTRAINT))
    private MemberDetails member;


    private Timestamp bookingDate;

    @Column(length = 60)
    
    private Long vehicleId;

    private Float rate;

    @Enumerated(EnumType.STRING)
    private RateType rateType;

    private Date pickupDate;
    private Date returnDate;

    @Column(length = 60)
    private String pickupLocation;

    @Column(length = 60)
    private String dropLocation;

    // Getters and Setters

    public Integer getBookingId() {
        return bookingId;
    }

    public void setBookingId(Integer bookingId) {
        this.bookingId = bookingId;
    }

    public MemberDetails getMember() {
        return member;
    }

    public void setMember(MemberDetails member) {
        this.member = member;
    }

    public Timestamp getBookingDate() {
        return bookingDate;
    }

    public void setBookingDate(Timestamp bookingDate) {
        this.bookingDate = bookingDate;
    }

    public Long getVehicleId() {
        return vehicleId;
    }

    public void setVehicleId(Long VehicleId) {
        this.vehicleId = VehicleId;
    }

    public Float getRate() {
        return rate;
    }

    public void setRate(Float rate) {
        this.rate = rate;
    }

    public RateType getRateType() {
        return rateType;
    }

    public void setRateType(RateType rateType) {
        this.rateType = rateType;
    }

    public Date getPickupDate() {
        return pickupDate;
    }

    public void setPickupDate(Date pickupDate) {
        this.pickupDate = pickupDate;
    }

    public Date getReturnDate() {
        return returnDate;
    }

    public void setReturnDate(Date returnDate) {
        this.returnDate = returnDate;
    }

    public String getPickupLocation() {
        return pickupLocation;
    }

    public void setPickupLocation(String pickupLocation) {
        this.pickupLocation = pickupLocation;
    }

    public String getDropLocation() {
        return dropLocation;
    }

    public void setDropLocation(String dropLocation) {
        this.dropLocation = dropLocation;
    }
}
