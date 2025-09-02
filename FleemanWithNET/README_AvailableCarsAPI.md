# Available Cars API

This API provides endpoints to retrieve available cars filtered by vehicle type, ensuring only cars with `IsAvailable = true` are returned.

## Endpoints

### 1. Get Available Cars by Vehicle ID (Basic)
**GET** `/api/CarDetails/available/by-vehicle/{vehicleId}`

Returns all available cars for a specific vehicle type with basic car information.

**Response:**
```json
[
  {
    "car_Id": 1,
    "carName": "Toyota Camry",
    "fuelStatus": "Full",
    "imgPath": "/images/camry.jpg",
    "isAvailable": true,
    "locationId": 1,
    "maintenanceDate": "2024-02-15T00:00:00",
    "registrationNumber": "ABC123",
    "vehicleId": 5
  }
]
```

### 2. Get Available Cars by Vehicle ID (Detailed)
**GET** `/api/CarDetails/available/with-details/by-vehicle/{vehicleId}`

Returns all available cars for a specific vehicle type with comprehensive information including vehicle rates and location details.

**Response:**
```json
[
  {
    "carId": 1,
    "carName": "Toyota Camry",
    "fuelStatus": "Full",
    "imgPath": "/images/camry.jpg",
    "isAvailable": true,
    "locationId": 1,
    "maintenanceDate": "2024-02-15T00:00:00",
    "registrationNumber": "ABC123",
    "vehicleId": 5,
    "vehicleType": "Sedan",
    "dailyRate": 30.00,
    "weeklyRate": 180.00,
    "monthlyRate": 720.00,
    "locationName": "Downtown Hub",
    "cityName": "New York",
    "stateName": "NY",
    "status": "Available"
  }
]
```

## Business Logic

### Filtering Criteria
- **Vehicle Type**: Cars are filtered by the specified `vehicleId`
- **Availability**: Only cars with `IsAvailable = true` are returned
- **Active Status**: Ensures only cars that can be rented are shown

### Data Relationships
The detailed endpoint joins multiple tables to provide comprehensive information:
- **`car_details`**: Basic car information and availability status
- **`vehicle_details`**: Vehicle type and pricing information
- **`location_details`**: Pickup/drop-off location information
- **`city_details`**: City information for location context
- **`state_details`**: State information for location context

## Use Cases

1. **Car Selection**: Frontend can show available cars for a specific vehicle type
2. **Inventory Management**: Staff can see which cars are available for rental
3. **Customer Booking**: Display available options when customers select a vehicle type
4. **Fleet Planning**: Understand current availability of different vehicle categories
5. **Location-based Search**: Find available cars at specific locations

## Data Model

### AvailableCarDTO
- **Car Information**: ID, name, fuel status, image, registration
- **Availability**: Current availability status
- **Vehicle Details**: Type and pricing (daily, weekly, monthly rates)
- **Location Details**: Location name, city, and state
- **Status**: Always set to "Available"

## Error Handling

- **No Cars Found**: Returns 404 with descriptive message
- **Invalid Vehicle ID**: Returns 404 if vehicle type doesn't exist
- **Empty Results**: Returns 404 if no available cars for the vehicle type

## Performance Notes

- Uses efficient SQL JOINs for detailed information
- Filters are applied at the database level
- Consider adding indexes on:
  - `car_details.VehicleId`
  - `car_details.IsAvailable`
  - `car_details.LocationId`

## Examples

### Get Available Sedans
```bash
GET /api/CarDetails/available/by-vehicle/5
```

### Get Available SUVs with Full Details
```bash
GET /api/CarDetails/available/with-details/by-vehicle/3
```

### Get Available Luxury Cars
```bash
GET /api/CarDetails/available/with-details/by-vehicle/8
```

## Response Status Codes

- **200 OK**: Available cars found and returned
- **404 Not Found**: No available cars for the specified vehicle type
- **500 Internal Server Error**: Server error during processing

## Frontend Integration

### Basic Usage
```javascript
// Get available cars for vehicle type 5
fetch('/api/CarDetails/available/by-vehicle/5')
  .then(response => response.json())
  .then(cars => {
    // Display available cars
    console.log('Available cars:', cars);
  });
```

### Detailed Usage
```javascript
// Get available cars with full details
fetch('/api/CarDetails/available/with-details/by-vehicle/5')
  .then(response => response.json())
  .then(cars => {
    // Display cars with rates and location info
    cars.forEach(car => {
      console.log(`${car.carName} - $${car.dailyRate}/day at ${car.locationName}`);
    });
  });
```

## Business Rules

1. **Availability Check**: Only cars with `IsAvailable = true` are returned
2. **Vehicle Type Filter**: Results are filtered by the specified `vehicleId`
3. **Active Cars**: Only cars that are currently in service are included
4. **Location Context**: Provides location information for pickup/drop-off planning
5. **Pricing Information**: Includes current rates for customer decision-making
