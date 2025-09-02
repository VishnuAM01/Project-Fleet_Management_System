'use client';

import { useState, useEffect } from 'react';
import { Car, Users, DollarSign, Calendar, MapPin, Clock, TrendingUp, AlertCircle } from 'lucide-react';
import AuthGuard from '../../components/AuthGuard';
import Header from '../../components/Header';
import { getUserData, getLocationId } from '../../lib/auth';

export default function StaffDashboard() {
  const [userData, setUserData] = useState(null);
  const [locationId, setLocationId] = useState(null);
  const [recentBookings, setRecentBookings] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const data = getUserData();
    const locId = getLocationId();
    setUserData(data);
    setLocationId(locId);
    
    // Simulate loading recent bookings
    setTimeout(() => {
      setRecentBookings([
        {
          id: 1,
          memberName: 'John Doe',
          carName: 'Honda City',
          startDate: '2024-01-15',
          endDate: '2024-01-17',
          status: 'Active',
          amount: 2500
        },
        {
          id: 2,
          memberName: 'Jane Smith',
          carName: 'Maruti Swift',
          startDate: '2024-01-10',
          endDate: '2024-01-12',
          status: 'Completed',
          amount: 1800
        }
      ]);
      setLoading(false);
    }, 1000);
  }, []);

  const stats = [
    {
      title: 'Total Bookings',
      value: '45',
      icon: Calendar,
      color: 'bg-blue-500',
      change: '+12%',
      changeType: 'positive'
    },
    {
      title: 'Active Rentals',
      value: '8',
      icon: Car,
      color: 'bg-green-500',
      change: '+2',
      changeType: 'positive'
    },
    {
      title: 'Revenue Today',
      value: '₹25,500',
      icon: DollarSign,
      color: 'bg-purple-500',
      change: '+8%',
      changeType: 'positive'
    },
    {
      title: 'Available Cars',
      value: '12',
      icon: Car,
      color: 'bg-orange-500',
      change: '-3',
      changeType: 'negative'
    }
  ];

  const alerts = [
    {
      type: 'warning',
      message: 'Car #123 needs maintenance',
      time: '2 hours ago'
    },
    {
      type: 'info',
      message: 'New booking request received',
      time: '1 hour ago'
    },
    {
      type: 'success',
      message: 'Payment received for booking #456',
      time: '30 minutes ago'
    }
  ];

  return (
    <AuthGuard requiredUserType="staff">
      <div className="min-h-screen bg-gray-50">
        <Header />
        
        <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
          {/* Welcome Section */}
          <div className="mb-8">
            <h1 className="text-3xl font-bold text-gray-900 mb-2">
              Welcome back, {userData?.staffFirstName || 'Staff'}!
            </h1>
            <p className="text-black">
              Location: {locationId ? `Location ${locationId}` : 'Unknown Location'} • 
              Here's your daily overview.
            </p>
          </div>

          {/* Stats Grid */}
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6 mb-8">
            {stats.map((stat, index) => (
              <div key={index} className="bg-white rounded-lg shadow-sm p-6">
                <div className="flex items-center justify-between">
                  <div className="flex items-center">
                    <div className={`p-3 rounded-lg ${stat.color}`}>
                      <stat.icon className="h-6 w-6 text-white" />
                    </div>
                    <div className="ml-4">
                      <p className="text-sm font-medium text-black">{stat.title}</p>
                      <p className="text-2xl font-bold text-gray-900">{stat.value}</p>
                    </div>
                  </div>
                  <div className={`text-sm font-medium ${
                    stat.changeType === 'positive' ? 'text-green-600' : 'text-red-600'
                  }`}>
                    {stat.change}
                  </div>
                </div>
              </div>
            ))}
          </div>

          {/* Main Content Grid */}
          <div className="grid grid-cols-1 lg:grid-cols-3 gap-6 mb-8">
            {/* Quick Actions */}
            <div className="bg-white rounded-lg shadow-sm p-6">
              <h3 className="text-lg font-semibold text-gray-900 mb-4">Quick Actions</h3>
              <div className="space-y-3">
                <a
                  href="/staff/view-bookings"
                  className="flex items-center p-3 rounded-lg bg-blue-50 text-blue-700 hover:bg-blue-100 transition-colors"
                >
                  <Calendar className="h-5 w-5 mr-3" />
                  <span>View Bookings</span>
                </a>
                <a
                  href="/staff/assigned-cars"
                  className="flex items-center p-3 rounded-lg bg-green-50 text-green-700 hover:bg-green-100 transition-colors"
                >
                  <Car className="h-5 w-5 mr-3" />
                  <span>Manage Cars</span>
                </a>
                <a
                  href="/staff/Return"
                  className="flex items-center p-3 rounded-lg bg-orange-50 text-orange-700 hover:bg-orange-100 transition-colors"
                >
                  <Clock className="h-5 w-5 mr-3" />
                  <span>Process Returns</span>
                </a>
                <a
                  href="/staff/customers"
                  className="flex items-center p-3 rounded-lg bg-purple-50 text-purple-700 hover:bg-purple-100 transition-colors"
                >
                  <Users className="h-5 w-5 mr-3" />
                  <span>Customer Management</span>
                </a>
              </div>
            </div>

            {/* Alerts */}
            <div className="bg-white rounded-lg shadow-sm p-6">
              <h3 className="text-lg font-semibold text-gray-900 mb-4">Recent Alerts</h3>
              <div className="space-y-4">
                {alerts.map((alert, index) => (
                  <div key={index} className="flex items-start space-x-3">
                    <div className={`w-2 h-2 rounded-full mt-2 ${
                      alert.type === 'warning' ? 'bg-yellow-500' :
                      alert.type === 'info' ? 'bg-blue-500' :
                      'bg-green-500'
                    }`}></div>
                    <div className="flex-1">
                      <p className="text-sm font-medium text-gray-900">{alert.message}</p>
                      <p className="text-xs text-black">{alert.time}</p>
                    </div>
                  </div>
                ))}
              </div>
              <button className="w-full mt-4 text-blue-600 hover:text-blue-700 text-sm font-medium">
                View all alerts
              </button>
            </div>

            {/* Current Status */}
            <div className="bg-white rounded-lg shadow-sm p-6">
              <h3 className="text-lg font-semibold text-gray-900 mb-4">Location Status</h3>
              <div className="space-y-4">
                <div className="flex items-center justify-between">
                  <span className="text-sm text-black">Total Cars</span>
                  <span className="text-sm font-medium text-gray-900">20</span>
                </div>
                                 <div className="flex items-center justify-between">
                   <span className="text-sm text-black">Available</span>
                   <span className="text-sm font-medium text-green-600">12</span>
                 </div>
                 <div className="flex items-center justify-between">
                   <span className="text-sm text-black">Rented</span>
                   <span className="text-sm font-medium text-blue-600">8</span>
                 </div>
                 <div className="flex items-center justify-between">
                   <span className="text-sm text-black">Maintenance</span>
                   <span className="text-sm font-medium text-yellow-600">2</span>
                 </div>
                 <div className="flex items-center justify-between">
                   <span className="text-sm text-black">Out of Service</span>
                   <span className="text-sm font-medium text-red-600">1</span>
                 </div>
              </div>
              <div className="mt-4 pt-4 border-t border-gray-200">
                <div className="flex items-center justify-between">
                  <span className="text-sm font-medium text-gray-900">Utilization Rate</span>
                  <span className="text-sm font-medium text-blue-600">85%</span>
                </div>
                <div className="mt-2 w-full bg-gray-200 rounded-full h-2">
                  <div className="bg-blue-600 h-2 rounded-full" style={{ width: '85%' }}></div>
                </div>
              </div>
            </div>
          </div>

          {/* Recent Bookings */}
          <div className="bg-white rounded-lg shadow-sm">
            <div className="px-6 py-4 border-b border-gray-200 flex items-center justify-between">
              <h3 className="text-lg font-semibold text-gray-900">Recent Bookings</h3>
              <a
                href="/staff/view-bookings"
                className="text-blue-600 hover:text-blue-700 text-sm font-medium"
              >
                View all
              </a>
            </div>
            <div className="p-6">
              {loading ? (
                <div className="space-y-4">
                  {[1, 2, 3].map((i) => (
                    <div key={i} className="animate-pulse">
                      <div className="h-4 bg-gray-200 rounded w-1/4 mb-2"></div>
                      <div className="h-4 bg-gray-200 rounded w-1/2"></div>
                    </div>
                  ))}
                </div>
              ) : recentBookings.length > 0 ? (
                <div className="space-y-4">
                  {recentBookings.map((booking) => (
                    <div key={booking.id} className="flex items-center justify-between p-4 border border-gray-200 rounded-lg">
                      <div className="flex items-center space-x-4">
                        <div className="w-12 h-12 bg-blue-100 rounded-lg flex items-center justify-center">
                          <Car className="h-6 w-6 text-blue-600" />
                        </div>
                        <div>
                          <h4 className="font-medium text-gray-900">{booking.carName}</h4>
                                                     <p className="text-sm text-black">
                             {booking.memberName} • {booking.startDate} - {booking.endDate}
                           </p>
                        </div>
                      </div>
                      <div className="text-right">
                        <span className={`text-xs px-2 py-1 rounded-full ${
                          booking.status === 'Active' 
                            ? 'bg-green-100 text-green-800' 
                            : 'bg-gray-100 text-gray-800'
                        }`}>
                          {booking.status}
                        </span>
                        <p className="text-sm font-medium text-gray-900 mt-1">
                          ₹{booking.amount}
                        </p>
                      </div>
                    </div>
                  ))}
                </div>
              ) : (
                <div className="text-center py-8">
                                     <Calendar className="h-12 w-12 text-black mx-auto mb-4" />
                   <p className="text-black mb-4">No recent bookings</p>
                </div>
              )}
            </div>
          </div>

          {/* Performance Metrics */}
          <div className="grid grid-cols-1 md:grid-cols-2 gap-6 mt-8">
            <div className="bg-white rounded-lg shadow-sm p-6">
              <h3 className="text-lg font-semibold text-gray-900 mb-4">Today's Performance</h3>
              <div className="space-y-4">
                                 <div className="flex items-center justify-between">
                   <span className="text-sm text-black">New Bookings</span>
                   <span className="text-sm font-medium text-green-600">+5</span>
                 </div>
                 <div className="flex items-center justify-between">
                   <span className="text-sm text-black">Returns Processed</span>
                   <span className="text-sm font-medium text-blue-600">3</span>
                 </div>
                 <div className="flex items-center justify-between">
                   <span className="text-sm text-black">Customer Inquiries</span>
                   <span className="text-sm font-medium text-purple-600">8</span>
                 </div>
                 <div className="flex items-center justify-between">
                   <span className="text-sm text-black">Revenue Generated</span>
                   <span className="text-sm font-medium text-green-600">₹12,500</span>
                 </div>
              </div>
            </div>

            <div className="bg-white rounded-lg shadow-sm p-6">
              <h3 className="text-lg font-semibold text-gray-900 mb-4">Weekly Trends</h3>
              <div className="space-y-4">
                                 <div className="flex items-center space-x-2">
                   <TrendingUp className="h-4 w-4 text-green-600" />
                   <span className="text-sm text-black">Bookings increased by 15%</span>
                 </div>
                 <div className="flex items-center space-x-2">
                   <TrendingUp className="h-4 w-4 text-green-600" />
                   <span className="text-sm text-black">Revenue up by 8%</span>
                 </div>
                 <div className="flex items-center space-x-2">
                   <AlertCircle className="h-4 w-4 text-yellow-600" />
                   <span className="text-sm text-black">2 cars need maintenance</span>
                 </div>
                 <div className="flex items-center space-x-2">
                   <TrendingUp className="h-4 w-4 text-green-600" />
                   <span className="text-sm text-black">Customer satisfaction: 4.8/5</span>
                 </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </AuthGuard>
  );
}
