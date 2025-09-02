'use client';

import { useState, useEffect } from 'react';
import { User, LogOut, Car, Bell, Settings } from 'lucide-react';
import { getUserData, isStaff, logout } from '../lib/auth';

export default function Header() {
  const [userData, setUserData] = useState(null);
  const [showDropdown, setShowDropdown] = useState(false);

  useEffect(() => {
    const data = getUserData();
    setUserData(data);
  }, []);

  const handleLogout = async () => {
    await logout();
  };

  const getFullName = () => {
    if (!userData) return 'Staff Member';
    
    return `${userData.staffFirstName} ${userData.staffLastName}`;
  };

  const getRoleDisplay = () => {
    return 'Staff Member';
  };

  return (
    <header className="bg-white shadow-sm border-b">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="flex items-center justify-between h-16">
          {/* Logo and Navigation */}
          <div className="flex items-center space-x-8">
            <a href="/staff/dashboard" className="flex items-center space-x-2">
              <div className="w-8 h-8 bg-blue-600 rounded-lg flex items-center justify-center">
                <Car className="h-5 w-5 text-white" />
              </div>
              <span className="text-xl font-bold text-gray-900">IndiaDrive</span>
            </a>
          </div>

          {/* Right side - User menu */}
          <div className="flex items-center space-x-4">
            {/* Notifications */}
            <button className="p-2 text-gray-400 hover:text-gray-600 transition-colors">
              <Bell className="h-5 w-5" />
            </button>

            {/* Settings */}
            <button className="p-2 text-gray-400 hover:text-gray-600 transition-colors">
              <Settings className="h-5 w-5" />
            </button>

            {/* User dropdown */}
            <div className="relative">
              <button
                onClick={() => setShowDropdown(!showDropdown)}
                className="flex items-center space-x-3 p-2 rounded-lg hover:bg-gray-100 transition-colors"
              >
                <div className="w-8 h-8 bg-blue-600 rounded-full flex items-center justify-center">
                  <User className="h-4 w-4 text-white" />
                </div>
                <div className="hidden md:block text-left">
                  <p className="text-sm font-medium text-gray-900">{getFullName()}</p>
                  <p className="text-xs text-gray-500">{getRoleDisplay()}</p>
                </div>
              </button>

              {/* Dropdown menu */}
              {showDropdown && (
                <div className="absolute right-0 mt-2 w-48 bg-white rounded-lg shadow-lg border border-gray-200 py-2 z-50">
                  <div className="px-4 py-2 border-b border-gray-100">
                    <p className="text-sm font-medium text-gray-900">{getFullName()}</p>
                    <p className="text-xs text-gray-500">{userData?.email}</p>
                    <p className="text-xs text-gray-500">{getRoleDisplay()}</p>
                    {userData?.locationId && (
                      <p className="text-xs text-gray-500">Location ID: {userData.locationId}</p>
                    )}
                  </div>
                  
                  <a
                    href="/staff/dashboard"
                    className="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100"
                    onClick={() => setShowDropdown(false)}
                  >
                    Dashboard
                  </a>
                  
                  <a
                    href="/profile"
                    className="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100"
                    onClick={() => setShowDropdown(false)}
                  >
                    Profile
                  </a>
                  
                  <button
                    onClick={() => {
                      setShowDropdown(false);
                      handleLogout();
                    }}
                    className="w-full text-left px-4 py-2 text-sm text-red-600 hover:bg-red-50 flex items-center space-x-2"
                  >
                    <LogOut className="h-4 w-4" />
                    <span>Sign out</span>
                  </button>
                </div>
              )}
            </div>
          </div>
        </div>
      </div>

      {/* Close dropdown when clicking outside */}
      {showDropdown && (
        <div
          className="fixed inset-0 z-40"
          onClick={() => setShowDropdown(false)}
        />
      )}
    </header>
  );
}
