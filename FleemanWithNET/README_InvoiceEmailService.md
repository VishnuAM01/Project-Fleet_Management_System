# Invoice Email Service Integration

This document describes the complete integration of an invoice email service for the Fleeman Car Rental system, consisting of a .NET API endpoint and a PHP microservice for sending emails.

## Architecture Overview

```
┌─────────────────┐    HTTP Request    ┌─────────────────┐    HTTP Request    ┌─────────────────┐
│   Frontend      │ ──────────────────► │   .NET API      │ ──────────────────► │   PHP Service   │
│   (React/Angular)│                    │   (Invoice      │                    │   (Email        │
│                 │                    │   Controller)   │                    │   Sender)       │
└─────────────────┘                    └─────────────────┘                    └─────────────────┘
                                              │                                       │
                                              ▼                                       ▼
                                       ┌─────────────────┐                    ┌─────────────────┐
                                       │   Database      │                    │   Email Server  │
                                       │   (SQL Server)  │                    │   (SMTP/Local)   │
                                       └─────────────────┘                    └─────────────────┘
```

## Components

### 1. .NET API Components

#### New Files Created:
- `Services/IEmailService.cs` - Interface for email service
- `ServiceImpl/EmailService.cs` - Implementation of email service
- Updated `Controllers/InvoiceController.cs` - Added new endpoint
- Updated `Program.cs` - Added service registration
- Updated `appsettings.json` - Added PHP API configuration

#### New Endpoint:
```
POST /api/Invoice/send-invoice-email/{invoiceId}
```

**Request:**
- Method: POST
- URL: `http://localhost:5231/api/Invoice/send-invoice-email/2`
- Parameters: `invoiceId` (path parameter)

**Response:**
```json
{
  "message": "Invoice email sent successfully",
  "invoiceId": 2
}
```

### 2. PHP Microservice

#### Files Created:
- `php-email-service/index.php` - Main API endpoint
- `php-email-service/README.md` - PHP service documentation
- `php-email-service/test.php` - Test script
- `php-email-service/start-server.bat` - Windows batch file
- `php-email-service/start-server.ps1` - PowerShell script

#### PHP API Endpoint:
```
POST /api/send-invoice-email
```

**Request Body:**
```json
{
  "invoiceId": 123,
  "invoiceDate": "2024-01-15 10:30:00",
  "bookingId": 456,
  "actualReturnDate": "2024-01-15 14:30:00",
  "dropLocation": 1,
  "carId": 789,
  "carRentalAmount": 150.00,
  "addonRentalAmount": 25.00,
  "totalAmount": 175.00,
  "fuelStatus": "Full",
  "carName": "Toyota Camry",
  "memberName": "John Doe",
  "dropLocationName": "Downtown Office"
}
```

**Response:**
```json
{
  "success": true,
  "message": "Invoice email sent successfully",
  "invoiceId": 123
}
```

## Setup Instructions

### Prerequisites

1. **.NET 9.0** - For the main API
2. **PHP 7.4+** - For the email microservice
3. **SQL Server** - Database
4. **Email Server** - SMTP or local mail server

### Step 1: Start the .NET API

```bash
# Navigate to the .NET project directory
cd fleeman_with_dot_net

# Restore packages
dotnet restore

# Build the project
dotnet build

# Run the application
dotnet run
```

The .NET API will be available at: `http://localhost:5231`

### Step 2: Start the PHP Email Service

#### Option A: Using PowerShell (Recommended for Windows)
```powershell
cd php-email-service
.\start-server.ps1
```

#### Option B: Using Batch File
```cmd
cd php-email-service
start-server.bat
```

#### Option C: Manual Command
```bash
cd php-email-service
php -S localhost:8000
```

The PHP service will be available at: `http://localhost:8000`

### Step 3: Test the Integration

#### Test the PHP Service Directly
```bash
# Run the test script
cd php-email-service
php test.php
```

#### Test via cURL
```bash
# Test the .NET endpoint
curl -X POST http://localhost:5231/api/Invoice/send-invoice-email/2

# Test the PHP service directly
curl -X POST http://localhost:8000/api/send-invoice-email \
  -H "Content-Type: application/json" \
  -d '{
    "invoiceId": 123,
    "invoiceDate": "2024-01-15 10:30:00",
    "bookingId": 456,
    "actualReturnDate": "2024-01-15 14:30:00",
    "dropLocation": 1,
    "carId": 789,
    "carRentalAmount": 150.00,
    "addonRentalAmount": 25.00,
    "totalAmount": 175.00,
    "fuelStatus": "Full",
    "carName": "Toyota Camry",
    "memberName": "John Doe",
    "dropLocationName": "Downtown Office"
  }'
```

