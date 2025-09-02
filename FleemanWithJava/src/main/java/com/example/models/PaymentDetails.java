package com.example.models;

import java.sql.Date;
import jakarta.persistence.*;

@Entity
@Table(name = "Payment_Details")
public class PaymentDetails {

    public enum PaymentType {
        CREDIT_CARD, DEBIT_CARD, NET_BANKING, UPI, CASH
    }

    public enum PaymentStatus {
        SUCCESSFUL, FAILED
    }

    @Id
    private String paymentId;

    @OneToOne
    @JoinColumn(name = "booking_id", referencedColumnName = "booking_id", foreignKey = @ForeignKey(ConstraintMode.NO_CONSTRAINT))
    private BookingDetails booking; // full booking object instead of bookingId

    private Date paymentDate;
    private String transactionId;
    private Float rentalAmount;

    @Enumerated(EnumType.STRING)
    private PaymentType paymentType;

    @Enumerated(EnumType.STRING)
    private PaymentStatus paymentStatus;

    // Getters and Setters
    public String getPaymentId() {
        return paymentId;
    }
    public void setPaymentId(String paymentId) {
        this.paymentId = paymentId;
    }

    public BookingDetails getBooking() {
        return booking;
    }
    public void setBooking(BookingDetails booking) {
        this.booking = booking;
    }

    public Date getPaymentDate() {
        return paymentDate;
    }
    public void setPaymentDate(Date paymentDate) {
        this.paymentDate = paymentDate;
    }

    public String getTransactionId() {
        return transactionId;
    }
    public void setTransactionId(String transactionId) {
        this.transactionId = transactionId;
    }

    public Float getRentalAmount() {
        return rentalAmount;
    }
    public void setRentalAmount(Float rentalAmount) {
        this.rentalAmount = rentalAmount;
    }

    public PaymentType getPaymentType() {
        return paymentType;
    }
    public void setPaymentType(PaymentType paymentType) {
        this.paymentType = paymentType;
    }

    public PaymentStatus getPaymentStatus() {
        return paymentStatus;
    }
    public void setPaymentStatus(PaymentStatus paymentStatus) {
        this.paymentStatus = paymentStatus;
    }
}
