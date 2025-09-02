<?php
header('Content-Type: application/json');
header('Access-Control-Allow-Origin: *');
header('Access-Control-Allow-Methods: POST, GET, OPTIONS');
header('Access-Control-Allow-Headers: Content-Type');

// Handle preflight requests
if ($_SERVER['REQUEST_METHOD'] === 'OPTIONS') {
    http_response_code(200);
    exit();
}

// Only allow POST requests
if ($_SERVER['REQUEST_METHOD'] !== 'POST') {
    http_response_code(405);
    echo json_encode(['success' => false, 'message' => 'Method not allowed']);
    exit();
}

// Get the request path
$requestUri = $_SERVER['REQUEST_URI'];
$path = parse_url($requestUri, PHP_URL_PATH);

// Route the request
if ($path === '/api/send-invoice-email') {
    handleSendInvoiceEmail();
} else {
    http_response_code(404);
    echo json_encode(['success' => false, 'message' => 'Endpoint not found']);
}

function handleSendInvoiceEmail() {
    // Get JSON input
    $input = file_get_contents('php://input');
    $data = json_decode($input, true);
    
    if (!$data) {
        http_response_code(400);
        echo json_encode(['success' => false, 'message' => 'Invalid JSON data']);
        return;
    }
    
    // Validate required fields
    $requiredFields = ['invoiceId', 'invoiceDate', 'bookingId', 'carId', 'carRentalAmount', 'addonRentalAmount', 'totalAmount', 'memberName', 'carName'];
    foreach ($requiredFields as $field) {
        if (!isset($data[$field])) {
            http_response_code(400);
            echo json_encode(['success' => false, 'message' => "Missing required field: $field"]);
            return;
        }
    }
    
    try {
        // Send email
        $emailSent = sendInvoiceEmail($data);
        
        if ($emailSent) {
            echo json_encode([
                'success' => true,
                'message' => 'Invoice email sent successfully',
                'invoiceId' => $data['invoiceId']
            ]);
        } else {
            http_response_code(500);
            echo json_encode([
                'success' => false,
                'message' => 'Failed to send email'
            ]);
        }
    } catch (Exception $e) {
        http_response_code(500);
        echo json_encode([
            'success' => false,
            'message' => 'Error: ' . $e->getMessage()
        ]);
    }
}

function sendInvoiceEmail($invoiceData) {
    // Email configuration
    $to = 'customer@example.com'; // You can make this dynamic based on member email
    $subject = 'Invoice for Car Rental - Invoice #' . $invoiceData['invoiceId'];
    
    // Create email body
    $body = createEmailBody($invoiceData);
    
    // Email headers
    $headers = [
        'From: noreply@fleeman.com',
        'Reply-To: support@fleeman.com',
        'Content-Type: text/html; charset=UTF-8',
        'X-Mailer: PHP/' . phpversion()
    ];
    
    // Send email using PHP's mail function
    // Note: In production, you might want to use a more robust email service like PHPMailer, SendGrid, etc.
    $result = mail($to, $subject, $body, implode("\r\n", $headers));
    
    // Log the email attempt
    logEmailAttempt($invoiceData['invoiceId'], $result);
    
    return $result;
}

function createEmailBody($data) {
    $html = '
    <!DOCTYPE html>
    <html>
    <head>
        <meta charset="UTF-8">
        <title>Car Rental Invoice</title>
        <style>
            body { font-family: Arial, sans-serif; line-height: 1.6; color: #333; }
            .container { max-width: 600px; margin: 0 auto; padding: 20px; }
            .header { background-color: #2c3e50; color: white; padding: 20px; text-align: center; }
            .content { padding: 20px; background-color: #f9f9f9; }
            .invoice-details { background-color: white; padding: 20px; margin: 20px 0; border-radius: 5px; }
            .amount { font-size: 18px; font-weight: bold; color: #e74c3c; }
            .footer { text-align: center; padding: 20px; color: #7f8c8d; font-size: 12px; }
            table { width: 100%; border-collapse: collapse; margin: 20px 0; }
            th, td { padding: 10px; text-align: left; border-bottom: 1px solid #ddd; }
            th { background-color: #ecf0f1; }
        </style>
    </head>
    <body>
        <div class="container">
            <div class="header">
                <h1>Fleeman Car Rental</h1>
                <p>Invoice for Your Car Rental</p>
            </div>
            
            <div class="content">
                <p>Dear ' . htmlspecialchars($data['memberName']) . ',</p>
                
                <p>Thank you for choosing Fleeman Car Rental. Please find your invoice details below:</p>
                
                <div class="invoice-details">
                    <h2>Invoice Details</h2>
                    <table>
                        <tr>
                            <th>Invoice Number:</th>
                            <td>#' . htmlspecialchars($data['invoiceId']) . '</td>
                        </tr>
                        <tr>
                            <th>Invoice Date:</th>
                            <td>' . htmlspecialchars($data['invoiceDate']) . '</td>
                        </tr>
                        <tr>
                            <th>Booking ID:</th>
                            <td>' . htmlspecialchars($data['bookingId']) . '</td>
                        </tr>
                        <tr>
                            <th>Car:</th>
                            <td>' . htmlspecialchars($data['carName']) . '</td>
                        </tr>
                        <tr>
                            <th>Return Date:</th>
                            <td>' . htmlspecialchars($data['actualReturnDate']) . '</td>
                        </tr>
                        <tr>
                            <th>Drop Location:</th>
                            <td>' . htmlspecialchars($data['dropLocationName'] ?? 'N/A') . '</td>
                        </tr>
                        <tr>
                            <th>Fuel Status:</th>
                            <td>' . htmlspecialchars($data['fuelStatus'] ?? 'N/A') . '</td>
                        </tr>
                    </table>
                    
                    <h3>Charges</h3>
                    <table>
                        <tr>
                            <th>Car Rental:</th>
                            <td>$' . number_format($data['carRentalAmount'], 2) . '</td>
                        </tr>
                        <tr>
                            <th>Add-ons:</th>
                            <td>$' . number_format($data['addonRentalAmount'], 2) . '</td>
                        </tr>
                        <tr class="amount">
                            <th>Total Amount:</th>
                            <td>$' . number_format($data['totalAmount'], 2) . '</td>
                        </tr>
                    </table>
                </div>
                
                <p>If you have any questions about this invoice, please contact our customer service team.</p>
                
                <p>Thank you for your business!</p>
                
                <p>Best regards,<br>
                The Fleeman Car Rental Team</p>
            </div>
            
            <div class="footer">
                <p>This is an automated email. Please do not reply to this message.</p>
                <p>&copy; 2024 Fleeman Car Rental. All rights reserved.</p>
            </div>
        </div>
    </body>
    </html>';
    
    return $html;
}

function logEmailAttempt($invoiceId, $success) {
    $logFile = 'email_log.txt';
    $timestamp = date('Y-m-d H:i:s');
    $status = $success ? 'SUCCESS' : 'FAILED';
    $logEntry = "[$timestamp] Invoice #$invoiceId - Email $status\n";
    
    file_put_contents($logFile, $logEntry, FILE_APPEND | LOCK_EX);
}
?>
