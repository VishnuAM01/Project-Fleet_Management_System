# Invoice Creation API

This API provides endpoints to create invoices when cars are returned, automatically calculating rental amounts using the RateCalculator utility.

## Main Endpoint

### Create Invoice (Car Return)
**POST** `/api/Invoice/create`

Creates an invoice when a car is returned, calculating rental amounts and updating car availability.

**Request Body:**
```json
{
  "carId": 5,
  "fuelStatus": "Full",
  "bookingId": 10,
  "dropLocation": 3
}
```

**Response:**
```json
{
  "invoiceId": 1,
  "invoiceDate": "2024-01-15T14:30:00",
  "bookingId": 10,
  "actualReturnDate": "2024-01-15T14:30:00",
  "dropLocation": 3,
  "carId": 5,
  "carRentalAmount": 150.00,
  "addonRentalAmount": 25.00,
  "totalAmount": 175.00,
  "fuelStatus": "Full",
  "carName": "Toyota Camry",
  "memberName": "John Doe",
  "dropLocationName": "Airport Hub"
}
```

## Additional Endpoints

### Get Invoice by ID
**GET** `/api/Invoice/{invoiceId}`

### Get All Invoices
**GET** `/api/Invoice`

### Get Invoices by Booking ID
**GET** `/api/Invoice/booking/{bookingId}`

## Business Logic

### Invoice Creation Process
1. **Validation**: Checks if the booking exists and is confirmed
2. **Car Update**: Updates car fuel status and marks car as available again
3. **Rate Calculation**: Uses RateCalculator utility for car rental amounts
4. **Addon Calculation**: Calculates addon costs based on duration
5. **Invoice Creation**: Saves invoice with current timestamp
6. **Cleanup**: Deletes the booking confirmation entry since the booking is now complete

### Rate Calculation
- **Car Rental**: Uses `RateCalculator.CalculateRate()` with:
  - Start date: `PickupDate` from booking
  - End date: Current date/time
  - Rates: Daily, Weekly, Monthly from vehicle details
- **Addon Rental**: Simple multiplication of addon price × days

### Automatic Fields
- **Invoice Date**: Current date and time
- **Actual Return Date**: Current date and time
- **Car Rental Amount**: Calculated using RateCalculator
- **Addon Rental Amount**: Calculated based on duration

### Cleanup Logic
After creating an invoice, the system automatically:
- **Deletes the booking confirmation entry** from `booking_confirmation_details`
- **Reason**: The booking is now complete and invoiced, so it no longer needs to be tracked as "confirmed"
- **Benefits**: 
  - Prevents duplicate processing of completed bookings
  - Maintains clean data by removing completed transactions
  - Ensures the car can be reassigned to new bookings
  - Keeps the booking confirmation table focused on active/ongoing bookings

## Data Model

### InvoiceCreationRequestDTO
- `carId` (required): ID of the returned car
- `fuelStatus` (required): Fuel level when returned
- `bookingId` (required): ID of the confirmed booking
- `dropLocation` (required): Location where car was returned

### InvoiceResponseDTO
- **Invoice Details**: ID, dates, amounts
- **Car Information**: ID, name, fuel status
- **Member Information**: Customer name
- **Location Information**: Drop location name
- **Calculated Amounts**: Car rental, addon rental, total

## Use Cases

1. **Car Return Processing**: Generate invoices when customers return vehicles
2. **Fleet Management**: Track car availability and fuel status
3. **Financial Reporting**: Calculate rental revenue and costs
4. **Customer Billing**: Provide detailed invoices for services
5. **Audit Trail**: Record actual return times and locations

## Rate Calculation Examples

### Car Rental (using RateCalculator)
```csharp
// Example: 5 days rental
var amount = RateCalculator.CalculateRate(
    pickupDate: "2024-01-10",
    dropoffDate: "2024-01-15", 
    dailyRate: 30.00,
    weeklyRate: 180.00,
    monthlyRate: 720.00
);
// Result: 150.00 (5 days × 30.00)
```

### Addon Rental
```csharp
// Example: GPS for 5 days at $5/day
var addonAmount = 5.00 * 5; // $25.00
```

## Error Handling

- **Validation Errors**: Returns 400 Bad Request with error message
- **Not Found**: Returns 404 for invalid booking/car combinations
- **Database Errors**: Graceful error handling with meaningful messages

## Database Updates

When an invoice is created:
1. **Invoice Header**: New record in `invoice_header_table`
2. **Car Details**: Updates `FuelStatus` and `IsAvailable` fields
3. **Audit Trail**: Records actual return date and location
4. **Cleanup**: Removes completed booking from `booking_confirmation_details`

## Performance Notes

- Uses efficient Entity Framework queries with includes
- Single database transaction for all updates
- Optimized joins for related data retrieval
- Consider adding indexes on frequently queried fields

## Security Considerations

- Validates booking confirmation before invoice creation
- Ensures car belongs to the specified booking
- Updates car availability to prevent double-booking
- Maintains data integrity across related tables
  - Prevents duplicate processing by removing completed bookings