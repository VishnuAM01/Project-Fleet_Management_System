# PHP Email Microservice

This is a simple PHP microservice that handles sending invoice emails for the Fleeman Car Rental system.

## Features

- RESTful API endpoint for sending invoice emails
- HTML email templates with professional styling
- Email logging for debugging
- CORS support for cross-origin requests
- Input validation and error handling

## Setup Instructions

### Prerequisites

1. PHP 7.4 or higher
2. Web server (Apache, Nginx, or PHP built-in server)
3. Email server configured (SMTP or local mail server)

### Installation

1. **Using PHP Built-in Server (Development)**
   ```bash
   cd php-email-service
   php -S localhost:8000
   ```

2. **Using Apache**
   - Copy the files to your Apache web root directory
   - Ensure mod_rewrite is enabled
   - Access via: `http://your-domain/api/send-invoice-email`

3. **Using Nginx**
   - Copy the files to your Nginx web root directory
   - Configure Nginx to handle PHP files
   - Access via: `http://your-domain/api/send-invoice-email`

### Configuration

#### Email Settings

Edit the `sendInvoiceEmail()` function in `index.php` to configure:

- **Recipient Email**: Change `$to = 'customer@example.com'` to your desired email
- **From Email**: Modify the `From` header in the `$headers` array
- **Reply-To**: Update the `Reply-To` header

#### SMTP Configuration (Optional)

For production use, consider using PHPMailer or similar library for better email handling:

```php
// Example with PHPMailer
require 'vendor/autoload.php';
use PHPMailer\PHPMailer\PHPMailer;
use PHPMailer\PHPMailer\SMTP;

$mail = new PHPMailer(true);
$mail->isSMTP();
$mail->Host = 'smtp.gmail.com';
$mail->SMTPAuth = true;
$mail->Username = 'your-email@gmail.com';
$mail->Password = 'your-app-password';
$mail->SMTPSecure = PHPMailer::ENCRYPTION_STARTTLS;
$mail->Port = 587;
```

## API Usage

### Endpoint

`POST /api/send-invoice-email`

### Request Body

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

### Response

**Success (200):**
```json
{
  "success": true,
  "message": "Invoice email sent successfully",
  "invoiceId": 123
}
```

**Error (400/500):**
```json
{
  "success": false,
  "message": "Error description"
}
```

## Testing

### Using cURL

```bash
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

### Using Postman

1. Set method to `POST`
2. URL: `http://localhost:8000/api/send-invoice-email`
3. Headers: `Content-Type: application/json`
4. Body: Raw JSON with the invoice data

## Logging

Email attempts are logged to `email_log.txt` in the following format:

```
[2024-01-15 10:30:00] Invoice #123 - Email SUCCESS
[2024-01-15 10:35:00] Invoice #124 - Email FAILED
```

## Security Considerations

1. **Input Validation**: All input is validated and sanitized
2. **CORS**: Configured to allow cross-origin requests (adjust for production)
3. **Email Headers**: Proper headers are set to prevent email spoofing
4. **Error Handling**: Sensitive information is not exposed in error messages

## Production Deployment

1. **Use HTTPS**: Always use HTTPS in production
2. **Email Service**: Use a reliable email service (SendGrid, Mailgun, etc.)
3. **Logging**: Implement proper logging and monitoring
4. **Rate Limiting**: Add rate limiting to prevent abuse
5. **Authentication**: Consider adding API authentication for production use

## Troubleshooting

### Email Not Sending

1. Check if your server has email capabilities configured
2. Verify SMTP settings if using external email service
3. Check the `email_log.txt` file for error details
4. Ensure the recipient email address is valid

### API Not Responding

1. Verify the PHP server is running
2. Check the URL and endpoint path
3. Ensure CORS headers are properly set
4. Check server error logs

### JSON Parsing Errors

1. Verify the request body is valid JSON
2. Check that all required fields are present
3. Ensure proper Content-Type header is set
