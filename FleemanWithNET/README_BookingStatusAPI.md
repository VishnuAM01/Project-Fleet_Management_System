# Booking Status API Documentation

This document describes the booking status functionality for the Fleeman Car Rental system.

## Overview

The Booking Status API provides a comprehensive view of all bookings with their current status. The status is automatically determined based on which tables contain the booking record:

- **Pending**: Booking exists only in `booking_details` table
- **In Progress**: Booking exists in `booking_details` and `booking_confirmation_details` tables
- **Completed**: Booking exists in `booking_details`, `booking_confirmation_details`, and `invoice_header_tables` tables

## API Endpoints

### 1. Get All Booking Statuses

**Endpoint:** `GET /api/BookingStatus/all`

**Description:** Retrieves all bookings with their current status.

**Response (Success - 200):**
```json
[
  {
    "bookingId": 1,
    "memberId": 1,
    "memberName": "John Doe",
    "email": "john@example.com",
    "carId": 1,
    "carName": "Toyota Camry",
    "pickupDate": "2024-01-15T10:00:00",
    "returnDate": "2024-01-17T10:00:00",
    "status": "Completed",
    "totalAmount": 150.00,
    "createdDate": "2024-01-10T09:00:00",
    "confirmationId": 1,
    "invoiceId": 1
  },
  {
    "bookingId": 2,
    "memberId": 2,
    "memberName": "Jane Smith",
    "email": "jane@example.com",
    "carId": 2,
    "carName": "Honda Civic",
    "pickupDate": "2024-01-20T10:00:00",
    "returnDate": "2024-01-22T10:00:00",
    "status": "In Progress",
    "totalAmount": null,
    "createdDate": "2024-01-12T14:30:00",
    "confirmationId": 2,
    "invoiceId": null
  },
  {
    "bookingId": 3,
    "memberId": 3,
    "memberName": "Bob Johnson",
    "email": "bob@example.com",
    "carId": 3,
    "carName": "Ford Focus",
    "pickupDate": "2024-01-25T10:00:00",
    "returnDate": "2024-01-27T10:00:00",
    "status": "Pending",
    "totalAmount": null,
    "createdDate": "2024-01-14T16:45:00",
    "confirmationId": null,
    "invoiceId": null
  }
]
```

### 2. Get Booking Statuses by Member ID

**Endpoint:** `GET /api/BookingStatus/member/{memberId}`

**Description:** Retrieves all booking statuses for a specific member.

**Parameters:**
- `memberId` (int): The ID of the member

**Response (Success - 200):**
```json
[
  {
    "bookingId": 1,
    "memberId": 1,
    "memberName": "John Doe",
    "email": "john@example.com",
    "carId": 1,
    "carName": "Toyota Camry",
    "pickupDate": "2024-01-15T10:00:00",
    "returnDate": "2024-01-17T10:00:00",
    "status": "Completed",
    "totalAmount": 150.00,
    "createdDate": "2024-01-10T09:00:00",
    "confirmationId": 1,
    "invoiceId": 1
  }
]
```

### 3. Get Booking Status by Booking ID

**Endpoint:** `GET /api/BookingStatus/{bookingId}`

**Description:** Retrieves the status of a specific booking.

**Parameters:**
- `bookingId` (int): The ID of the booking

**Response (Success - 200):**
```json
{
  "bookingId": 1,
  "memberId": 1,
  "memberName": "John Doe",
  "email": "john@example.com",
  "carId": 1,
  "carName": "Toyota Camry",
  "pickupDate": "2024-01-15T10:00:00",
  "returnDate": "2024-01-17T10:00:00",
  "status": "Completed",
  "totalAmount": 150.00,
  "createdDate": "2024-01-10T09:00:00",
  "confirmationId": 1,
  "invoiceId": 1
}
```

**Response (Error - 404):**
```json
{
  "message": "Booking not found"
}
```

### 4. Get Bookings by Status

**Endpoint:** `GET /api/BookingStatus/status/{status}`

**Description:** Retrieves all bookings with a specific status.

**Parameters:**
- `status` (string): The status to filter by (Pending, In Progress, Completed)

**Response (Success - 200):**
```json
[
  {
    "bookingId": 3,
    "memberId": 3,
    "memberName": "Bob Johnson",
    "email": "bob@example.com",
    "carId": 3,
    "carName": "Ford Focus",
    "pickupDate": "2024-01-25T10:00:00",
    "returnDate": "2024-01-27T10:00:00",
    "status": "Pending",
    "totalAmount": null,
    "createdDate": "2024-01-14T16:45:00",
    "confirmationId": null,
    "invoiceId": null
  }
]
```

## Status Logic

The system determines booking status based on the following logic:

### Pending Status
- Booking exists only in `booking_details` table
- No confirmation or invoice records exist
- Represents a booking that has been created but not yet confirmed

### In Progress Status
- Booking exists in `booking_details` table
- Booking also exists in `booking_confirmation_details` table
- No invoice record exists yet
- Represents a confirmed booking that is currently active

### Completed Status
- Booking exists in `booking_details` table
- Booking exists in `booking_confirmation_details` table
- Booking also exists in `invoice_header_tables` table
- Represents a completed booking with final invoice

## Data Transfer Object (DTO)

### BookingStatusDTO
```csharp
public class BookingStatusDTO
{
    public int BookingId { get; set; }
    public int MemberId { get; set; }
    public string MemberName { get; set; }
    public string Email { get; set; }
    public int CarId { get; set; }
    public string CarName { get; set; }
    public DateTime PickupDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public string Status { get; set; } // "Pending", "In Progress", "Completed"
    public decimal? TotalAmount { get; set; }
    public DateTime CreatedDate { get; set; }
    public int? ConfirmationId { get; set; }
    public int? InvoiceId { get; set; }
}
```

