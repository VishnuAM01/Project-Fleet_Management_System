package com.example.dto;



public class BookingSummaryDTO {
    private Integer bookingId;
    private String memberName;
    private String pickupLocation;
    private String pickupDate;
    private Long vehicleId;
    private String vehicleType;

    public BookingSummaryDTO() {}

    public BookingSummaryDTO(Integer bookingId, String memberName, String pickupLocation,
                             String pickupDate, Long vehicleId, String vehicleType) {
        this.bookingId = bookingId;
        this.memberName = memberName;
        this.pickupLocation = pickupLocation;
        this.pickupDate = pickupDate;
        this.vehicleId = vehicleId;
        this.vehicleType = vehicleType;
    }

	public Integer getBookingId() {
		return bookingId;
	}

	public void setBookingId(Integer bookingId) {
		this.bookingId = bookingId;
	}

	public String getMemberName() {
		return memberName;
	}

	public void setMemberName(String memberName) {
		this.memberName = memberName;
	}

	public String getPickupLocation() {
		return pickupLocation;
	}

	public void setPickupLocation(String pickupLocation) {
		this.pickupLocation = pickupLocation;
	}

	public String getPickupDate() {
		return pickupDate;
	}

	public void setPickupDate(String pickupDate) {
		this.pickupDate = pickupDate;
	}

	public Long getVehicleId() {
		return vehicleId;
	}

	public void setVehicleId(Long vehicleId) {
		this.vehicleId = vehicleId;
	}

	public String getVehicleType() {
		return vehicleType;
	}

	public void setVehicleType(String vehicleType) {
		this.vehicleType = vehicleType;
	}

    // Getters and Setters
    
}

