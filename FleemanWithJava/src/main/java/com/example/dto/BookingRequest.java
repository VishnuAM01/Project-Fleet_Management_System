package com.example.dto;

import java.util.List;

public class BookingRequest {
    private String pickupDate;
    private String returnDate;
    private String selectedReturnHub;
    public String getSelectedReturnHub() {
		return selectedReturnHub;
	}


	public void setSelectedReturnHub(String selectedReturnHub) {
		this.selectedReturnHub = selectedReturnHub;
	}


	private String selectedHub;
    private long selectedVehicleId;
    private String selectedRateType;
    private List<String> selectedAddons;
    
    
    public String getPickupDate() {
		return pickupDate;
	}


	public void setPickupDate(String pickupDate) {
		this.pickupDate = pickupDate;
	}


	public String getReturnDate() {
		return returnDate;
	}


	public void setReturnDate(String returnDate) {
		this.returnDate = returnDate;
	}



	public String getSelectedHub() {
		return selectedHub;
	}


	public void setSelectedHub(String selectedHub) {
		this.selectedHub = selectedHub;
	}


	public long getSelectedVehicleId() {
		return selectedVehicleId;
	}


	public void setSelectedVehicleId(long selectedVehicleId) {
		this.selectedVehicleId = selectedVehicleId;
	}


	public String getSelectedRateType() {
		return selectedRateType;
	}


	public void setSelectedRateType(String selectedRateType) {
		this.selectedRateType = selectedRateType;
	}


	public List<String> getSelectedAddons() {
		return selectedAddons;
	}


	public void setSelectedAddons(List<String> selectedAddons) {
		this.selectedAddons = selectedAddons;
	}


	public static class Location {
        private String type;
        private String value;
		public String getType() {
			return type;
		}
		public void setType(String type) {
			this.type = type;
		}
		public String getValue() {
			return value;
		}
		public void setValue(String value) {
			this.value = value;
		}
        
        
    }
}
