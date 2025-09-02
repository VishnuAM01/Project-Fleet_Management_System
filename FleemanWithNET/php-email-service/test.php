<?php
// Test script for the email service
echo "Testing PHP Email Service...\n";

// Test data
$testData = [
    'invoiceId' => 999,
    'invoiceDate' => date('Y-m-d H:i:s'),
    'bookingId' => 888,
    'actualReturnDate' => date('Y-m-d H:i:s'),
    'dropLocation' => 1,
    'carId' => 777,
    'carRentalAmount' => 150.00,
    'addonRentalAmount' => 25.00,
    'totalAmount' => 175.00,
    'fuelStatus' => 'Full',
    'carName' => 'Toyota Camry',
    'memberName' => 'John Doe',
    'dropLocationName' => 'Downtown Office'
];

// Test the email function directly
echo "Testing email function...\n";

// Include the main file to access functions
require_once 'index.php';

// Test the email sending function
try {
    $result = sendInvoiceEmail($testData);
    if ($result) {
        echo "✓ Email sent successfully!\n";
    } else {
        echo "✗ Email failed to send.\n";
    }
} catch (Exception $e) {
    echo "✗ Error: " . $e->getMessage() . "\n";
}

// Test the email body creation
echo "Testing email body creation...\n";
try {
    $emailBody = createEmailBody($testData);
    if (!empty($emailBody)) {
        echo "✓ Email body created successfully!\n";
        echo "Email body length: " . strlen($emailBody) . " characters\n";
    } else {
        echo "✗ Email body creation failed.\n";
    }
} catch (Exception $e) {
    echo "✗ Error: " . $e->getMessage() . "\n";
}

// Test logging
echo "Testing logging...\n";
try {
    logEmailAttempt($testData['invoiceId'], true);
    echo "✓ Logging test completed!\n";
} catch (Exception $e) {
    echo "✗ Error: " . $e->getMessage() . "\n";
}

echo "Test completed!\n";
?>
