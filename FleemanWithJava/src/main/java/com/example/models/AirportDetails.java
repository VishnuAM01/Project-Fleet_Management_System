package com.example.models;
import jakarta.persistence.ForeignKey;
import jakarta.persistence.JoinColumn;
import jakarta.persistence.Column;
import jakarta.persistence.ConstraintMode;

import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.JoinColumn;
import jakarta.persistence.ManyToOne;
import jakarta.persistence.Table;

@Entity
@Table(name="Airport_Details")
public class AirportDetails {

	
	private int Airport_Id;
	@Column(unique=true,nullable=false)
	private String Airport_Name;
	@Column(unique=true,nullable=false)
	private String Airport_Code;
	
	@Id
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	public int getAirport_Id() {
		return Airport_Id;
	}
	public void setAirport_Id(int airport_Id) {
		Airport_Id = airport_Id;
	}
	@Column(unique=true,nullable=false)
	public String getAirport_Name() {
		return Airport_Name;
	}
	public void setAirport_Name(String airport_Name) {
		Airport_Name = airport_Name;
	}
	@Column(unique=true,nullable=false)
	public String getAirport_Code() {
		return Airport_Code;
	}
	public void setAirport_Code(String airport_Code) {
		Airport_Code = airport_Code;
	}
	

}
