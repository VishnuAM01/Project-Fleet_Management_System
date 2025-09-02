# Staff Login API Documentation

This document describes the staff login functionality for the Fleeman Car Rental system.

## Overview

Staff login is designed to be simple and secure, requiring only:
- Email
- Password

The system automatically returns the staff's location ID along with the authentication token.

Staff users have Role ID 2 and can access staff-specific features.

## API Endpoints

### 1. Staff Login

**Endpoint:** `POST /api/Auth/staff-login`

**Description:** Authenticates staff members and returns a JWT token with location ID.

### 2. Staff Registration

**Endpoint:** `POST /api/Auth/staff-register`

**Description:** Registers a new staff member.

**Request Body:**
```json
{
  "email": "staff@fleeman.com",
  "password": "staffpassword123",
  "locationId": 1,
  "staffFirstName": "John",
  "staffLastName": "Doe",
  "mobileNumber": 1234567890,
  "address": "123 Main St",
  "city": "New York",
  "state": "NY",
  "zipCode": 10001,
  "dateOfBirth": "1990-01-01T00:00:00",
  "department": "Customer Service",
  "position": "Manager",
  "salary": 50000.00
}
```

**Response (Success - 200):**
```json
{
  "message": "Staff registered successfully."
}
```

**Response (Error - 400):**
```json
{
  "message": "Staff already exists with this email."
}
```

**Request Body:**
```json
{
  "email": "staff@fleeman.com",
  "password": "staffpassword123"
}
```

**Response (Success - 200):**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "staffId": 1,
  "email": "staff@fleeman.com",
  "roleId": 2,
  "locationId": 1,
  "staffFirstName": "John",
  "staffLastName": "Doe",
  "fullName": "John Doe"
}
```

**Response (Error - 401):**
```json
{
  "message": "Invalid staff credentials."
}
```

### 3. Token Verification (Updated)

**Endpoint:** `POST /api/Auth/verify`

**Description:** Verifies JWT tokens for both members and staff.

**Headers:**
```
Authorization: Bearer {token}
```

**Response for Staff Token:**
```json
{
  "isValid": true,
  "staffId": 1,
  "email": "staff@fleeman.com",
  "roleId": 2,
  "locationId": 1,
  "fullName": "John Doe",
  "userType": "Staff",
  "expiresAt": "2024-01-15T11:30:00.000Z"
}
```

**Response for Member Token:**
```json
{
  "isValid": true,
  "memberId": 1,
  "email": "member@example.com",
  "roleId": 1,
  "userType": "Member",
  "expiresAt": "2024-01-15T11:30:00.000Z"
}
```

## JWT Token Claims

### Staff Token Claims
- `email`: Staff email address
- `role`: Role ID (2 for staff)
- `StaffId`: Staff ID
- `LocationId`: Location ID

### Member Token Claims
- `email`: Member email address
- `role`: Role ID (1 for members)
- `MemberId`: Member ID

## Database Requirements

### Staff Details Table
The system expects the following fields in the `staff_details` table:
- `Staff_Id` (Primary Key)
- `StaffFirstName`
- `StaffLastName`
- `Email`
- `MobileNumber`
- `Address`
- `City`
- `State`
- `ZipCode`
- `DateOfBirth`
- `HireDate`
- `Department`
- `Position`
- `Salary`
- `IsActive`
- `LocationId` (Required - links staff to a specific location)

### Authentication Table
The system expects the following fields in the `authentication_table`:
- `AuthId` (Primary Key)
- `email`
- `Password` (hashed)
- `Role_Id` (2 for staff)

## Usage Examples

### cURL Example

```bash
# Staff Login
curl -X POST http://localhost:5231/api/Auth/staff-login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "staff@fleeman.com",
    "password": "staffpassword123"
  }'

# Staff Registration
curl -X POST http://localhost:5231/api/Auth/staff-register \
  -H "Content-Type: application/json" \
  -d '{
    "email": "staff@fleeman.com",
    "password": "staffpassword123",
    "locationId": 1,
    "staffFirstName": "John",
    "staffLastName": "Doe",
    "mobileNumber": 1234567890,
    "address": "123 Main St",
    "city": "New York",
    "state": "NY",
    "zipCode": 10001,
    "dateOfBirth": "1990-01-01T00:00:00",
    "department": "Customer Service",
    "position": "Manager",
    "salary": 50000.00
  }'

# Token Verification
curl -X POST http://localhost:5231/api/Auth/verify \
  -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
```

### JavaScript Example

```javascript
// Staff Login
const staffLogin = async () => {
  try {
    const response = await fetch('http://localhost:5231/api/Auth/staff-login', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        email: 'staff@fleeman.com',
        password: 'staffpassword123'
      })
    });

    if (response.ok) {
      const data = await response.json();
      console.log('Staff logged in:', data);
      localStorage.setItem('staffToken', data.token);
    } else {
      console.error('Login failed');
    }
  } catch (error) {
    console.error('Error:', error);
  }
};

