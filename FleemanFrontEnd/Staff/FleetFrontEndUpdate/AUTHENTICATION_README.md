# JWT Authentication System for IndiaDrive

This document describes the JWT authentication system implemented for the IndiaDrive car rental application.

## Overview

The authentication system supports two types of users:
- **Members** (Role ID: 1) - Regular customers who can book cars
- **Staff** (Role ID: 2) - Employees who manage the rental operations

## Features

### ✅ Implemented Features

1. **Dual Authentication System**
   - Member login/registration
   - Staff login/registration
   - Role-based access control

2. **JWT Token Management**
   - Secure token storage in localStorage
   - Automatic token verification
   - Token expiration handling

3. **Route Protection**
   - Client-side authentication guards
   - Server-side middleware protection
   - Role-based route restrictions

4. **User Interface**
   - Modern, responsive login/register pages
   - User-specific dashboards
   - Header with user information and logout

5. **API Integration**
   - Authenticated API requests
   - Automatic token inclusion in headers
   - Error handling for authentication failures

## File Structure

```
app/
├── lib/
│   └── auth.js                 # Authentication utilities
├── components/
│   ├── AuthGuard.js           # Route protection component
│   └── Header.js              # Navigation header with user info
├── login/
│   └── page.js                # Login page (member & staff)
├── register/
│   └── page.js                # Registration page (member & staff)
├── dashboard/
│   └── page.js                # Member dashboard
├── staff/
│   └── dashboard/
│       └── page.js            # Staff dashboard
└── invoices/
    └── page.js                # Updated with authentication
middleware.js                  # Server-side route protection
```

## API Endpoints

### Authentication Endpoints

| Endpoint | Method | Description | User Type |
|----------|--------|-------------|-----------|
| `/api/Auth/login` | POST | Member login | Member |
| `/api/Auth/register` | POST | Member registration | Member |
| `/api/Auth/staff-login` | POST | Staff login | Staff |
| `/api/Auth/staff-register` | POST | Staff registration | Staff |
| `/api/Auth/verify` | POST | Token verification | Both |

### Request/Response Examples

#### Staff Login
```javascript
// Request
{
  "email": "staff@fleeman.com",
  "password": "staffpassword123"
}

// Response
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

#### Member Login
```javascript
// Request
{
  "email": "member@example.com",
  "password": "memberpassword123"
}

// Response
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "memberId": 1,
  "email": "member@example.com",
  "roleId": 1,
  "firstName": "John",
  "lastName": "Doe",
  "fullName": "John Doe"
}
```

## Usage

### 1. Login Flow

```javascript
import { staffLogin, memberLogin } from '../lib/auth';

// Staff login
const result = await staffLogin(email, password);
if (result.success) {
  // Redirect to staff dashboard
  window.location.href = '/staff/dashboard';
}

// Member login
const result = await memberLogin(email, password);
if (result.success) {
  // Redirect to member dashboard
  window.location.href = '/dashboard';
}
```

### 2. Route Protection

```javascript
import AuthGuard from '../components/AuthGuard';

// Protect member routes
<AuthGuard requiredUserType="member">
  <MemberDashboard />
</AuthGuard>

// Protect staff routes
<AuthGuard requiredUserType="staff">
  <StaffDashboard />
</AuthGuard>

// Protect any authenticated user
<AuthGuard>
  <ProtectedComponent />
</AuthGuard>
```

### 3. Authenticated API Calls

```javascript
import { authenticatedFetch } from '../lib/auth';

// Make authenticated API calls
const response = await authenticatedFetch('/api/Invoice');
if (response.ok) {
  const data = await response.json();
  // Handle data
}
```

### 4. User Information

```javascript
import { getUserData, getUserType, isAuthenticated } from '../lib/auth';