## Configuration

### .NET Configuration

The PHP API URL is configured in `appsettings.json`:

```json
{
  "PhpApiSettings": {
    "BaseUrl": "http://localhost:8000"
  }
}
```

### PHP Configuration

Edit the email settings in `php-email-service/index.php`:

```php
// Change recipient email
$to = 'customer@example.com';

// Modify email headers
$headers = [
    'From: noreply@fleeman.com',
    'Reply-To: support@fleeman.com',
    'Content-Type: text/html; charset=UTF-8',
    'X-Mailer: PHP/' . phpversion()
];
```

## Email Template

The PHP service generates a professional HTML email with:

- Company branding and styling
- Invoice details in a structured table
- Car rental information
- Pricing breakdown
- Contact information

## Error Handling

### .NET API Errors
- **400 Bad Request**: Invalid invoice ID or missing data
- **404 Not Found**: Invoice not found
- **500 Internal Server Error**: Communication with PHP service failed

### PHP Service Errors
- **400 Bad Request**: Invalid JSON or missing required fields
- **405 Method Not Allowed**: Wrong HTTP method
- **500 Internal Server Error**: Email sending failed

## Logging

### .NET Logging
- Console output for debugging
- Exception details in error responses

### PHP Logging
- Email attempts logged to `email_log.txt`
- Format: `[timestamp] Invoice #123 - Email SUCCESS/FAILED`

## Security Considerations

1. **Input Validation**: Both services validate all input data
2. **CORS**: PHP service allows cross-origin requests (configure for production)
3. **Email Headers**: Proper headers prevent email spoofing
4. **Error Messages**: Sensitive information not exposed in errors

## Production Deployment

### .NET API
1. Deploy to IIS, Azure, or other hosting platform
2. Configure HTTPS
3. Set up proper logging and monitoring
4. Use environment-specific configuration

### PHP Service
1. Deploy to web server (Apache/Nginx)
2. Configure SMTP for reliable email delivery
3. Set up SSL/TLS
4. Implement rate limiting
5. Add API authentication if needed

### Email Service Options
1. **PHPMailer**: More robust email handling
2. **SendGrid**: Cloud email service
3. **Mailgun**: Transactional email service
4. **AWS SES**: Amazon's email service

## Troubleshooting

### Common Issues

1. **PHP not found**
   - Install PHP and add to PATH
   - Verify with `php -v`

2. **Email not sending**
   - Check email server configuration
   - Verify recipient email address
   - Check `email_log.txt` for errors

3. **CORS errors**
   - Ensure PHP service is running
   - Check CORS headers in PHP service

4. **Connection refused**
   - Verify both services are running
   - Check port configurations
   - Ensure firewall allows connections

### Debug Steps

1. Test PHP service directly with `test.php`
2. Check .NET API logs for HTTP client errors
3. Verify email server configuration
4. Test with different email addresses

## API Documentation

### Complete Flow Example

1. **Frontend calls .NET API:**
   ```
   POST http://localhost:5231/api/Invoice/send-invoice-email/2
   ```

2. **.NET API retrieves invoice data and calls PHP service:**
   ```
   POST http://localhost:8000/api/send-invoice-email
   Content-Type: application/json
   
   {
     "invoiceId": 2,
     "invoiceDate": "2024-01-15 10:30:00",
     "bookingId": 456,
     "actualReturnDate": "2024-01-15 14:30:00",
     "dropLocation": 1,
     "carId": 789,
     "carRentalAmount": 150.00,
     "addonRentalAmount": 25.00,
     "totalAmount": 175.00,
     "fuelStatus": "Full",
     "carName": "Toyota Camry",
     "memberName": "John Doe",
     "dropLocationName": "Downtown Office"
   }
   ```

3. **PHP service sends email and responds:**
   ```json
   {
     "success": true,
     "message": "Invoice email sent successfully",
     "invoiceId": 2
   }
   ```

4. **.NET API responds to frontend:**
   ```json
   {
     "message": "Invoice email sent successfully",
     "invoiceId": 2
   }
   ```

This integration provides a clean separation of concerns with the .NET API handling business logic and the PHP service specializing in email delivery.
