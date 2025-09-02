import { NextResponse } from 'next/server';

export async function POST(request) {
  try {
    const { token } = await request.json();
    
    const response = NextResponse.json({ success: true });
    
    // Set the cookie with proper attributes
    response.cookies.set('staffToken', token, {
      httpOnly: false, // Allow client-side access
      secure: process.env.NODE_ENV === 'production',
      sameSite: 'lax',
      maxAge: 86400, // 24 hours
      path: '/',
    });
    
    return response;
  } catch (error) {
    return NextResponse.json({ success: false, error: 'Failed to set token' }, { status: 500 });
  }
}
