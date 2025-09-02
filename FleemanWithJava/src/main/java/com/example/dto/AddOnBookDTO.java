package com.example.dto;

public class AddOnBookDTO {
	
	private Integer BookingId;
	private Short AddonId;
	public Integer getBookingId() {
		return BookingId;
	}
	public void setBookingId(Integer bookingId) {
		BookingId = bookingId;
	}
	public Short getAddonId() {
		return AddonId;
	}
	public void setAddonId(Short addonId) {
		this.AddonId = addonId;
	}
}
