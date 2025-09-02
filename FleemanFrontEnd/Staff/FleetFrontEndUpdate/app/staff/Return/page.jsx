'use client';
import { useState, useEffect } from 'react';

export default function ReturnDashboard() {
  const [bookingData, setBookingData] = useState([]);
  const [returnedCars, setReturnedCars] = useState({});
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [selectedBooking, setSelectedBooking] = useState(null);
  const [selectedFuelStatus, setSelectedFuelStatus] = useState('');
  const [invoiceData, setInvoiceData] = useState({});

  // Fetch booking confirmations
  const fetchBookings = async () => {
    try {
      const res = await fetch('http://localhost:5231/api/BookingConfirmationDetails/confirmed-with-details');
      const data = await res.json();
      setBookingData(data);
    } catch (error) {
      console.error('Error fetching booking confirmations:', error);
    }
  };

  useEffect(() => {
    fetchBookings();
  }, []);

  const openModal = (booking) => {
    setSelectedBooking(booking);
    setSelectedFuelStatus('');
    setIsModalOpen(true);
  };

  const closeModal = () => {
    setIsModalOpen(false);
    setSelectedBooking(null);
  };

  const downloadInvoice = async (bookingId) => {
    try {
      const response = await fetch(`http://192.168.1.28:8080/api/invoice/${bookingId}`, {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
        },
      });

      if (response.ok) {
        // Get the blob from the response
        const blob = await response.blob();
        
        // Create a download link
        const url = window.URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.style.display = 'none';
        a.href = url;
        a.download = `invoice-${bookingId}.pdf`; // or whatever filename you want
        
        // Append to body, click, and remove
        document.body.appendChild(a);
        a.click();
        window.URL.revokeObjectURL(url);
        document.body.removeChild(a);
        
        alert(`✅ Invoice downloaded for booking ID ${bookingId}`);
      } else {
        alert('Failed to download invoice. Please try again.');
      }
    } catch (error) {
      console.error('Error downloading invoice:', error);
      alert('Error downloading invoice');
    }
  };

  const handleReturn = async () => {
    if (!selectedBooking) return;

    const { car } = selectedBooking;

    if (!selectedFuelStatus) {
      alert('Please select fuel status.');
      return;
    }

    // First API payload: booking ID, car ID, and drop location (with hardcoded location ID)
    const firstApiPayload = {
      bookingId: selectedBooking.bookingId,
      carId: selectedBooking.carId || null,
      dropLocation: 1,
      fuelStatus : selectedFuelStatus // Hardcoded location ID as requested
    };

    // Second API URL: car ID in path and fuel status as query parameter
    // const secondApiUrl = `http://192.168.1.28:8080/api/car/${car?.carId}/fuel-status?fuelStatus=${selectedFuelStatus}`;

    try {
      // Hit both APIs simultaneously using Promise.all
      const [firstApiResponse, secondApiResponse] = await Promise.all([
        // First API call
        fetch('http://localhost:5231/api/Invoice/create', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(firstApiPayload),
        }),
        // Second API call
        // fetch(secondApiUrl, {
        //   method: 'PUT',
        //   headers: {
        //     'Content-Type': 'application/json',
        //   },
        // })
      ]);

             // Check if both APIs were successful
       if (firstApiResponse.ok) {
         // Get the invoice data from the first API response
         const invoiceResponse = await firstApiResponse.json();
         
         setInvoiceData((prev) => ({
           ...prev,
           [selectedBooking.bookingId]: invoiceResponse,
         }));
         
         setReturnedCars((prev) => ({
           ...prev,
           [selectedBooking.bookingId]: { ...car, fuelStatus: selectedFuelStatus },
         }));
         
                   alert(`✅ Returned ${car?.carName} for booking ID ${selectedBooking.bookingId}. Invoice generated!`);
          
          // Refresh the booking data to get updated status from server
          await fetchBookings();
          
          closeModal();
       } else {
        // Handle individual API failures
        if (!firstApiResponse.ok) {
          const errorText = await firstApiResponse.text();
          console.error('First API Error response:', errorText);
          alert('Failed to save return info. Please try again.');
        }
      
      }
    } catch (error) {
      console.error('Error posting return data:', error);
      alert('Error saving return info');
    }
  };

  return (
    <div className="min-h-screen bg-white p-4">
      <div className="max-w-7xl mx-auto">
        <h1 className="text-2xl font-bold mb-6 text-gray-800">Vehicle Return Schedule</h1>

        <div className="overflow-x-auto bg-white shadow-lg rounded-lg border border-gray-200">
          <table className="min-w-full divide-y divide-gray-200">
            <thead className="bg-gray-100">
              <tr>
                        <th className="px-6 py-3 text-left text-xs font-medium text-black uppercase">Booking ID</th>
        <th className="px-6 py-3 text-left text-xs font-medium text-black uppercase">User Name</th>
        <th className="px-6 py-3 text-left text-xs font-medium text-black uppercase">Return Location</th>
        <th className="px-6 py-3 text-left text-xs font-medium text-black uppercase">Return DateTime</th>
        <th className="px-6 py-3 text-left text-xs font-medium text-black uppercase">Car Model</th>
        <th className="px-6 py-3 text-left text-xs font-medium text-black uppercase">Car</th>
        <th className="px-6 py-3 text-left text-xs font-medium text-black uppercase">Registration No</th>
        <th className="px-6 py-3 text-left text-xs font-medium text-black uppercase">Action</th>
              </tr>
            </thead>
            <tbody className="bg-white divide-y divide-gray-200">
              {bookingData.map((booking) => {
                const returned = returnedCars[booking.bookingId];
                const userName = booking.memberFirstName + " " + booking.memberLastName || 'Unknown';
                const returnLocation = booking.dropLocationName || 'Unknown';
                const returnDateTime = booking.returnDate;
                const carModel = booking.vehicleName || 'N/A';
                const car = booking.carName || 'N/A';
                const regNo = booking.carLicensePlate || 'N/A';

                return (
                  <tr key={booking.bookingId} className="hover:bg-gray-50 transition-colors">
                                      <td className="px-6 py-4 text-sm text-black">{booking.bookingId}</td>
                  <td className="px-6 py-4 text-sm font-medium text-gray-900">{userName}</td>
                  <td className="px-6 py-4 text-sm text-black">{returnLocation}</td>
                  <td className="px-6 py-4 text-sm text-black">{returnDateTime}</td>
                  <td className="px-6 py-4 text-sm text-black">{carModel}</td>
                  <td className="px-6 py-4 text-sm text-black">{car}</td>
                  <td className="px-6 py-4 text-sm text-black">{regNo}</td>
                                         <td className="px-6 py-4 text-sm">
                       {returned ? (
                         <button
                           onClick={() => downloadInvoice(booking.bookingId)}
                           className="text-green-600 hover:text-green-800 font-medium"
                         >
                           Download Invoice
                         </button>
                       ) : (
                         <button
                           onClick={() => openModal(booking)}
                           className="text-green-600 hover:text-green-800 font-medium"
                         >
                           Return
                         </button>
                       )}
                     </td>
                  </tr>
                );
              })}
            </tbody>
          </table>
        </div>

        {/* Modal */}
        {isModalOpen && selectedBooking && (
          <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
            <div className="bg-white rounded-xl p-6 w-full max-w-md shadow-2xl">
              <div className="flex justify-between items-center mb-4">
                <h2 className="text-lg font-semibold text-gray-800">Return Vehicle</h2>
                <button onClick={closeModal} className="text-black hover:text-gray-700">
                  ✕
                </button>
              </div>

              {returnedCars[selectedBooking.bookingId] ? (
                <div className="space-y-3 text-sm text-black">
                  <p>
                    <strong>Returned Car:</strong> {returnedCars[selectedBooking.bookingId].carName}
                  </p>
                  <p>
                    <strong>Reg No:</strong>{' '}
                    {returnedCars[selectedBooking.bookingId].registrationNUmber ||
                      returnedCars[selectedBooking.bookingId].registrationNumber}
                  </p>
                  <p>
                    <strong>Fuel Status:</strong> {returnedCars[selectedBooking.bookingId].fuelStatus}
                  </p>
                  {returnedCars[selectedBooking.bookingId].imgPath && (
                    <img
                      src={returnedCars[selectedBooking.bookingId].imgPath}
                      alt={returnedCars[selectedBooking.bookingId].carName}
                      className="w-full rounded mt-3"
                    />
                  )}
                  <div className="pt-4 text-right">
                    <button
                      onClick={closeModal}
                      className="px-4 py-2 bg-blue-600 text-white rounded hover:bg-blue-700"
                    >
                      Close
                    </button>
                  </div>
                </div>
              ) : (
                <div className="space-y-4 text-sm text-black">
                  <div>
                    <p>
                      <strong>Car:</strong> {selectedBooking.car?.carName || 'N/A'}
                    </p>
                    <p>
                      <strong>Reg No:</strong>{' '}
                      {selectedBooking.car?.registrationNUmber || selectedBooking.car?.registrationNumber || 'N/A'}
                    </p>
                  </div>

                  <div>
                    <label className="block font-medium mb-1">Fuel Status:</label>
                    <select
                      value={selectedFuelStatus}
                      onChange={(e) => setSelectedFuelStatus(e.target.value)}
                      className="w-full border border-gray-300 rounded px-3 py-2"
                    >
                      <option value="">-- Select --</option>
                      <option value="Full">Full</option>
                      <option value="Half">Half</option>
                      <option value="Empty">Empty</option>
                    </select>
                  </div>

                  <div className="flex justify-end gap-3 pt-2">
                    <button
                      onClick={closeModal}
                      className="px-4 py-2 bg-gray-200 rounded hover:bg-gray-300"
                    >
                      Cancel
                    </button>
                    <button
                      onClick={handleReturn}
                      className="px-4 py-2 bg-green-600 text-white rounded hover:bg-green-700"
                    >
                      Confirm Return
                    </button>
                  </div>
                </div>
              )}
            </div>
          </div>
        )}
      </div>
    </div>
  );
}
