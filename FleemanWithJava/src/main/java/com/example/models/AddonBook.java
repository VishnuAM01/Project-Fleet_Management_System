package com.example.models;
import jakarta.persistence.*;
@Entity
@Table(name="Add_on_Book")
public class AddonBook {
	@Id
	private Integer bookingId;	
	private  Short AddonId;

	/*Getter-Setters*/
	public Integer getBookingId() {
		return bookingId;
	}

	public void setBookingId(Integer bookingId) {
		this.bookingId = bookingId;
	}

	public Short getAddonId() {
		return AddonId;
	}

	public void setAddonId(Short addonId) {
		AddonId = addonId;
	}

}
