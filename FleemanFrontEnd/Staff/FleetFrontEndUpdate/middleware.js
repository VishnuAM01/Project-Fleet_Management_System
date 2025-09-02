import { NextResponse } from 'next/server';

// Define protected routes - Staff only
const staffRoutes = [
  '/staff',
  '/staff/dashboard',
  '/staff/view-bookings',
  '/staff/assigned-cars',
  '/staff/Return',
  '/invoices'
];

export function middleware(request) {
  const { pathname } = request.nextUrl;
  
  // Check if the pathname is a staff route
  const isStaffRoute = staffRoutes.some(route => pathname.startsWith(route));

  // Get token from cookies
  const token = request.cookies.get('staffToken')?.value;

  // Only redirect if accessing staff routes without token
  if (isStaffRoute && !token) {
    return NextResponse.redirect(new URL('/login', request.url));
  }

  // If accessing root path without token, redirect to login
  if (pathname === '/' && !token) {
    return NextResponse.redirect(new URL('/login', request.url));
  }

  // If accessing root path with token, redirect to staff dashboard
  if (pathname === '/' && token) {
    return NextResponse.redirect(new URL('/staff/dashboard', request.url));
  }

  // Allow all other requests to proceed
  return NextResponse.next();
}

export const config = {
  matcher: [
    /*
     * Match all request paths except for the ones starting with:
     * - api (API routes)
     * - _next/static (static files)
     * - _next/image (image optimization files)
     * - favicon.ico (favicon file)
     */
    '/((?!api|_next/static|_next/image|favicon.ico).*)',
  ],
};
