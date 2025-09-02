# Confirmed Bookings API

This API provides endpoints to retrieve confirmed bookings with comprehensive details including both booking ID and car ID.

## Endpoints

### 1. Get All Confirmed Bookings with Details
**GET** `/api/BookingConfirmationDetails/confirmed-with-details`

Returns all confirmed bookings with detailed information including both `BookingId` and `CarId`.

**Response:**
```json
[
  {
    "bookingId": 1,
    "carId": 5,
    "bookingDate": "2024-01-15T10:00:00",
    "pickupDate": "2024-01-20T09:00:00",
    "returnDate": "2024-01-25T17:00:00",
    "pickupLocation": 1,
    "dropLocation": 2,
    "vehicleId": 5,
    "memberId": 10,
    "carName": "Toyota Camry",
    "carModel": "SE",
    "carBrand": "Toyota",
    "carYear": 2024,
    "carColor": "Silver",
    "carLicensePlate": "ABC123",
    "memberFirstName": "John",
    "memberLastName": "Doe",
    "email": "john.doe@example.com",
    "mobileNumber": "1234567890",
    "pickupLocationName": "Downtown Hub",
    "dropLocationName": "Airport Hub",
    "vehicleName": "Sedan",
    "vehicleModel": "Standard",
    "status": "Confirmed"
  }
]
```

### 2. Get Filtered Confirmed Bookings with Details
**GET** `/api/BookingConfirmationDetails/confirmed-with-details/filtered?carId={id}&memberId={id}&fromDate={date}&toDate={date}`

Returns filtered confirmed bookings based on optional parameters.

**Query Parameters:**
- `carId` (optional): Filter bookings for a specific car
- `memberId` (optional): Filter bookings for a specific member
- `fromDate` (optional): Filter bookings from this date onwards
- `toDate` (optional): Filter bookings up to this date

**Examples:**
```
GET /api/BookingConfirmationDetails/confirmed-with-details/filtered?carId=5
GET /api/BookingConfirmationDetails/confirmed-with-details/filtered?memberId=10
GET /api/BookingConfirmationDetails/confirmed-with-details/filtered?fromDate=2024-01-01&toDate=2024-01-31
GET /api/BookingConfirmationDetails/confirmed-with-details/filtered?carId=5&memberId=10
```

## Business Logic

The API retrieves confirmed bookings by:
1. Joining `booking_confirmation_details` with `booking_details` on `BookingId`
2. Joining with `car_details` on `Car_Id` to get car information
3. Joining with `member_details` to get customer information
4. Joining with `location_details` for pickup and drop location names
5. Joining with `vehicle_details` for vehicle category information

## Data Model

### ConfirmedBookingDTO
- **Primary Keys**: `BookingId`, `CarId`
- **Booking Details**: Dates, locations, vehicle, member
- **Car Details**: Name, model, brand, year, color, license plate
- **Member Details**: First name, last name, email, mobile
- **Location Details**: Pickup and drop location names
- **Vehicle Details**: Vehicle category name and model
- **Status**: Always set to "Confirmed"

## Key Features

1. **Dual Primary Keys**: Each object contains both `BookingId` and `CarId`
2. **Comprehensive Information**: Includes all related data from multiple tables
3. **Efficient Joins**: Uses optimized SQL queries with proper joins
4. **Flexible Filtering**: Multiple optional filter parameters
5. **Left Joins**: Ensures all confirmed bookings are returned even if related data is missing

## Use Cases

1. **Fleet Management**: Track which cars are assigned to which bookings
2. **Customer Service**: View complete booking and car information
3. **Operations**: Monitor car utilization and booking status
4. **Reporting**: Generate reports on confirmed bookings with car details
5. **Audit Trail**: Track booking confirmations with associated vehicles

## Performance Notes

- Uses efficient SQL JOINs instead of multiple database queries
- Filters are applied at the database level for optimal performance
- Consider adding database indexes on frequently filtered fields:
  - `booking_confirmation_details.BookingId`
  - `booking_confirmation_details.Car_Id`
  - `booking_details.BookingDate`
  - `booking_details.MemberId`

## Error Handling

- Returns empty array if no confirmed bookings found
- Handles null values gracefully in related data
- Uses left joins to ensure all confirmed bookings are returned
- Gracefully handles missing related data

## Database Relationships

The API leverages these key relationships:
- `booking_confirmation_details.BookingId` → `booking_details.BookingId`
- `booking_confirmation_details.Car_Id` → `car_details.Car_Id`
- `booking_details.MemberId` → `member_details.Member_Id`
- `booking_details.PickupLocation` → `location_details.Location_Id`
- `booking_details.DropLocation` → `location_details.Location_Id`
- `booking_details.VehicleId` → `vehicle_details.VehicleId`
