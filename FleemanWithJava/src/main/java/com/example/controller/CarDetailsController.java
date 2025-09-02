package com.example.controller;

import java.util.List;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import com.example.dto.CarDetailsDTO;
import com.example.models.CarDetails;
import com.example.service.CarDetailsService;

/*
 * @RestController -> It tells Spring this class handles HTTP requests and returns JSON (or XML) directly as the response body.
 * @RequestMapping("/api/car") -> Defines the base URL for all endpoints in this controller.
 * @CrossOrigin("*") ->Enables CORS (Cross-Origin Resource Sharing). * allow requests from any front-end origin(ie, any port).
 */
@RestController
@RequestMapping("/api/car")
@CrossOrigin("*")
public class CarDetailsController {
	/*
	 * @Autowired automatically injects the bean CarDetailsService into this class
	 */
	@Autowired
	private CarDetailsService carService;
	
	@GetMapping
	public List<CarDetailsDTO> getAllCars(){
		return carService.getAllCars();
	}
	/*
	 * @PathVariable binds the URL path segment to the method parameter (i.e, Gets values from the URL path)
	 */
	@GetMapping("/{id}")
	public CarDetailsDTO getCarById(@PathVariable Integer id) {
		return carService.getCarById(id);
	}
	
	/*
	 * @RequestBody binds the incoming JSON request body to a java object.
	 */
	@PostMapping
	public CarDetailsDTO addCar(@RequestBody CarDetailsDTO dto) {
		return carService.addCar(dto);
	}
	
	@PutMapping("/{id}")
	public CarDetailsDTO updateCar(@PathVariable Integer id, @RequestBody CarDetailsDTO dto) {
		return carService.updateCar(id, dto);
	}
	
	@DeleteMapping("/{id}")
	public void deleteCar(@PathVariable Integer id) {
		carService.deleteCar(id);
	}
	@GetMapping("/by-vehicle/{vehicleId}")
	public ResponseEntity<List<CarDetailsDTO>> getCarsByVehicleId(@PathVariable("vehicleId") Long vehicleId) {
	    List<CarDetailsDTO> cars = carService.getCarsByVehicleId(vehicleId);
	    return ResponseEntity.ok(cars);
	}
	
	@PutMapping("/{carId}/fuel-status")
	public ResponseEntity<String> updateFuelStatus(
	        @PathVariable("carId") Integer carId,
	        @RequestParam("fuelStatus") String fuelStatus) {
	    try {
	        carService.updateFuelStatusByCarId(carId, fuelStatus);
	        return ResponseEntity.ok("Fuel status updated successfully for car ID: " + carId);
	    } catch (RuntimeException e) {
	        return ResponseEntity.status(404).body("Error: " + e.getMessage());
	    }
	}

	}

