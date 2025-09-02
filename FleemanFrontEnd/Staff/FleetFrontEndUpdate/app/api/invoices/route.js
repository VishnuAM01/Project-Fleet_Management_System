import { NextResponse } from 'next/server';

// Mock data for demonstration - replace with actual API calls to your backend
const mockInvoices = [
  {
    invoiceId: 1,
    invoiceDate: "2024-01-15T14:30:00",
    bookingId: 10,
    actualReturnDate: "2024-01-15T14:30:00",
    dropLocation: 3,
    carId: 5,
    carRentalAmount: 150.00,
    addonRentalAmount: 25.00,
    totalAmount: 175.00,
    fuelStatus: "Full",
    carName: "Toyota Camry",
    memberName: "John Doe",
    dropLocationName: "Airport Hub"
  },
  {
    invoiceId: 2,
    invoiceDate: "2024-01-16T10:15:00",
    bookingId: 12,
    actualReturnDate: "2024-01-16T10:15:00",
    dropLocation: 1,
    carId: 8,
    carRentalAmount: 200.00,
    addonRentalAmount: 0.00,
    totalAmount: 200.00,
    fuelStatus: "Half",
    carName: "Honda City",
    memberName: "Jane Smith",
    dropLocationName: "City Center"
  },
  {
    invoiceId: 3,
    invoiceDate: "2024-01-17T16:45:00",
    bookingId: 15,
    actualReturnDate: "2024-01-17T16:45:00",
    dropLocation: 2,
    carId: 12,
    carRentalAmount: 300.00,
    addonRentalAmount: 50.00,
    totalAmount: 350.00,
    fuelStatus: "Full",
    carName: "Maruti Swift",
    memberName: "Mike Johnson",
    dropLocationName: "Mall Plaza"
  },
  {
    invoiceId: 4,
    invoiceDate: "2024-01-18T09:20:00",
    bookingId: 18,
    actualReturnDate: "2024-01-18T09:20:00",
    dropLocation: 4,
    carId: 3,
    carRentalAmount: 120.00,
    addonRentalAmount: 15.00,
    totalAmount: 135.00,
    fuelStatus: "Quarter",
    carName: "Hyundai i20",
    memberName: "Sarah Wilson",
    dropLocationName: "Station Square"
  },
  {
    invoiceId: 5,
    invoiceDate: "2024-01-19T13:10:00",
    bookingId: 22,
    actualReturnDate: "2024-01-19T13:10:00",
    dropLocation: 1,
    carId: 7,
    carRentalAmount: 180.00,
    addonRentalAmount: 30.00,
    totalAmount: 210.00,
    fuelStatus: "Full",
    carName: "Tata Nexon",
    memberName: "David Brown",
    dropLocationName: "City Center"
  }
];

export async function GET() {
  try {
    // In a real application, you would fetch data from your backend API
    // const response = await fetch('YOUR_BACKEND_URL/api/Invoice');
    // const invoices = await response.json();
    
    // For now, using mock data
    const invoices = mockInvoices;
    
    return NextResponse.json(invoices);
  } catch (error) {
    console.error('Error fetching invoices:', error);
    return NextResponse.json(
      { error: 'Failed to fetch invoices' },
      { status: 500 }
    );
  }
}

export async function POST(request) {
  try {
    const body = await request.json();
    
    // In a real application, you would send this to your backend API
    // const response = await fetch('YOUR_BACKEND_URL/api/Invoice/create', {
    //   method: 'POST',
    //   headers: {
    //     'Content-Type': 'application/json',
    //   },
    //   body: JSON.stringify(body),
    // });
    // const invoice = await response.json();
    
    // For now, creating a mock response
    const newInvoice = {
      invoiceId: Math.floor(Math.random() * 1000) + 100,
      invoiceDate: new Date().toISOString(),
      bookingId: body.bookingId,
      actualReturnDate: new Date().toISOString(),
      dropLocation: body.dropLocation,
      carId: body.carId,
      carRentalAmount: 150.00,
      addonRentalAmount: 25.00,
      totalAmount: 175.00,
      fuelStatus: body.fuelStatus,
      carName: "Sample Car",
      memberName: "Sample Customer",
      dropLocationName: "Sample Location"
    };
    
    return NextResponse.json(newInvoice, { status: 201 });
  } catch (error) {
    console.error('Error creating invoice:', error);
    return NextResponse.json(
      { error: 'Failed to create invoice' },
      { status: 500 }
    );
  }
}