// Check authentication status
if (isAuthenticated()) {
  const userData = getUserData();
  const userType = getUserType();
  
  console.log('User:', userData);
  console.log('Type:', userType); // 'member' or 'staff'
}
```

## Security Features

### 1. Token Management
- Tokens stored securely in localStorage
- Automatic token verification on protected routes
- Token expiration handling
- Automatic logout on invalid tokens

### 2. Route Protection
- Client-side authentication guards
- Server-side middleware protection
- Role-based access control
- Automatic redirects for unauthorized access

### 3. API Security
- Automatic token inclusion in API requests
- 401 handling with automatic logout
- CORS protection through middleware

## Database Requirements

### Staff Details Table
```sql
CREATE TABLE staff_details (
  Staff_Id INT PRIMARY KEY IDENTITY(1,1),
  StaffFirstName NVARCHAR(50) NOT NULL,
  StaffLastName NVARCHAR(50) NOT NULL,
  Email NVARCHAR(100) UNIQUE NOT NULL,
  MobileNumber NVARCHAR(15),
  Address NVARCHAR(200),
  City NVARCHAR(50),
  State NVARCHAR(50),
  ZipCode NVARCHAR(10),
  DateOfBirth DATE,
  HireDate DATE DEFAULT GETDATE(),
  Department NVARCHAR(50),
  Position NVARCHAR(50),
  Salary DECIMAL(10,2),
  IsActive BIT DEFAULT 1,
  LocationId INT NOT NULL
);
```

### Authentication Table
```sql
CREATE TABLE authentication_table (
  AuthId INT PRIMARY KEY IDENTITY(1,1),
  email NVARCHAR(100) UNIQUE NOT NULL,
  Password NVARCHAR(255) NOT NULL,
  Role_Id INT NOT NULL -- 1 for members, 2 for staff
);
```

## Testing

### 1. Test Staff Login
```bash
curl -X POST http://localhost:5231/api/Auth/staff-login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "staff@fleeman.com",
    "password": "staffpassword123"
  }'
```

### 2. Test Member Login
```bash
curl -X POST http://localhost:5231/api/Auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "member@example.com",
    "password": "memberpassword123"
  }'
```

### 3. Test Token Verification
```bash
curl -X POST http://localhost:5231/api/Auth/verify \
  -H "Authorization: Bearer YOUR_TOKEN_HERE"
```

## Error Handling

### Common Error Responses

| Status | Error | Description |
|--------|-------|-------------|
| 401 | Invalid credentials | Wrong email/password |
| 401 | Token expired | JWT token has expired |
| 400 | Validation error | Invalid input data |
| 500 | Server error | Internal server error |

### Error Handling in Frontend

```javascript
const result = await staffLogin(email, password);
if (!result.success) {
  // Display error message
  setError(result.error);
} else {
  // Handle success
  setSuccess('Login successful!');
}
```

## Configuration

### Environment Variables
```env
# API Base URL
NEXT_PUBLIC_API_BASE_URL=http://localhost:5231/api

# JWT Secret (Backend)
JWT_SECRET=your-secret-key

# Token Expiration (Backend)
JWT_EXPIRATION=1h
```

### API Base URL
The API base URL is configured in `app/lib/auth.js`:
```javascript
const API_BASE_URL = 'http://localhost:5231/api';
```

## Deployment Considerations

### 1. HTTPS
- Always use HTTPS in production
- Secure cookie settings
- HSTS headers

### 2. Token Storage
- Consider using HTTP-only cookies for better security
- Implement token refresh mechanism
- Set appropriate token expiration times

### 3. CORS
- Configure CORS properly for production domains
- Restrict allowed origins
- Handle preflight requests

### 4. Rate Limiting
- Implement rate limiting on authentication endpoints
- Prevent brute force attacks
- Monitor failed login attempts

## Troubleshooting

### Common Issues

1. **Token not found**
   - Check localStorage for token
   - Verify token format
   - Clear localStorage and re-login

2. **401 Unauthorized**
   - Token may be expired
   - Check token validity
   - Re-authenticate user

3. **Route protection not working**
   - Verify AuthGuard component usage
   - Check user type requirements
   - Ensure proper imports

4. **API calls failing**
   - Verify token is included in headers
   - Check API endpoint availability
   - Verify CORS configuration

### Debug Mode

Enable debug logging in `app/lib/auth.js`:
```javascript
const DEBUG = true;

if (DEBUG) {
  console.log('Auth Debug:', { token, userData, userType });
}
```

## Future Enhancements

### Planned Features
1. **Token Refresh**
   - Automatic token refresh before expiration
   - Refresh token rotation
   - Silent authentication

2. **Multi-factor Authentication**
   - SMS verification
   - Email verification
   - TOTP support

3. **Session Management**
   - Multiple device sessions
   - Session revocation
   - Activity monitoring

4. **Advanced Security**
   - IP-based restrictions
   - Device fingerprinting
   - Anomaly detection

## Support

For issues or questions regarding the authentication system:
1. Check the troubleshooting section
2. Review the API documentation
3. Test with the provided examples
4. Contact the development team

---

**Last Updated:** January 2024
**Version:** 1.0.0
