# Unconfirmed Bookings API

This API provides endpoints to retrieve bookings that are not present in the booking confirmation details table.

## Endpoints

### 1. Get All Unconfirmed Bookings
**GET** `/api/bookings/unconfirmed`

Returns all bookings that exist in the `booking_details` table but are not present in the `booking_confirmation_details` table.

**Response:**
```json
[
  {
    "bookingId": 1,
    "bookingDate": "2024-01-15T10:00:00",
    "pickupDate": "2024-01-20T09:00:00",
    "returnDate": "2024-01-25T17:00:00",
    "pickupLocation": 1,
    "dropLocation": 2,
    "vehicleId": 5,
    "memberId": 10,
    "addonBookId": null,
    "memberFirstName": "John",
    "memberLastName": "Doe",
    "email": "john.doe@example.com",
    "mobileNumber": "1234567890",
    "pickupLocationName": "Downtown Hub",
    "dropLocationName": "Airport Hub",
    "vehicleName": "Toyota Camry",
    "vehicleModel": "2024",
    "status": "Unconfirmed"
  }
]
```

### 2. Get Filtered Unconfirmed Bookings
**GET** `/api/bookings/unconfirmed/filtered?fromDate={date}&toDate={date}&memberId={id}`

Returns filtered unconfirmed bookings based on optional parameters.

**Query Parameters:**
- `fromDate` (optional): Filter bookings from this date onwards
- `toDate` (optional): Filter bookings up to this date
- `memberId` (optional): Filter bookings for a specific member

**Examples:**
```
GET /api/bookings/unconfirmed/filtered?fromDate=2024-01-01&toDate=2024-01-31
GET /api/bookings/unconfirmed/filtered?memberId=10
GET /api/bookings/unconfirmed/filtered?fromDate=2024-01-01&memberId=10
```

## Business Logic

The API identifies unconfirmed bookings by:
1. Getting all `BookingId` values from the `booking_confirmation_details` table
2. Finding all bookings in the `booking_details` table that are NOT in the confirmed list
3. Joining with related tables to provide comprehensive information:
   - `member_details` for customer information
   - `location_details` for pickup and drop location names
   - `vehicle_details` for vehicle information

## Data Model

### UnconfirmedBookingDTO
- **Basic Booking Info**: ID, dates, locations, vehicle, member
- **Member Details**: First name, last name, email, mobile
- **Location Details**: Pickup and drop location names
- **Vehicle Details**: Vehicle name and model
- **Status**: Always set to "Unconfirmed"

## Use Cases

1. **Customer Service**: Identify pending bookings that need follow-up
2. **Operations**: Track unconfirmed reservations for resource planning
3. **Reporting**: Generate reports on booking conversion rates
4. **Notifications**: Send reminders for unconfirmed bookings

## Performance Notes

- Uses efficient SQL JOINs instead of multiple database queries
- Filters are applied at the database level for optimal performance
- Consider adding database indexes on frequently filtered fields if needed

## Error Handling

- Returns empty array if no unconfirmed bookings found
- Handles null values gracefully in related data
- Uses left joins to ensure all unconfirmed bookings are returned even if related data is missing
