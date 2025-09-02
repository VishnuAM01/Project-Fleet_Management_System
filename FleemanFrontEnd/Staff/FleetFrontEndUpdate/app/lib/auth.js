// Authentication utility functions for Fleeman Car Rental system - Staff Only

const API_BASE_URL = 'http://localhost:5231/api';

// Token management
export const getToken = () => {
  if (typeof window !== 'undefined') {
    return localStorage.getItem('staffToken');
  }
  return null;
};

export const setToken = async (token) => {
  if (typeof window !== 'undefined') {
    localStorage.setItem('staffToken', token);
    
    // Set cookie via API route for better reliability
    try {
      await fetch('/api/set-token', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ token }),
      });
    } catch (error) {
      console.error('Failed to set cookie via API:', error);
    }
  }
};

export const removeToken = async () => {
  if (typeof window !== 'undefined') {
    localStorage.removeItem('staffToken');
    localStorage.removeItem('staffData');
    
    // Remove cookie via API route
    try {
      await fetch('/api/remove-token', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
      });
    } catch (error) {
      console.error('Failed to remove cookie via API:', error);
    }
  }
};

export const getUserData = () => {
  if (typeof window !== 'undefined') {
    const staffData = localStorage.getItem('staffData');
    return staffData ? JSON.parse(staffData) : null;
  }
  return null;
};

export const setUserData = (data) => {
  if (typeof window !== 'undefined') {
    localStorage.setItem('staffData', JSON.stringify(data));
  }
};

// API calls
export const staffLogin = async (email, password) => {
  try {
    const response = await fetch(`${API_BASE_URL}/Auth/staff-login`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ email, password }),
    });

    if (response.ok) {
      const data = await response.json();
      await setToken(data.token);
      setUserData(data);
      return { success: true, data };
    } else {
      const error = await response.json();
      return { success: false, error: error.message || 'Login failed' };
    }
  } catch (error) {
    return { success: false, error: 'Network error. Please try again.' };
  }
};

export const staffRegister = async (staffData) => {
  try {
    const response = await fetch(`${API_BASE_URL}/Auth/staff-register`, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(staffData),
    });

    if (response.ok) {
      const data = await response.json();
      return { success: true, data };
    } else {
      const error = await response.json();
      return { success: false, error: error.message || 'Registration failed' };
    }
  } catch (error) {
    return { success: false, error: 'Network error. Please try again.' };
  }
};

export const verifyToken = async (token) => {
  try {
    const response = await fetch(`${API_BASE_URL}/Auth/verify`, {
      method: 'POST',
      headers: {
        'Authorization': `Bearer ${token}`,
      },
    });

    if (response.ok) {
      const data = await response.json();
      return { success: true, data };
    } else {
      return { success: false, error: 'Token verification failed' };
    }
  } catch (error) {
    return { success: false, error: 'Network error during token verification' };
  }
};

// Authentication state management
export const isAuthenticated = () => {
  const token = getToken();
  return !!token;
};

export const isStaff = () => {
  const userData = getUserData();
  return userData && userData.roleId === 2;
};

export const getUserType = () => {
  if (isStaff()) return 'staff';
  return null;
};

export const getLocationId = () => {
  const userData = getUserData();
  return userData?.locationId;
};

// Logout function
export const logout = async () => {
  try {
    // Call logout API to clear server-side cookies
    await fetch('/api/logout', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
    });
  } catch (error) {
    console.error('Logout API call failed:', error);
  }
  
  await removeToken();
  if (typeof window !== 'undefined') {
    window.location.href = '/login';
  }
};

// Protected route helper
export const requireAuth = () => {
  if (!isAuthenticated()) {
    if (typeof window !== 'undefined') {
      window.location.href = '/login';
    }
    return false;
  }

  if (!isStaff()) {
    if (typeof window !== 'undefined') {
      window.location.href = '/login';
    }
    return false;
  }

  return true;
};

// API request helper with authentication
export const authenticatedFetch = async (url, options = {}) => {
  const token = getToken();
  
  const config = {
    ...options,
    headers: {
      'Content-Type': 'application/json',
      ...options.headers,
    },
  };

  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }

  try {
    const response = await fetch(url, config);
    
    if (response.status === 401) {
      // Token expired or invalid
      await logout();
      return { success: false, error: 'Authentication required' };
    }

    return response;
  } catch (error) {
    return { success: false, error: 'Network error' };
  }
};
