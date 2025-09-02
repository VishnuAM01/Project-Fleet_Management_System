package com.example.models;

import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.Table;

@Entity
@Table(name="State_Details")
public class StateDetails {

	private int State_Id;
	private String State_Name;
	
	@Id
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	public int getState_Id() {
		return State_Id;
	}
	public void setState_Id(int state_Id) {
		State_Id = state_Id;
	}
	public String getState_Name() {
		return State_Name;
	}
	public void setState_Name(String state_Name) {
		State_Name = state_Name;
	}
	
	

}
