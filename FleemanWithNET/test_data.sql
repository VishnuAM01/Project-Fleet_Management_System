-- Fleet Management System - Test Data
-- Simple test entries for each table

-- 1. States
INSERT INTO state_details (State_Name) VALUES 
('California'),
('Texas'),
('New York');

-- 2. Cities
INSERT INTO city_details (City_Name, State_Id) VALUES 
('Los Angeles', 1),
('San Francisco', 1),
('Houston', 2),
('Dallas', 2),
('New York City', 3);

-- 3. Airports
INSERT INTO airport_details (Airport_Code, Airport_Name) VALUES 
('LAX', 'Los Angeles International Airport'),
('SFO', 'San Francisco International Airport'),
('IAH', 'George Bush Intercontinental Airport'),
('JFK', 'John F. Kennedy International Airport');

-- 4. Locations
INSERT INTO location_details (LocationName, Address, City_Id, State_Id, ZipCode, MobileNumber) VALUES 
('Downtown LA Office', '123 Main St, Los Angeles', 1, 1, '90001', '555-0101'),
('SF Airport Location', '456 Airport Blvd, San Francisco', 2, 1, '94102', '555-0102'),
('Houston Downtown', '789 Business Ave, Houston', 3, 2, '77001', '555-0103');

-- 5. Vehicle Types
INSERT INTO vehicle_details (VehicleType, DailyRate, WeeklyRate, MonthlyRate, ImgPath) VALUES 
('Economy', 50.00, 300.00, 1200.00, '/images/economy.jpg'),
('SUV', 80.00, 480.00, 1920.00, '/images/suv.jpg'),
('Luxury', 120.00, 720.00, 2880.00, '/images/luxury.jpg');

-- 6. Cars
INSERT INTO car_details (CarName, RegistrationNumber, FuelStatus, IsAvailable, LocationId, VehicleId, ImgPath) VALUES 
('Toyota Corolla', 'ABC123', 'Full', 1, 1, 1, '/images/corolla.jpg'),
('Honda CR-V', 'XYZ789', 'Full', 1, 1, 2, '/images/crv.jpg'),
('BMW 5 Series', 'DEF456', 'Full', 1, 2, 3, '/images/bmw.jpg');

-- 7. Add-ons
INSERT INTO add_on_details (addOnName, addOnPrice) VALUES 
('GPS Navigation', 15.00),
('Child Seat', 10.00),
('Insurance', 25.00);

-- 8. Members
INSERT INTO member_details (MemberFirstName, MemberLastName, Email, MobileNumber, DrivingLicenseId, Address, City, State, ZipCode) VALUES 
('John', 'Doe', 'john.doe@email.com', 5551234567, 'DL123456789', '123 Oak St', 'Los Angeles', 'California', 90001),
('Jane', 'Smith', 'jane.smith@email.com', 5559876543, 'DL987654321', '456 Pine Ave', 'San Francisco', 'California', 94102);

-- 9. Roles
INSERT INTO roles (RoleName, CreatedDate, IsActive) VALUES 
('Admin', GETDATE(), 1),
('Staff', GETDATE(), 1),
('Customer', GETDATE(), 1);

-- 10. Authentication
INSERT INTO authentication_table (email, Password, Role_Id) VALUES 
('admin@fleeman.com', 'admin123', 1),
('staff@fleeman.com', 'staff123', 2),
('john.doe@email.com', 'password123', 3);

-- 11. Bookings
INSERT INTO booking_details (MemberId, VehicleId, PickupLocation, DropLocation, PickupDate, ReturnDate, BookingDate) VALUES 
(1, 1, 1, 1, '2024-01-15 10:00:00', '2024-01-17 10:00:00', '2024-01-10 09:00:00'),
(2, 2, 2, 2, '2024-01-20 14:00:00', '2024-01-22 14:00:00', '2024-01-12 11:00:00');

-- 12. Booking Confirmations
INSERT INTO booking_confirmation_details (BookingId, Car_Id) VALUES 
(1, 1),
(2, 2);

-- 13. Add-on Bookings
INSERT INTO add_on_book (BookingId, add_on_id) VALUES 
(1, 1),
(1, 3),
(2, 2);

-- 14. Invoices
INSERT INTO invoice_header_table (BookingId, CarId, DropLocation, InvoiceDate, CarRentalAmount, AddonRentalAmount, ActualReturnDate) VALUES 
(1, 1, 1, '2024-01-17 10:00:00', 100.00, 40.00, '2024-01-17 10:00:00'),
(2, 2, 2, '2024-01-22 14:00:00', 160.00, 10.00, '2024-01-22 14:00:00');

PRINT 'Test data inserted successfully!';
