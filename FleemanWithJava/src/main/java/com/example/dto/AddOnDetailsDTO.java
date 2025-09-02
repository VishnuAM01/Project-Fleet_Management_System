package com.example.dto;

public class AddOnDetailsDTO {
	
	private Short addOnId;
	private String addOnName;
	private Double addOnPrice;
	
	//Constructor
	public AddOnDetailsDTO() {}

	public AddOnDetailsDTO(Short addOnId, String addOnName, Double addOnPrice) {
		this.addOnId = addOnId;
		this.addOnName = addOnName;
		this.addOnPrice = addOnPrice;
	}
	
	//Getters & Setters
	public Short getAddOnId() {
		return addOnId;
	}

	public void setAddOnId(Short addOnId) {
		this.addOnId = addOnId;
	}

	public String getAddOnName() {
		return addOnName;
	}

	public void setAddOnName(String addOnName) {
		this.addOnName = addOnName;
	}

	public Double getAddOnPrice() {
		return addOnPrice;
	}

	public void setAddOnPrice(Double addOnPrice) {
		this.addOnPrice = addOnPrice;
	}

}
