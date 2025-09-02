package com.example.models;

import jakarta.persistence.*;

@Entity
@Table(name="add_on_details")
public class AddonDetails {
	
	@Id
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	@Column(name="add_on_id")
	private Short addOnId;
	
	@Column(name="add_on_name", unique=true, length=60, nullable=false)
	private String addOnName;
	
	@Column(name="add_on_price", nullable=false )
	private Double addOnPrice;

	//Default Constructor - Required for JPA/Hibernate
	public AddonDetails() {}
	
	//Parameterized Constructor, not necessary
	public AddonDetails(String addOnName, Double addOnPrice) {
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
