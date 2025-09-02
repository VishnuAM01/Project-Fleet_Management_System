package com.example.dto;

import java.util.List;

public class InvoiceRequestDTO {
    private Integer bookingId;
    private Short carId;
    private Integer dropLocationId;
    private List<String> selectedAddons;
   	public List<String> getSelectedAddons() {
   		return selectedAddons;
   	}
   	public void setSelectedAddons(List<String> selectedAddons) {
   		this.selectedAddons = selectedAddons;
   	}
	public Integer getBookingId() {
		return bookingId;
	}
	public void setBookingId(Integer bookingId) {
		this.bookingId = bookingId;
	}
	public Short getCarId() {
		return carId;
	}
	public void setCarId(Short carId) {
		this.carId = carId;
	}
	public Integer getDropLocationId() {
		return dropLocationId;
	}
	public void setDropLocationId(Integer dropLocationId) {
		this.dropLocationId = dropLocationId;
	}
    
}
