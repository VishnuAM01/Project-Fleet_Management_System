'use client';

import { useState, useEffect, useRef } from 'react';
import { Download, Eye, Search, Filter, Calendar, User, Car, DollarSign } from 'lucide-react';
import jsPDF from 'jspdf';
import html2canvas from 'html2canvas';
import { authenticatedFetch } from '../lib/auth';

export default function InvoicesPage() {
  const [invoices, setInvoices] = useState([]);
  const [loading, setLoading] = useState(true);
  const [searchTerm, setSearchTerm] = useState('');
  const [filterStatus, setFilterStatus] = useState('all');
  const [selectedInvoice, setSelectedInvoice] = useState(null);
  const [showModal, setShowModal] = useState(false);
  const [sendingEmail, setSendingEmail] = useState(false);
  const [error, setError] = useState(null);
  const [downloading, setDownloading] = useState(false);
  const pdfRef = useRef(null);

  useEffect(() => {
    fetchInvoices();
  }, []);

  const fetchInvoices = async () => {
    try {
      setLoading(true);
      const response = await authenticatedFetch('http://localhost:5231/api/Invoice');
      if (response.ok) {
        const data = await response.json();
        setInvoices(data);
      } else {
        console.error('Failed to fetch invoices');
      }
    } catch (error) {
      console.error('Error fetching invoices:', error);
    } finally {
      setLoading(false);
    }
  };

  const downloadInvoice = async (invoiceId) => {
    try {
      setDownloading(true);
      
      // First try to get the invoice data
      let invoiceData = invoices.find(inv => inv.invoiceId === invoiceId);
      
      if (!invoiceData) {
              // If not found in list, try API call
      const response = await authenticatedFetch(`http://localhost:5231/api/Invoice/get-invoice-by-id/${invoiceId}`);
      if (response.ok) {
        invoiceData = await response.json();
      } else {
        throw new Error('Invoice not found');
      }
      }

      // Generate PDF content
      const pdfContent = generatePDFContent(invoiceData);
      
      // Create PDF using jsPDF
      const pdf = new jsPDF('p', 'mm', 'a4');
      const pageWidth = pdf.internal.pageSize.getWidth();
      const pageHeight = pdf.internal.pageSize.getHeight();
      const margin = 20;
      const contentWidth = pageWidth - (2 * margin);
      
      let yPosition = margin;
      
      // Add header
      pdf.setFontSize(24);
      pdf.setTextColor(37, 99, 235); // Blue color
      pdf.text('IndiaDrive', pageWidth / 2, yPosition, { align: 'center' });
      yPosition += 10;
      
      pdf.setFontSize(16);
      pdf.setTextColor(107, 114, 128); // Gray color
      pdf.text('Rental Invoice', pageWidth / 2, yPosition, { align: 'center' });
      yPosition += 20;
      
      // Add invoice details
      pdf.setFontSize(12);
      pdf.setTextColor(0, 0, 0);
      
      // Invoice information
      pdf.setFontSize(14);
      pdf.setFont(undefined, 'bold');
      pdf.text('Invoice Information', margin, yPosition);
      yPosition += 8;
      
      pdf.setFontSize(10);
      pdf.setFont(undefined, 'normal');
      pdf.text(`Invoice #: ${invoiceData.invoiceId}`, margin, yPosition);
      yPosition += 6;
      pdf.text(`Invoice Date: ${formatDate(invoiceData.invoiceDate)}`, margin, yPosition);
      yPosition += 6;
      pdf.text(`Booking ID: ${invoiceData.bookingId}`, margin, yPosition);
      yPosition += 6;
      pdf.text(`Return Date: ${formatDate(invoiceData.actualReturnDate)}`, margin, yPosition);
      yPosition += 15;
      
      // Customer & Vehicle information
      pdf.setFontSize(14);
      pdf.setFont(undefined, 'bold');
      pdf.text('Customer & Vehicle', margin, yPosition);
      yPosition += 8;
      
      pdf.setFontSize(10);
      pdf.setFont(undefined, 'normal');
      pdf.text(`Customer: ${invoiceData.memberName}`, margin, yPosition);
      yPosition += 6;
      pdf.text(`Vehicle: ${invoiceData.carName}`, margin, yPosition);
      yPosition += 6;
      pdf.text(`Fuel Status: ${invoiceData.fuelStatus}`, margin, yPosition);
      yPosition += 6;
      pdf.text(`Drop Location: ${invoiceData.dropLocationName}`, margin, yPosition);
      yPosition += 15;
      
      // Amount breakdown
      pdf.setFontSize(14);
      pdf.setFont(undefined, 'bold');
      pdf.text('Amount Breakdown', margin, yPosition);
      yPosition += 8;
      
      pdf.setFontSize(10);
      pdf.setFont(undefined, 'normal');
      pdf.text(`Car Rental: ${formatCurrency(invoiceData.carRentalAmount)}`, margin, yPosition);
      yPosition += 6;
      pdf.text(`Addon Rental: ${formatCurrency(invoiceData.addonRentalAmount)}`, margin, yPosition);
      yPosition += 8;
      
      // Total amount
      pdf.setFontSize(12);
      pdf.setFont(undefined, 'bold');
      pdf.setTextColor(37, 99, 235); // Blue color
      pdf.text(`Total Amount: ${formatCurrency(invoiceData.totalAmount)}`, margin, yPosition);
      yPosition += 20;
      
      // Footer
      pdf.setFontSize(10);
      pdf.setTextColor(107, 114, 128);
      pdf.text('Thank you for choosing IndiaDrive!', pageWidth / 2, yPosition, { align: 'center' });
      yPosition += 5;
      pdf.text('For any queries, please contact our customer support.', pageWidth / 2, yPosition, { align: 'center' });
      
      // Save the PDF
      pdf.save(`invoice-${invoiceId}.pdf`);
      
      // Show success message
      const successMessage = document.createElement('div');
      successMessage.className = 'fixed top-4 right-4 bg-green-500 text-white px-6 py-3 rounded-lg shadow-lg z-50';
      successMessage.textContent = 'Invoice downloaded successfully!';
      document.body.appendChild(successMessage);
      
      setTimeout(() => {
        document.body.removeChild(successMessage);
      }, 3000);
      
    } catch (error) {
      console.error('Error downloading invoice:', error);
      
      // Show error message
      const errorMessage = document.createElement('div');
      errorMessage.className = 'fixed top-4 right-4 bg-red-500 text-white px-6 py-3 rounded-lg shadow-lg z-50';
      errorMessage.textContent = 'Failed to download invoice. Please try again.';
      document.body.appendChild(errorMessage);
      
      setTimeout(() => {
        document.body.removeChild(errorMessage);
      }, 3000);
    } finally {
      setDownloading(false);
    }
  };

  const generatePDFContent = (invoice) => {
    // This function is used for generating HTML content if needed
    // Currently using jsPDF directly for better control
    return `
      <div style="font-family: Arial, sans-serif; padding: 20px;">
        <h1 style="color: #2563eb; text-align: center;">IndiaDrive</h1>
        <h2 style="text-align: center; color: #666;">Rental Invoice</h2>
        
        <div style="margin: 20px 0;">
          <h3>Invoice Information</h3>
          <p><strong>Invoice #:</strong> ${invoice.invoiceId}</p>
          <p><strong>Invoice Date:</strong> ${formatDate(invoice.invoiceDate)}</p>
          <p><strong>Booking ID:</strong> ${invoice.bookingId}</p>
          <p><strong>Return Date:</strong> ${formatDate(invoice.actualReturnDate)}</p>
        </div>
        
        <div style="margin: 20px 0;">
          <h3>Customer & Vehicle</h3>
          <p><strong>Customer:</strong> ${invoice.memberName}</p>
          <p><strong>Vehicle:</strong> ${invoice.carName}</p>
          <p><strong>Fuel Status:</strong> ${invoice.fuelStatus}</p>
          <p><strong>Drop Location:</strong> ${invoice.dropLocationName}</p>
        </div>
        
        <div style="margin: 20px 0;">
          <h3>Amount Breakdown</h3>
          <p><strong>Car Rental:</strong> ${formatCurrency(invoice.carRentalAmount)}</p>
          <p><strong>Addon Rental:</strong> ${formatCurrency(invoice.addonRentalAmount)}</p>
          <p><strong>Total Amount:</strong> ${formatCurrency(invoice.totalAmount)}</p>
        </div>
        
        <div style="text-align: center; margin-top: 40px; color: #666;">
          <p>Thank you for choosing IndiaDrive!</p>
          <p>For any queries, please contact our customer support.</p>
        </div>
      </div>
    `;
  };

  const viewInvoice = async (invoiceId) => {
    try {
      // First try to get from the existing invoices list
      const existingInvoice = invoices.find(inv => inv.invoiceId === invoiceId);
      if (existingInvoice) {
        setSelectedInvoice(existingInvoice);
        setShowModal(true);
        return;
      }

      // If not found in list, try API call
      const response = await authenticatedFetch(`http://localhost:5231/api/Invoice/get-invoice-by-id/${invoiceId}`);
      if (response.ok) {
        const data = await response.json();
        setSelectedInvoice(data);
        setShowModal(true);
      } else {
        console.error('Failed to fetch invoice details');
        // Show a fallback modal with basic info
        setSelectedInvoice({
          invoiceId: invoiceId,
          invoiceDate: new Date().toISOString(),
          bookingId: 'N/A',
          actualReturnDate: new Date().toISOString(),
          dropLocation: 'N/A',
          carId: 'N/A',
          carRentalAmount: 0,
          addonRentalAmount: 0,
          totalAmount: 0,
          fuelStatus: 'N/A',
          carName: 'N/A',
          memberName: 'N/A',
          dropLocationName: 'N/A'
        });
        setShowModal(true);
      }
    } catch (error) {
      console.error('Error fetching invoice details:', error);
      // Show a fallback modal with basic info
      setSelectedInvoice({
        invoiceId: invoiceId,
        invoiceDate: new Date().toISOString(),
        bookingId: 'N/A',
        actualReturnDate: new Date().toISOString(),
        dropLocation: 'N/A',
        carId: 'N/A',
        carRentalAmount: 0,
        addonRentalAmount: 0,
        totalAmount: 0,
        fuelStatus: 'N/A',
        carName: 'N/A',
        memberName: 'N/A',
        dropLocationName: 'N/A'
      });
      setShowModal(true);
    }
  };

  const sendInvoiceEmail = async (invoiceId) => {
    try {
      setSendingEmail(true);
      setError(null);
      
      const response = await authenticatedFetch(`http://localhost:5231/api/Invoice/send-invoice-email/${invoiceId}`, {
        method: 'POST',
        body: JSON.stringify({
          invoiceId: invoiceId,
          email: 'customer@example.com' // You can add an email input field if needed
        }),
      });
      
      if (response.ok) {
        // Show success message
        const successMessage = document.createElement('div');
        successMessage.className = 'fixed top-4 right-4 bg-green-500 text-white px-6 py-3 rounded-lg shadow-lg z-50';
        successMessage.textContent = 'Invoice sent to email successfully!';
        document.body.appendChild(successMessage);
        
        setTimeout(() => {
          document.body.removeChild(successMessage);
        }, 3000);
      } else {
        setError('Failed to send invoice email. Please try again.');
      }
    } catch (error) {
      console.error('Error sending invoice email:', error);
      setError('Error sending invoice email. Please try again.');
    } finally {
      setSendingEmail(false);
    }
  };

  const filteredInvoices = invoices.filter(invoice => {
    const matchesSearch = 
      invoice.invoiceId.toString().includes(searchTerm) ||
      invoice.memberName?.toLowerCase().includes(searchTerm.toLowerCase()) ||
      invoice.carName?.toLowerCase().includes(searchTerm.toLowerCase());
    
    return matchesSearch;
  });

  const formatDate = (dateString) => {
    return new Date(dateString).toLocaleDateString('en-US', {
      year: 'numeric',
      month: 'short',
      day: 'numeric',
      hour: '2-digit',
      minute: '2-digit'
    });
  };

  const formatCurrency = (amount) => {
    return new Intl.NumberFormat('en-IN', {
      style: 'currency',
      currency: 'INR'
    }).format(amount);
  };

  if (loading) {
    return (
      <div className="min-h-screen bg-gray-50 flex items-center justify-center">
        <div className="animate-spin rounded-full h-32 w-32 border-b-2 border-blue-600"></div>
      </div>
    );
  }

  return (
    <div className="min-h-screen bg-gray-50">
      {/* Header */}
      <div className="bg-white shadow-sm border-b">
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-6">
          <div className="flex items-center justify-between">
            <div>
              <h1 className="text-3xl font-bold text-gray-900">Invoices</h1>
              <p className="mt-1 text-sm text-gray-500">
                Manage and download rental invoices
              </p>
            </div>
            <div className="flex items-center space-x-3">
              <button
                onClick={fetchInvoices}
                className="bg-blue-600 text-white px-4 py-2 rounded-lg hover:bg-blue-700 transition-colors"
              >
                Refresh
              </button>
            </div>
          </div>
        </div>
      </div>

      {/* Search and Filters */}
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-6">
        <div className="bg-white rounded-lg shadow-sm p-6 mb-6">
          <div className="flex flex-col sm:flex-row gap-4">
            <div className="flex-1">
              <div className="relative">
                <Search className="absolute left-3 top-1/2 transform -translate-y-1/2 text-gray-400 h-5 w-5" />
                <input
                  type="text"
                  placeholder="Search by invoice ID, customer name, or car..."
                  value={searchTerm}
                  onChange={(e) => setSearchTerm(e.target.value)}
                  className="w-full pl-10 pr-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                />
              </div>
            </div>
            {/* <div className="flex gap-2">
              <select
                value={filterStatus}
                onChange={(e) => setFilterStatus(e.target.value)}
                className="px-4 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent"
              >
                <option value="all">All Invoices</option>
                <option value="recent">Recent (Last 30 days)</option>
                <option value="high-value">High Value (₹1000)</option>
              </select>
            </div> */}
          </div>
        </div>

        {/* Invoices Grid */}
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
          {filteredInvoices.map((invoice) => (
            <div key={invoice.invoiceId} className="bg-white rounded-lg shadow-sm border hover:shadow-md transition-shadow">
              <div className="p-6">
                <div className="flex items-center justify-between mb-4">
                  <div className="flex items-center space-x-2">
                    <div className="w-2 h-2 bg-green-500 rounded-full"></div>
                    <span className="text-sm font-medium text-gray-900">
                      Invoice #{invoice.invoiceId}
                    </span>
                  </div>
                  <span className="text-sm text-gray-500">
                    {formatDate(invoice.invoiceDate)}
                  </span>
                </div>

                <div className="space-y-3 mb-4">
                  <div className="flex items-center space-x-2">
                    <User className="h-4 w-4 text-gray-400" />
                    <span className="text-sm text-gray-600">{invoice.memberName}</span>
                  </div>
                  <div className="flex items-center space-x-2">
                    <Car className="h-4 w-4 text-gray-400" />
                    <span className="text-sm text-gray-600">{invoice.carName}</span>
                  </div>
                  <div className="flex items-center space-x-2">
                    <Calendar className="h-4 w-4 text-gray-400" />
                    <span className="text-sm text-gray-600">
                      {formatDate(invoice.actualReturnDate)}
                    </span>
                  </div>
                  <div className="flex items-center space-x-2">
                    <DollarSign className="h-4 w-4 text-gray-400" />
                    <span className="text-sm font-semibold text-gray-900">
                      {formatCurrency(invoice.totalAmount)}
                    </span>
                  </div>
                </div>

                <div className="flex space-x-2">
                  <button
                    onClick={() => viewInvoice(invoice.invoiceId)}
                    className="flex-1 flex items-center justify-center space-x-1 bg-gray-100 text-gray-700 px-3 py-2 rounded-lg hover:bg-gray-200 transition-colors"
                  >
                    <Eye className="h-4 w-4" />
                    <span className="text-sm">View</span>
                  </button>
                                     <button
                     onClick={() => downloadInvoice(invoice.invoiceId)}
                     disabled={downloading}
                     className={`flex-1 flex items-center justify-center space-x-1 px-3 py-2 rounded-lg transition-colors ${
                       downloading 
                         ? 'bg-gray-400 text-white cursor-not-allowed' 
                         : 'bg-blue-600 text-white hover:bg-blue-700'
                     }`}
                   >
                     {downloading ? (
                       <div className="animate-spin rounded-full h-4 w-4 border-b-2 border-white"></div>
                     ) : (
                       <Download className="h-4 w-4" />
                     )}
                     <span className="text-sm">{downloading ? 'Generating...' : 'Download'}</span>
                   </button>
                </div>
              </div>
            </div>
          ))}
        </div>

        {filteredInvoices.length === 0 && !loading && (
          <div className="text-center py-12">
            <div className="text-gray-400 mb-4">
              <svg className="mx-auto h-12 w-12" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M9 12h6m-6 4h6m2 5H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z" />
              </svg>
            </div>
            <h3 className="text-lg font-medium text-gray-900 mb-2">No invoices found</h3>
            <p className="text-gray-500">No invoices match your search criteria.</p>
          </div>
        )}
      </div>

      {/* Invoice Detail Modal */}
      {showModal && selectedInvoice && (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center p-4 z-50">
          <div className="bg-white rounded-lg max-w-2xl w-full max-h-[90vh] overflow-y-auto">
            <div className="p-6">
              <div className="flex items-center justify-between mb-6">
                <h2 className="text-2xl font-bold text-gray-900">
                  Invoice #{selectedInvoice.invoiceId}
                </h2>
                <button
                  onClick={() => setShowModal(false)}
                  className="text-gray-400 hover:text-gray-600"
                >
                  <svg className="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M6 18L18 6M6 6l12 12" />
                  </svg>
                </button>
              </div>

              <div className="grid grid-cols-1 md:grid-cols-2 gap-6 mb-6">
                <div>
                  <h3 className="text-lg font-semibold text-gray-900 mb-3">Invoice Details</h3>
                  <div className="space-y-2">
                    <div className="flex justify-between">
                      <span className="text-gray-600">Invoice Date:</span>
                      <span className="font-medium text-black">{formatDate(selectedInvoice.invoiceDate)}</span>
                    </div>
                    <div className="flex justify-between">
                      <span className="text-gray-600">Booking ID:</span>
                      <span className="font-medium text-black">{selectedInvoice.bookingId}</span>
                    </div>
                    <div className="flex justify-between">
                      <span className="text-gray-600">Return Date:</span>
                      <span className="font-medium text-black">{formatDate(selectedInvoice.actualReturnDate)}</span>
                    </div>
                  </div>
                </div>

                <div>
                  <h3 className="text-lg font-semibold text-gray-900 mb-3">Customer & Vehicle</h3>
                  <div className="space-y-2">
                    <div className="flex justify-between">
                      <span className="text-gray-600">Customer:</span>
                      <span className="font-medium text-black">{selectedInvoice.memberName}</span>
                    </div>
                    <div className="flex justify-between">
                      <span className="text-gray-600">Vehicle:</span>
                      <span className="font-medium text-black">{selectedInvoice.carName}</span>
                    </div>
                    <div className="flex justify-between">
                      <span className="text-gray-600">Fuel Status:</span>
                      <span className="font-medium text-black">{selectedInvoice.fuelStatus}</span>
                    </div>
                    <div className="flex justify-between">
                      <span className="text-gray-600">Drop Location:</span>
                      <span className="font-medium text-black">{selectedInvoice.dropLocationName}</span>
                    </div>
                  </div>
                </div>
              </div>

                             <div className="border-t pt-6">
                 <h3 className="text-lg font-semibold text-gray-900 mb-3">Amount Breakdown</h3>
                 <div className="space-y-2">
                   <div className="flex justify-between">
                     <span className="text-gray-600">Car Rental:</span>
                     <span className="font-medium text-black">{formatCurrency(selectedInvoice.carRentalAmount)}</span>
                   </div>
                   <div className="flex justify-between">
                     <span className="text-gray-600">Addon Rental:</span>
                     <span className="font-medium text-black">{formatCurrency(selectedInvoice.addonRentalAmount)}</span>
                   </div>
                   <div className="flex justify-between text-lg font-bold border-t pt-2">
                     <span className='text-black'>Total Amount:</span>
                     <span className="text-blue-600">{formatCurrency(selectedInvoice.totalAmount)}</span>
                   </div>
                 </div>
               </div>

               {error && (
                 <div className="mt-4 p-3 bg-red-100 border border-red-400 text-red-700 rounded-lg">
                   {error}
                 </div>
               )}

                             <div className="flex space-x-3 mt-6">
                 <button
                   onClick={() => downloadInvoice(selectedInvoice.invoiceId)}
                   disabled={downloading}
                   className={`flex-1 px-4 py-2 rounded-lg transition-colors flex items-center justify-center space-x-2 ${
                     downloading 
                       ? 'bg-gray-400 text-white cursor-not-allowed' 
                       : 'bg-blue-600 text-white hover:bg-blue-700'
                   }`}
                 >
                   {downloading ? (
                     <div className="animate-spin rounded-full h-4 w-4 border-b-2 border-white"></div>
                   ) : (
                     <Download className="h-4 w-4" />
                   )}
                   <span>{downloading ? 'Generating...' : 'Download PDF'}</span>
                 </button>
                 <button
                   onClick={() => sendInvoiceEmail(selectedInvoice.invoiceId)}
                   disabled={sendingEmail}
                   className={`flex-1 px-4 py-2 rounded-lg transition-colors flex items-center justify-center space-x-2 ${
                     sendingEmail 
                       ? 'bg-gray-400 text-white cursor-not-allowed' 
                       : 'bg-green-600 text-white hover:bg-green-700'
                   }`}
                 >
                   {sendingEmail ? (
                     <div className="animate-spin rounded-full h-4 w-4 border-b-2 border-white"></div>
                   ) : (
                     <svg className="h-4 w-4" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                       <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M3 8l7.89 4.26a2 2 0 002.22 0L21 8M5 19h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v10a2 2 0 002 2z" />
                     </svg>
                   )}
                   <span>{sendingEmail ? 'Sending...' : 'Send to Email'}</span>
                 </button>
                 <button
                   onClick={() => setShowModal(false)}
                   className="flex-1 bg-gray-100 text-gray-700 px-4 py-2 rounded-lg hover:bg-gray-200 transition-colors"
                 >
                   Close
                 </button>
               </div>
            </div>
          </div>
        </div>
      )}
    </div>
  );
}
