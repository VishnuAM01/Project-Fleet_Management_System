package com.example.models;
import java.sql.Date;
import jakarta.persistence.*;

@Entity
@Table(name="Car_Details")
public class CarDetails {
	@Id
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	private Integer carId;
	
	@Column(length=60)
	private String carName;
	
	@Column(length=60)
	private String registrationNumber;
	
	@Column(length=20)
	private String fuelStatus;
	
	private Date maintenanceDate;
	
	@ManyToOne
	@JoinColumn(name = "vehicleId", foreignKey = @ForeignKey(ConstraintMode.NO_CONSTRAINT))
	private VehicleDetails vehicleDetails;
	
	private String imgPath;
	
	private Boolean isAvailable;
	
	private String locationId;

	/*Getter-Setters*/
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

	public String getRegistrationNUmber() {
		return registrationNumber;
	}

	public void setRegistrationNUmber(String registrationNumber) {
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

	public VehicleDetails getVehicleDetails() {
		return vehicleDetails;
	}

	public void setVehicleDetails(VehicleDetails vehicleDetails) {
		this.vehicleDetails = vehicleDetails;
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

