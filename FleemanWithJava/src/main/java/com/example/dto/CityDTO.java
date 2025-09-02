package com.example.dto;

import com.fasterxml.jackson.annotation.JsonIgnore;

public class CityDTO {
    private String city_Name;
    private String state_Name;

    // constructor
    public CityDTO(String city_Name, String state_Name) {
        this.city_Name = city_Name;
        this.state_Name = state_Name;
    }

    
    public String getCity_Name() {
        return city_Name;
    }

    
    public String getState_Name() {
        return state_Name;
    }
}
