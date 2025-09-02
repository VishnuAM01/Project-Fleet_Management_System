import { NextResponse } from 'next/server'

export async function POST() {
  const response = NextResponse.json({ message: 'Logged out successfully' })
  
  // Clear the staff token cookie
  response.cookies.set('staffToken', '', {
    httpOnly: true,
    secure: process.env.NODE_ENV === 'production',
    sameSite: 'strict',
    maxAge: 0,
    path: '/'
  })
  
  return response
}
