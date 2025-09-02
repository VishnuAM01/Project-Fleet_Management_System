'use client';

import { useEffect, useState } from 'react';
import { isAuthenticated, isStaff, verifyToken, getToken } from '../lib/auth';

export default function AuthGuard({ children, redirectTo = '/login' }) {
  const [isLoading, setIsLoading] = useState(true);
  const [isAuthorized, setIsAuthorized] = useState(false);

  useEffect(() => {
    const checkAuth = async () => {
      try {
        // Check if user is authenticated
        if (!isAuthenticated()) {
          window.location.href = redirectTo;
          return;
        }

        // Verify token with backend
        const token = getToken();
        const verificationResult = await verifyToken(token);

        if (!verificationResult.success) {
          // Token is invalid or expired
          window.location.href = redirectTo;
          return;
        }

        // Check if user is staff
        if (!isStaff()) {
          // Redirect to login if not staff
          window.location.href = redirectTo;
          return;
        }

        setIsAuthorized(true);
      } catch (error) {
        console.error('Auth check failed:', error);
        window.location.href = redirectTo;
      } finally {
        setIsLoading(false);
      }
    };

    checkAuth();
  }, [redirectTo]);

  if (isLoading) {
    return (
      <div className="min-h-screen bg-gray-50 flex items-center justify-center">
        <div className="text-center">
          <div className="animate-spin rounded-full h-12 w-12 border-b-2 border-blue-600 mx-auto mb-4"></div>
          <p className="text-gray-600">Verifying authentication...</p>
        </div>
      </div>
    );
  }

  if (!isAuthorized) {
    return null;
  }

  return children;
}
