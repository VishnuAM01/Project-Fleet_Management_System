import Link from "next/link";

export default function Home() {
  return (
    <div className="min-h-screen bg-gray-50">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-12">
        <div className="text-center">
          <h1 className="text-4xl font-bold text-black mb-4">
            Welcome to IndiaDrive
          </h1>
          <p className="text-xl text-gray-600 mb-8">
            Your trusted car rental management system
          </p>
          
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6 max-w-4xl mx-auto">
            <Link href="/invoices" className="group">
              <div className="bg-white rounded-lg shadow-sm border p-6 hover:shadow-md transition-shadow">
                <div className="text-blue-600 mb-4">
                  <svg className="w-12 h-12 mx-auto" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M9 12h6m-6 4h6m2 5H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z" />
                  </svg>
                </div>
                <h3 className="text-lg font-semibold text-gray-900 mb-2">Invoice Management</h3>
                <p className="text-gray-600">View and download rental invoices</p>
              </div>
            </Link>
            
            <div className="bg-white rounded-lg shadow-sm border p-6">
              <div className="text-green-600 mb-4">
                <svg className="w-12 h-12 mx-auto" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 8v4l3 3m6-3a9 9 0 11-18 0 9 9 0 0118 0z" />
                </svg>
              </div>
              <h3 className="text-lg font-semibold text-gray-900 mb-2">Booking Management</h3>
              <p className="text-gray-600">Manage car bookings and reservations</p>
            </div>
            
            <div className="bg-white rounded-lg shadow-sm border p-6">
              <div className="text-purple-600 mb-4">
                <svg className="w-12 h-12 mx-auto" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M19 21V5a2 2 0 00-2-2H7a2 2 0 00-2 2v16m14 0h2m-2 0h-5m-9 0H3m2 0h5M9 7h1m-1 4h1m4-4h1m-1 4h1m-5 10v-5a1 1 0 011-1h2a1 1 0 011 1v5m-4 0h4" />
                </svg>
              </div>
              <h3 className="text-lg font-semibold text-gray-900 mb-2">Fleet Management</h3>
              <p className="text-gray-600">Track vehicle availability and status</p>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}
