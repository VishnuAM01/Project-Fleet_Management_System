# Login Response Enhancement

## Overview
The login endpoint has been enhanced to return both the JWT token and member ID to the frontend, along with additional user information.

## Changes Made

### 1. New DTO: LoginResponseDTO
Created `DTO/LoginResponseDTO.cs` with the following structure:
```csharp
public class LoginResponseDTO
{
    public string Token { get; set; }
    public int MemberId { get; set; }
    public string Email { get; set; }
    public int RoleId { get; set; }
}
```

### 2. Enhanced User Service
Added new method `GetUserDetailsForLogin(string email)` to `IUser` interface and `UserService` implementation that:
- Retrieves member details from the database
- Returns a `LoginResponseDTO` with member ID, email, and role ID
- Handles error cases when user is not found

### 3. Updated Auth Controller
Modified `AuthController.Login` method to:
- Use the new service method to get user details
- Include member ID in JWT token claims
- Return the complete `LoginResponseDTO` instead of just the token

### 4. JWT Token Claims
The JWT token now includes:
- `ClaimTypes.Email`: User's email address
- `ClaimTypes.Role`: User's role ID
- `"MemberId"`: User's member ID (custom claim)

## API Response Format

### Before (Old Response)
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
}
```

### After (New Response)
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "memberId": 123,
  "email": "user@example.com",
  "roleId": 1
}
```

## Token Verification Endpoint

### Endpoint: `POST /api/auth/verify`

This endpoint validates JWT tokens and returns user information. It's useful for:
- Frontend token validation
- Extracting user details from tokens
- Checking token expiration
- Refreshing user context

#### Request Headers
```
Authorization: Bearer {your_jwt_token}
```

#### Response Format
```json
{
  "isValid": true,
  "memberId": 123,
  "email": "user@example.com",
  "roleId": 1,
  "expiresAt": "2024-01-15T10:30:00Z"
}
```

#### Error Responses
- **400 Bad Request**: Invalid authorization header format
- **401 Unauthorized**: Token expired, invalid signature, or missing claims
- **400 Bad Request**: Token validation failed with specific error message

#### Example Frontend Usage
```javascript
// Verify token and get user info
const verifyToken = async (token) => {
  try {
    const response = await fetch('/api/auth/verify', {
      method: 'POST',
      headers: {
        'Authorization': `Bearer ${token}`,
        'Content-Type': 'application/json'
      }
    });

    if (response.ok) {
      const userData = await response.json();
      // Token is valid, update user context
      localStorage.setItem('memberId', userData.memberId);
      localStorage.setItem('userEmail', userData.email);
      localStorage.setItem('userRole', userData.roleId);
      return userData;
    } else {
      // Token is invalid, redirect to login
      localStorage.removeItem('token');
      localStorage.removeItem('memberId');
      window.location.href = '/login';
    }
  } catch (error) {
    console.error('Token verification failed:', error);
    return null;
  }
};

// Use this function to check token validity on app startup
const checkAuthStatus = async () => {
  const token = localStorage.getItem('token');
  if (token) {
    const userData = await verifyToken(token);
    if (userData) {
      // User is authenticated, proceed with app
      console.log('User authenticated:', userData);
    }
  } else {
    // No token, redirect to login
    window.location.href = '/login';
  }
};
```

## Frontend Integration

### Using the Member ID
The frontend can now:
1. Store the member ID locally for user identification
2. Use it in subsequent API calls that require member context
3. Display user-specific information without additional API calls

### Example Frontend Usage
```javascript
// Login response handling
const response = await fetch('/api/auth/login', {
  method: 'POST',
  headers: { 'Content-Type': 'application/json' },
  body: JSON.stringify({ email, password })
});

const loginData = await response.json();

// Store token and member ID
localStorage.setItem('token', loginData.token);
localStorage.setItem('memberId', loginData.memberId);
localStorage.setItem('userEmail', loginData.email);
localStorage.setItem('userRole', loginData.roleId);

// Use member ID in subsequent requests
const userProfile = await fetch(`/api/member/${loginData.memberId}`, {
  headers: { 'Authorization': `Bearer ${loginData.token}` }
});
```

