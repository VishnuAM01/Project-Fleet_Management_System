package com.example.dto;

import java.util.List;

public class LocationResponseDTO {
    private String city;
    private String airport;
    private List<LocationNameAddressDTO> locationNames;

    public LocationResponseDTO() {}

    public LocationResponseDTO(String city, String airport, List<LocationNameAddressDTO> locationNames) {
        this.city = city;
        this.airport = airport;
        this.locationNames = locationNames;
    }

    public String getCity() { return city; }
    public void setCity(String city) { this.city = city; }

    public String getAirport() { return airport; }
    public void setAirport(String airport) { this.airport = airport; }

    public List<LocationNameAddressDTO> getLocationNames() { return locationNames; }
    public void setLocationNames(List<LocationNameAddressDTO> locationNames) { this.locationNames = locationNames; }
}