// Staff Registration
const registerStaff = async (staffData) => {
  try {
    const response = await fetch('http://localhost:5231/api/Auth/staff-register', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(staffData)
    });

    if (response.ok) {
      const data = await response.json();
      console.log('Staff registered:', data);
      return data;
    } else {
      console.error('Registration failed');
    }
  } catch (error) {
    console.error('Error:', error);
  }
};

// Token Verification
const verifyToken = async (token) => {
  try {
    const response = await fetch('http://localhost:5231/api/Auth/verify', {
      method: 'POST',
      headers: {
        'Authorization': `Bearer ${token}`
      }
    });

    if (response.ok) {
      const data = await response.json();
      console.log('Token verified:', data);
      return data;
    } else {
      console.error('Token verification failed');
      return null;
    }
  } catch (error) {
    console.error('Error:', error);
    return null;
  }
};
```

## Security Features

1. **Password Hashing**: All passwords are hashed using ASP.NET Core Identity password hasher
2. **JWT Tokens**: Secure token-based authentication
3. **Role-Based Access**: Different roles for staff (2) and members (1)
4. **Token Expiration**: Tokens expire after 1 hour
5. **Input Validation**: All inputs are validated

## Error Handling

### Common Error Responses

**401 Unauthorized:**
```json
{
  "message": "Invalid staff credentials."
}
```

**400 Bad Request:**
```json
{
  "message": "Token missing required claims."
}
```

**401 Unauthorized (Token Expired):**
```json
{
  "message": "Token has expired."
}
```

## Testing

### Test Staff Login

1. **Ensure staff exists in database:**
   ```sql
   -- Check if staff exists
   SELECT * FROM staff_details WHERE Email = 'staff@fleeman.com';
   
   -- Check if authentication record exists
   SELECT * FROM authentication_table WHERE email = 'staff@fleeman.com' AND Role_Id = 2;
   ```

2. **Test login endpoint:**
   ```bash
   curl -X POST http://localhost:5231/api/Auth/staff-login \
     -H "Content-Type: application/json" \
     -d '{
       "email": "staff@fleeman.com",
       "password": "staffpassword123"
     }'
   ```

3. **Test staff registration:**
   ```bash
   curl -X POST http://localhost:5231/api/Auth/staff-register \
     -H "Content-Type: application/json" \
     -d '{
       "email": "newstaff@fleeman.com",
       "password": "newpassword123",
       "locationId": 1,
       "staffFirstName": "Jane",
       "staffLastName": "Smith",
       "mobileNumber": 9876543210,
       "address": "456 Oak St",
       "city": "Los Angeles",
       "state": "CA",
       "zipCode": 90210,
       "dateOfBirth": "1985-05-15T00:00:00",
       "department": "Sales",
       "position": "Associate",
       "salary": 45000.00
     }'
   ```

4. **Test token verification:**
   ```bash
   curl -X POST http://localhost:5231/api/Auth/verify \
     -H "Authorization: Bearer YOUR_TOKEN_HERE"
   ```

## Frontend Integration

### React Example

```jsx
import React, { useState } from 'react';

const StaffLogin = () => {
  const [formData, setFormData] = useState({
    email: '',
    password: ''
  });

  const handleSubmit = async (e) => {
    e.preventDefault();
    
    try {
      const response = await fetch('/api/Auth/staff-login', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(formData)
      });

      if (response.ok) {
        const data = await response.json();
        localStorage.setItem('staffToken', data.token);
        localStorage.setItem('staffData', JSON.stringify(data));
        // Redirect to staff dashboard
        window.location.href = '/staff-dashboard';
      } else {
        const error = await response.json();
        alert(error.message);
      }
    } catch (error) {
      console.error('Login error:', error);
      alert('Login failed. Please try again.');
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <div>
        <label>Email:</label>
        <input
          type="email"
          value={formData.email}
          onChange={(e) => setFormData({...formData, email: e.target.value})}
          required
        />
      </div>
      <div>
        <label>Password:</label>
        <input
          type="password"
          value={formData.password}
          onChange={(e) => setFormData({...formData, password: e.target.value})}
          required
        />
      </div>
      <button type="submit">Staff Login</button>
    </form>
  );
};

export default StaffLogin;
```

## Notes

1. **Location ID**: The system now automatically retrieves the staff's location ID from the database during login. The `LocationId` field is required in the `staff_details` table.

2. **Role ID**: Staff users must have `Role_Id = 2` in the authentication table.

3. **Password Requirements**: Ensure staff passwords meet your security requirements.

4. **Token Storage**: Store tokens securely in your frontend application (e.g., in memory for SPA, or secure HTTP-only cookies).

5. **Logout**: Implement proper logout functionality to clear stored tokens.

6. **Database Migration**: You may need to add the `LocationId` column to your `staff_details` table if it doesn't exist.