## Usage Examples

### cURL Examples

```bash
# Get all booking statuses
curl -X GET http://localhost:5231/api/BookingStatus/all

# Get booking statuses for member ID 1
curl -X GET http://localhost:5231/api/BookingStatus/member/1

# Get booking status for booking ID 1
curl -X GET http://localhost:5231/api/BookingStatus/1

# Get all pending bookings
curl -X GET http://localhost:5231/api/BookingStatus/status/Pending

# Get all completed bookings
curl -X GET http://localhost:5231/api/BookingStatus/status/Completed
```

### JavaScript Examples

```javascript
// Get all booking statuses
const getAllBookingStatuses = async () => {
  try {
    const response = await fetch('http://localhost:5231/api/BookingStatus/all');
    if (response.ok) {
      const data = await response.json();
      console.log('All booking statuses:', data);
      return data;
    }
  } catch (error) {
    console.error('Error:', error);
  }
};

// Get booking statuses for a member
const getMemberBookingStatuses = async (memberId) => {
  try {
    const response = await fetch(`http://localhost:5231/api/BookingStatus/member/${memberId}`);
    if (response.ok) {
      const data = await response.json();
      console.log('Member booking statuses:', data);
      return data;
    }
  } catch (error) {
    console.error('Error:', error);
  }
};

// Get bookings by status
const getBookingsByStatus = async (status) => {
  try {
    const response = await fetch(`http://localhost:5231/api/BookingStatus/status/${status}`);
    if (response.ok) {
      const data = await response.json();
      console.log(`${status} bookings:`, data);
      return data;
    }
  } catch (error) {
    console.error('Error:', error);
  }
};
```

### React Example

```jsx
import React, { useState, useEffect } from 'react';

const BookingStatusDashboard = () => {
  const [bookings, setBookings] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetchBookingStatuses();
  }, []);

  const fetchBookingStatuses = async () => {
    try {
      const response = await fetch('/api/BookingStatus/all');
      if (response.ok) {
        const data = await response.json();
        setBookings(data);
      }
    } catch (error) {
      console.error('Error fetching bookings:', error);
    } finally {
      setLoading(false);
    }
  };

  const getStatusColor = (status) => {
    switch (status) {
      case 'Pending': return 'text-yellow-600 bg-yellow-100';
      case 'In Progress': return 'text-blue-600 bg-blue-100';
      case 'Completed': return 'text-green-600 bg-green-100';
      default: return 'text-gray-600 bg-gray-100';
    }
  };

  if (loading) return <div>Loading...</div>;

  return (
    <div className="container mx-auto p-4">
      <h1 className="text-2xl font-bold mb-4">Booking Status Dashboard</h1>
      
      <div className="grid gap-4">
        {bookings.map((booking) => (
          <div key={booking.bookingId} className="border rounded-lg p-4 shadow">
            <div className="flex justify-between items-start">
              <div>
                <h3 className="font-semibold">{booking.carName}</h3>
                <p className="text-gray-600">{booking.memberName}</p>
                <p className="text-sm text-gray-500">
                  {new Date(booking.pickupDate).toLocaleDateString()} - {new Date(booking.returnDate).toLocaleDateString()}
                </p>
              </div>
              <span className={`px-3 py-1 rounded-full text-sm font-medium ${getStatusColor(booking.status)}`}>
                {booking.status}
              </span>
            </div>
            
            {booking.totalAmount && (
              <div className="mt-2">
                <span className="font-semibold">Total: ${booking.totalAmount}</span>
              </div>
            )}
          </div>
        ))}
      </div>
    </div>
  );
};

export default BookingStatusDashboard;
```

## Error Handling

### Common Error Responses

**404 Not Found:**
```json
{
  "message": "Booking not found"
}
```

**500 Internal Server Error:**
```json
{
  "message": "Error retrieving booking statuses",
  "error": "Database connection error"
}
```

## Database Requirements

The API requires the following tables with proper relationships:

### booking_details
- `BookingId` (Primary Key)
- `MemberId` (Foreign Key to member_details)
- `VehicleId` (Foreign Key to car_details via VehicleId)
- `PickupDate`
- `ReturnDate`
- `BookingDate`

### booking_confirmation_details
- `BookingConfirmationDetailsId` (Primary Key)
- `BookingId` (Foreign Key to booking_details)

### invoice_header_table
- `InvoiceId` (Primary Key)
- `BookingId` (Foreign Key to booking_details)
- `CarRentalAmount`
- `AddonRentalAmount`

### member_details
- `Member_Id` (Primary Key)
- `MemberFirstName`
- `MemberLastName`
- `Email`

### car_details
- `Car_Id` (Primary Key)
- `CarName`
- `VehicleId` (Links to booking_details.VehicleId)

## Notes

1. **Status Priority**: The system checks for invoice records first (highest priority), then confirmation records, then falls back to pending status.

2. **Performance**: For large datasets, consider implementing pagination or filtering options.

3. **Real-time Updates**: This API provides a snapshot of current status. For real-time updates, consider implementing WebSocket connections or polling mechanisms.

4. **Caching**: Consider implementing caching for frequently accessed booking statuses to improve performance.

5. **Authorization**: Implement proper authorization to ensure users can only access booking statuses they are authorized to view.
