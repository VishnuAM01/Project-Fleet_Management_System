package com.example.dto;

public class LocationNameAddressDTO {
    private String name;
    private String address;
    private String mobileNumber;

    public LocationNameAddressDTO() {}

    public LocationNameAddressDTO(String name, String address, String mobileNumber) {
        this.name = name;
        this.address = address;
        this.mobileNumber = mobileNumber;
    }

    public String getName() { return name; }
    public void setName(String name) { this.name = name; }

    public String getAddress() { return address; }
    public void setAddress(String address) { this.address = address; }

    public String getMobileNumber() { return mobileNumber; }
    public void setMobileNumber(String mobileNumber) { this.mobileNumber = mobileNumber; }
}
