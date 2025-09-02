'use client';

import { useState, useEffect } from 'react';
import { Eye, EyeOff, User, Lock, Car, Mail, Phone, MapPin, Calendar, Building, Briefcase, DollarSign } from 'lucide-react';
import { staffRegister, isAuthenticated, getUserType } from '../lib/auth';

export default function RegisterPage() {
  const [showPassword, setShowPassword] = useState(false);
  const [showConfirmPassword, setShowConfirmPassword] = useState(false);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState('');
  const [success, setSuccess] = useState('');

  // Staff form data
  const [staffData, setStaffData] = useState({
    email: '',
    password: '',
    confirmPassword: '',
    staffFirstName: '',
    staffLastName: '',
    mobileNumber: '',
    address: '',
    city: '',
    state: '',
    zipCode: '',
    dateOfBirth: '',
    department: '',
    position: '',
    salary: '',
    locationId: 1
  });

  useEffect(() => {
    // Redirect if already authenticated
    if (isAuthenticated()) {
      const type = getUserType();
      if (type === 'staff') {
        window.location.href = '/staff/dashboard';
      }
    }
  }, []);

  const handleInputChange = (e) => {
    setStaffData({
      ...staffData,
      [e.target.name]: e.target.value
    });
    if (error) setError('');
  };

  const validateForm = () => {
    if (!staffData.email || !staffData.password || !staffData.confirmPassword) {
      setError('Please fill in all required fields.');
      return false;
    }

    if (staffData.password !== staffData.confirmPassword) {
      setError('Passwords do not match.');
      return false;
    }

    if (staffData.password.length < 6) {
      setError('Password must be at least 6 characters long.');
      return false;
    }

    // Email validation
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(staffData.email)) {
      setError('Please enter a valid email address.');
      return false;
    }

    return true;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    setError('');
    setSuccess('');

    if (!validateForm()) {
      setLoading(false);
      return;
    }

    try {
      // Remove confirmPassword from data to send
      const { confirmPassword, ...dataWithoutConfirm } = staffData;
      
      const result = await staffRegister(dataWithoutConfirm);

      if (result.success) {
        setSuccess('Staff registration successful! Redirecting to login...');
        // Clear form
        setStaffData({
          email: '',
          password: '',
          confirmPassword: '',
          staffFirstName: '',
          staffLastName: '',
          mobileNumber: '',
          address: '',
          city: '',
          state: '',
          zipCode: '',
          dateOfBirth: '',
          department: '',
          position: '',
          salary: '',
          locationId: 1
        });
        
        // Redirect to login after a short delay
        setTimeout(() => {
          window.location.href = '/login';
        }, 2000);
      } else {
        setError(result.error);
      }
    } catch (error) {
      setError('An unexpected error occurred. Please try again.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="min-h-screen bg-gradient-to-br from-blue-50 to-indigo-100 py-8 px-4">
      <div className="max-w-2xl mx-auto">
        {/* Header */}
        <div className="text-center mb-8">
          <div className="mx-auto h-16 w-16 bg-blue-600 rounded-full flex items-center justify-center mb-4">
            <Car className="h-8 w-8 text-white" />
          </div>
          <h2 className="text-3xl font-bold text-gray-900 mb-2">
            Staff Registration - IndiaDrive
          </h2>
          <p className="text-black">
            Create your staff account to start your journey
          </p>
        </div>

        {/* Registration Form */}
        <div className="bg-white rounded-lg shadow-lg p-8">
          <form onSubmit={handleSubmit} className="space-y-6">
            {/* Error/Success Messages */}
            {error && (
              <div className="bg-red-50 border border-red-200 text-red-700 px-4 py-3 rounded-lg">
                {error}
              </div>
            )}

            {success && (
              <div className="bg-green-50 border border-green-200 text-green-700 px-4 py-3 rounded-lg">
                {success}
              </div>
            )}

            {/* Personal Information */}
            <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
              <div>
                <label className="block text-sm font-medium text-black mb-2">
                  First Name
                </label>
                <div className="relative">
                  <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
                    <User className="h-5 w-5 text-black" />
                  </div>
                  <input
                    type="text"
                    name="staffFirstName"
                    value={staffData.staffFirstName}
                    onChange={handleInputChange}
                    required
                    className="block w-full pl-10 pr-3 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                    placeholder="Enter first name"
                  />
                </div>
              </div>

              <div>
                <label className="block text-sm font-medium text-black mb-2">
                  Last Name
                </label>
                <div className="relative">
                  <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
                    <User className="h-5 w-5 text-black" />
                  </div>
                  <input
                    type="text"
                    name="staffLastName"
                    value={staffData.staffLastName}
                    onChange={handleInputChange}
                    required
                    className="block w-full pl-10 pr-3 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                    placeholder="Enter last name"
                  />
                </div>
              </div>
            </div>

            {/* Contact Information */}
            <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
              <div>
                <label className="block text-sm font-medium text-black mb-2">
                  Email Address
                </label>
                <div className="relative">
                  <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
                    <Mail className="h-5 w-5 text-black" />
                  </div>
                  <input
                    type="email"
                    name="email"
                    value={staffData.email}
                    onChange={handleInputChange}
                    required
                    className="block w-full pl-10 pr-3 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                    placeholder="Enter email address"
                  />
                </div>
              </div>

              <div>
                <label className="block text-sm font-medium text-black mb-2">
                  Mobile Number
                </label>
                <div className="relative">
                  <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
                    <Phone className="h-5 w-5 text-black" />
                  </div>
                  <input
                    type="tel"
                    name="mobileNumber"
                    value={staffData.mobileNumber}
                    onChange={handleInputChange}
                    required
                    className="block w-full pl-10 pr-3 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                    placeholder="Enter mobile number"
                  />
                </div>
              </div>
            </div>

            {/* Address Information */}
            <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
              <div>
                <label className="block text-sm font-medium text-black mb-2">
                  Address
                </label>
                <div className="relative">
                  <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
                    <MapPin className="h-5 w-5 text-black" />
                  </div>
                  <input
                    type="text"
                    name="address"
                    value={staffData.address}
                    onChange={handleInputChange}
                    required
                    className="block w-full pl-10 pr-3 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                    placeholder="Enter address"
                  />
                </div>
              </div>

              <div>
                <label className="block text-sm font-medium text-black mb-2">
                  City
                </label>
                <input
                  type="text"
                  name="city"
                  value={staffData.city}
                  onChange={handleInputChange}
                  required
                  className="block w-full px-3 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                  placeholder="Enter city"
                />
              </div>

              <div>
                <label className="block text-sm font-medium text-black mb-2">
                  State
                </label>
                <input
                  type="text"
                  name="state"
                  value={staffData.state}
                  onChange={handleInputChange}
                  required
                  className="block w-full px-3 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                  placeholder="Enter state"
                />
              </div>
            </div>

            <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
              <div>
                <label className="block text-sm font-medium text-black mb-2">
                  ZIP Code
                </label>
                <input
                  type="text"
                  name="zipCode"
                  value={staffData.zipCode}
                  onChange={handleInputChange}
                  required
                  className="block w-full px-3 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                  placeholder="Enter ZIP code"
                />
              </div>

              <div>
                <label className="block text-sm font-medium text-black mb-2">
                  Date of Birth
                </label>
                <div className="relative">
                  <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
                    <Calendar className="h-5 w-5 text-black" />
                  </div>
                  <input
                    type="date"
                    name="dateOfBirth"
                    value={staffData.dateOfBirth}
                    onChange={handleInputChange}
                    required
                    className="block w-full pl-10 pr-3 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                  />
                </div>
              </div>
            </div>

            {/* Staff-specific fields */}
            <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
              <div>
                <label className="block text-sm font-medium text-black mb-2">
                  Department
                </label>
                <div className="relative">
                  <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
                    <Building className="h-5 w-5 text-black" />
                  </div>
                  <input
                    type="text"
                    name="department"
                    value={staffData.department}
                    onChange={handleInputChange}
                    required
                    className="block w-full pl-10 pr-3 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                    placeholder="Enter department"
                  />
                </div>
              </div>

              <div>
                <label className="block text-sm font-medium text-black mb-2">
                  Position
                </label>
                <div className="relative">
                  <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
                    <Briefcase className="h-5 w-5 text-black" />
                  </div>
                  <input
                    type="text"
                    name="position"
                    value={staffData.position}
                    onChange={handleInputChange}
                    required
                    className="block w-full pl-10 pr-3 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                    placeholder="Enter position"
                  />
                </div>
              </div>

              <div>
                <label className="block text-sm font-medium text-black mb-2">
                  Salary
                </label>
                <div className="relative">
                  <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
                    <DollarSign className="h-5 w-5 text-black" />
                  </div>
                  <input
                    type="number"
                    name="salary"
                    value={staffData.salary}
                    onChange={handleInputChange}
                    required
                    className="block w-full pl-10 pr-3 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                    placeholder="Enter salary"
                  />
                </div>
              </div>
            </div>

            {/* Password Fields */}
            <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
              <div>
                <label className="block text-sm font-medium text-black mb-2">
                  Password
                </label>
                <div className="relative">
                  <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
                    <Lock className="h-5 w-5 text-black" />
                  </div>
                  <input
                    type={showPassword ? 'text' : 'password'}
                    name="password"
                    value={staffData.password}
                    onChange={handleInputChange}
                    required
                    className="block w-full pl-10 pr-12 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                    placeholder="Enter password"
                  />
                  <button
                    type="button"
                    onClick={() => setShowPassword(!showPassword)}
                    className="absolute inset-y-0 right-0 pr-3 flex items-center"
                  >
                    {showPassword ? (
                      <EyeOff className="h-5 w-5 text-black hover:text-gray-800" />
                    ) : (
                      <Eye className="h-5 w-5 text-black hover:text-gray-800" />
                    )}
                  </button>
                </div>
              </div>

              <div>
                <label className="block text-sm font-medium text-black mb-2">
                  Confirm Password
                </label>
                <div className="relative">
                  <div className="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
                    <Lock className="h-5 w-5 text-black" />
                  </div>
                  <input
                    type={showConfirmPassword ? 'text' : 'password'}
                    name="confirmPassword"
                    value={staffData.confirmPassword}
                    onChange={handleInputChange}
                    required
                    className="block w-full pl-10 pr-12 py-3 border border-gray-300 rounded-lg focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                    placeholder="Confirm password"
                  />
                  <button
                    type="button"
                    onClick={() => setShowConfirmPassword(!showConfirmPassword)}
                    className="absolute inset-y-0 right-0 pr-3 flex items-center"
                  >
                    {showConfirmPassword ? (
                      <EyeOff className="h-5 w-5 text-black hover:text-gray-800" />
                    ) : (
                      <Eye className="h-5 w-5 text-black hover:text-gray-800" />
                    )}
                  </button>
                </div>
              </div>
            </div>

            {/* Submit Button */}
            <button
              type="submit"
              disabled={loading}
              className={`w-full flex justify-center py-3 px-4 border border-transparent rounded-lg text-sm font-medium text-white transition-colors ${
                loading
                  ? 'bg-gray-400 cursor-not-allowed'
                  : 'bg-blue-600 hover:bg-blue-700 focus:ring-2 focus:ring-blue-500 focus:ring-offset-2'
              }`}
            >
              {loading ? (
                <div className="flex items-center space-x-2">
                  <div className="animate-spin rounded-full h-4 w-4 border-b-2 border-white"></div>
                  <span>Creating account...</span>
                </div>
              ) : (
                'Create Staff Account'
              )}
            </button>
          </form>

          {/* Additional Links */}
          <div className="mt-6 text-center">
            <p className="text-sm text-black">
              Already have a staff account?{' '}
              <a href="/login" className="font-medium text-blue-600 hover:text-blue-500">
                Sign in here
              </a>
            </p>
          </div>
        </div>

        {/* Footer */}
        <div className="text-center text-sm text-black mt-8">
          <p>&copy; 2024 IndiaDrive. All rights reserved.</p>
        </div>
      </div>
    </div>
  );
}
