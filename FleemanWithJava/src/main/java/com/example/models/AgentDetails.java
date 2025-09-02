package com.example.models;

import jakarta.persistence.Column;
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
@Table(name="Agent_Details")
public class AgentDetails {

	private int Agent_Id;
	
	@Column(unique=true,nullable=false)
	private String Agent_User_Name;
	private String Agent_First_Name;
	private String Agent_Last_Name;
	
	@Column(unique=true,nullable=false)
	private String Email;
	
	
	@Column(unique=true,nullable=false)
	private Long Mobile_Number;	
	
	private AuthRoles role;
	

	@Id
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	public int getAgent_Id() {
		return Agent_Id;
	}
	public void setAgent_Id(int agent_Id) {
		Agent_Id = agent_Id;
	}
	@Column(unique=true,nullable=false)
	public String getAgent_User_Name() {
		return Agent_User_Name;
	}
	public void setAgent_User_Name(String agent_User_Name) {
		Agent_User_Name = agent_User_Name;
	}
	public String getAgent_First_Name() {
		return Agent_First_Name;
	}
	public void setAgent_First_Name(String agent_First_Name) {
		Agent_First_Name = agent_First_Name;
	}
	public String getAgent_Last_Name() {
		return Agent_Last_Name;
	}
	public void setAgent_Last_Name(String agent_Last_Name) {
		Agent_Last_Name = agent_Last_Name;
	}
	@ManyToOne
	@JoinColumn(name = "role_id",foreignKey = @ForeignKey(ConstraintMode.NO_CONSTRAINT))
	public AuthRoles getRole() {
		return role;
	}
	public void setRole(AuthRoles role) {
		this.role = role;
	}
	@Column(unique=true,nullable=false)
	public String getEmail() {
		return Email;
	}
	public void setEmail(String email) {
		Email = email;
	}
	@Column(unique=true,nullable=false)
	public Long getMobile_Number() {
		return Mobile_Number;
	}
	public void setMobile_Number(Long mobile_Number) {
		Mobile_Number = mobile_Number;
	}

}
