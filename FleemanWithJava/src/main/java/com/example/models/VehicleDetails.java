package com.example.models;
import jakarta.persistence.*;

@Entity
@Table(name="VehicleDetails")
public class VehicleDetails{
	@Id
	private long vehicleId;
	
	@Column(length=60)
	private String vehicleType;
	
	private Float dailyRate;
	
	private Float weeklyRate;
	
	private Float monthlyRate;
	
	private String imgPath;
	
	public long getVehicleId() {
		return  vehicleId;
	}

	public void setVehicleId(Short vehicleId) {
		this.vehicleId = vehicleId;
	}

	public String getVehicleType() {
		return vehicleType;
	}

	public void setVehicleType(String vehicleType) {
		this.vehicleType = vehicleType;
	}

	public Float getDailyRate() {
		return dailyRate;
	}

	public void setDailyRate(Float dailyRate) {
		this.dailyRate = dailyRate;
	}

	public Float getWeeklyRate() {
		return weeklyRate;
	}

	public void setWeeklyRate(Float weeklyRate) {
		this.weeklyRate = weeklyRate;
	}

	public Float getMonthlyRate() {
		return monthlyRate;
	}

	public void setMonthlyRate(Float monthlyRate) {
		this.monthlyRate = monthlyRate;
	}

	public String getImgPath() {
		return imgPath;
	}

	public void setImgPath(String imgPath) {
		this.imgPath = imgPath;
	}
}