## Testing

### HTTP Test File
Updated `fleeman_with_dot_net.http` with a test endpoint:
```http
POST {{fleeman_with_dot_net_HostAddress}}/api/auth/login
Content-Type: application/json

{
  "email": "test@example.com",
  "password": "yourpassword"
}
```

### Manual Testing
1. Start the application
2. Use the HTTP test file or Postman to test the login endpoint
3. Verify that the response includes all required fields
4. Decode the JWT token to verify member ID is included in claims

## Security Considerations

- The member ID is included in the JWT token claims for easy access
- The token maintains the same expiration time (1 hour)
- All existing security measures remain in place
- The member ID claim can be used for authorization checks on the backend

## Database Requirements

The implementation assumes:
- `member_details` table with `Member_Id` as primary key
- `authentication_table` with `email` field for user lookup
- Proper relationship between authentication and member details through email

## Error Handling

The service includes proper error handling:
- Returns appropriate error messages when user is not found
- Maintains existing validation logic
- Provides clear feedback for debugging

## Complete Authentication Flow

### 1. User Login
```javascript
// Step 1: User submits credentials
const loginResponse = await fetch('/api/auth/login', {
  method: 'POST',
  headers: { 'Content-Type': 'application/json' },
  body: JSON.stringify({ email, password })
});

const loginData = await loginResponse.json();

// Store authentication data
localStorage.setItem('token', loginData.token);
localStorage.setItem('memberId', loginData.memberId);
localStorage.setItem('userEmail', loginData.email);
localStorage.setItem('userRole', loginData.roleId);
```

### 2. Token Verification (Optional)
```javascript
// Step 2: Verify token validity (can be done on app startup)
const verifyResponse = await fetch('/api/auth/verify', {
  method: 'POST',
  headers: {
    'Authorization': `Bearer ${loginData.token}`,
    'Content-Type': 'application/json'
  }
});

const verifyData = await verifyResponse.json();
// verifyData contains: isValid, memberId, email, roleId, expiresAt
```

### 3. Using Member ID in API Calls
```javascript
// Step 3: Use member ID in subsequent requests
const userProfile = await fetch(`/api/member/${loginData.memberId}`, {
  headers: {
    'Authorization': `Bearer ${loginData.token}`,
    'Content-Type': 'application/json'
  }
});

// Or use member ID from localStorage
const memberId = localStorage.getItem('memberId');
const bookings = await fetch(`/api/bookings?memberId=${memberId}`, {
  headers: {
    'Authorization': `Bearer ${localStorage.getItem('token')}`,
    'Content-Type': 'application/json'
  }
});
```

### 4. Token Refresh/Validation
```javascript
// Step 4: Periodically validate token or check on app resume
const checkTokenValidity = async () => {
  const token = localStorage.getItem('token');
  if (!token) return false;

  try {
    const response = await fetch('/api/auth/verify', {
      method: 'POST',
      headers: { 'Authorization': `Bearer ${token}` }
    });
    
    if (response.ok) {
      const userData = await response.json();
      // Update user context with fresh data
      localStorage.setItem('memberId', userData.memberId);
      return true;
    } else {
      // Token invalid, clear storage and redirect to login
      localStorage.clear();
      window.location.href = '/login';
      return false;
    }
  } catch (error) {
    console.error('Token validation failed:', error);
    return false;
  }
};

// Check token validity every 5 minutes
setInterval(checkTokenValidity, 5 * 60 * 1000);

// Check on app resume/focus
document.addEventListener('visibilitychange', () => {
  if (!document.hidden) {
    checkTokenValidity();
  }
});
```
