package com.example.dto;

public class CarVehicleDTO {

    private String carImg;
    private String vehicleType;
    private Float dailyRate;
    private Float weeklyRate;
    private Float monthlyRate;

    public CarVehicleDTO(String carImg, String vehicleType, Float dailyRate, Float weeklyRate, Float monthlyRate) {
        this.carImg = carImg;
        this.vehicleType = vehicleType;
        this.dailyRate = dailyRate;
        this.weeklyRate = weeklyRate;
        this.monthlyRate = monthlyRate;
    }

    // Getters and Setters
    public String getCarImg() { return carImg; }
    public void setCarImg(String carImg) { this.carImg = carImg; }

    public String getVehicleType() { return vehicleType; }
    public void setVehicleType(String vehicleType) { this.vehicleType = vehicleType; }

    public Float getDailyRate() { return dailyRate; }
    public void setDailyRate(Float dailyRate) { this.dailyRate = dailyRate; }

    public Float getWeeklyRate() { return weeklyRate; }
    public void setWeeklyRate(Float weeklyRate) { this.weeklyRate = weeklyRate; }

    public Float getMonthlyRate() { return monthlyRate; }
    public void setMonthlyRate(Float monthlyRate) { this.monthlyRate = monthlyRate; }
}
