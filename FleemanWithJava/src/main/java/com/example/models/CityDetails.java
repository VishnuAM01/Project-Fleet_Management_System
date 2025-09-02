package com.example.models;

import jakarta.persistence.CascadeType;
import jakarta.persistence.ConstraintMode;
import jakarta.persistence.Entity;
import jakarta.persistence.ForeignKey;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.JoinColumn;
import jakarta.persistence.ManyToOne;
import jakarta.persistence.Table;

@Entity
@Table(name="City_Details")
public class CityDetails {

	private int City_Id;
	private String City_Name;
	private StateDetails state;
	
	
	@ManyToOne
	@JoinColumn(name = "State_Id",foreignKey = @ForeignKey(ConstraintMode.NO_CONSTRAINT))
	public StateDetails getState() {
		return state;
	}
	public void setState(StateDetails state) {
		this.state = state;
	}
	@Id
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	public int getCity_Id() {
		return City_Id;
	}
	public void setCity_Id(int city_Id) {
		City_Id = city_Id;
	}
	public String getCity_Name() {
		return City_Name;
	}
	public void setCity_Name(String city_Name) {
		City_Name = city_Name;
	}

}
