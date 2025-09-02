package com.example.dto;

import java.sql.Date;

import com.example.models.VehicleDetails;

public class CarDetailsDTO {
	private Integer carId;
	private String carName;
	private String registrationNumber;
	private String fuelStatus;
	private Date maintenanceDate;
	private Long vehicleId;
	private String imgPath;
	private Boolean isAvailable;
	private String locationId;
	
	 private VehicleDetails vehicle;

	public VehicleDetails getVehicle() {
		return vehicle;
	}
	 public void setVehicle(VehicleDetails vehicle) {
		 this.vehicle = vehicle;
	 }
	// Getters & Setters
	public Integer getCarId() {
		return carId;
	}
	public void setCarId(Integer carId) {
		this.carId = carId;
	}
	public String getCarName() {
		return carName;
	}
	public void setCarName(String carName) {
		this.carName = carName;
	}
	public String getRegistrationNumber() {
		return registrationNumber;
	}
	public void setRegistrationNumber(String registrationNumber) {
		this.registrationNumber = registrationNumber;
	}
	public String getFuelStatus() {
		return fuelStatus;
	}
	public void setFuelStatus(String fuelStatus) {
		this.fuelStatus = fuelStatus;
	}
	public Date getMaintenanceDate() {
		return maintenanceDate;
	}
	public void setMaintenanceDate(Date maintenanceDate) {
		this.maintenanceDate = maintenanceDate;
	}
	public Long getVehicleId() {
		return vehicleId;
	}
	public void setVehicleId(Long vehicleId) {
		this.vehicleId = vehicleId;
	}
	public String getImgPath() {
		return imgPath;
	}
	public void setImgPath(String imgPath) {
		this.imgPath = imgPath;
	}
	public Boolean getIsAvailable() {
		return isAvailable;
	}
	public void setIsAvailable(Boolean isAvailable) {
		this.isAvailable = isAvailable;
	}
	public String getLocationId() {
		return locationId;
	}
	public void setLocationId(String locationId) {
		this.locationId = locationId;
	}
}
