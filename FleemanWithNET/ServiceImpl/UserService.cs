using Azure.Core;
using fleeman_with_dot_net.DTO;
using fleeman_with_dot_net.Models;
using fleeman_with_dot_net.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace fleeman_with_dot_net.Services
{
    public class UserService : IUser
    {
        private readonly FleetDBContext _context;
        private readonly PasswordHasher<AuthenticationTable> _hasher;

        public UserService(FleetDBContext context)
        {
            _context = context;
            _hasher = new PasswordHasher<AuthenticationTable>();
        }


        public async Task Register(MemberRegistrationRequest request)
        {
            var existingUser = await _context.authentication_tables
                .FirstOrDefaultAsync(u => u.email == request.Email);

            if (existingUser != null)
                throw new Exception("User already exists.");

           
            var auth = new AuthenticationTable
            {
                email = request.Email,
               
                Role_Id = request.Role_Id
            };
            // Hash password
            auth.Password = _hasher.HashPassword(auth, request.Password);

            _context.authentication_tables.Add(auth);
            var member = new MemberDetails
            {
                MemberFirstName = request.FirstName,
                MemberLastName = request.LastName,
                Address = $"{request.Address1} {request.Address2}",
                City = request.City,
                ZipCode = int.TryParse(request.Zip, out var zip) ? zip : (int?)null,
                Email = request.Email,
                MobileNumber = long.TryParse(request.Cell, out var mob) ? mob : 0,
                DrivingLicenseId = request.DrivingLicense,
                DrivingLicenseValidThru = request.DlValidThru,
                PassportNo = request.PassportNo,
                PassportIssuedBy = request.PassportIssuedBy,
                PassportValidate = request.PassportValid,
                Dob = request.BirthDate,
                Idp = request.IdpNo,
                IdpIssuedBy = DateTime.TryParse(request.IdpIssuedBy, out var idpDate) ? idpDate : (DateTime?)null,
                IdpValidThru = request.IdpValidThru
            };

            _context.member_details.Add(member);

            _context.SaveChanges();

        }

        public async Task<bool> ValidateUser(AuthenticationTable user)
        {
            var existingUser = await _context.authentication_tables
                .FirstOrDefaultAsync(u => u.email == user.email);

            if (existingUser == null) return false;

            var result = _hasher.VerifyHashedPassword(existingUser, existingUser.Password, user.Password);
            return result == PasswordVerificationResult.Success;
        }

        public async Task<AuthenticationTable> GetUserByMemberId(string memberId)
        {
            return await _context.authentication_tables
                .FirstOrDefaultAsync(u => u.email == memberId);
        }

        public async Task<LoginResponseDTO> GetUserDetailsForLogin(string email)
        {
            var memberDetails = await _context.member_details
                .FirstOrDefaultAsync(m => m.Email == email);

            var authUser = await _context.authentication_tables
                .FirstOrDefaultAsync(a => a.email == email);

            if (memberDetails == null || authUser == null)
                throw new Exception("User not found.");

            return new LoginResponseDTO
            {
                MemberId = memberDetails.Member_Id,
                Email = email,
                RoleId = authUser.Role_Id,
                Token = "" // Token will be set in the controller
            };
        }

        public async Task<bool> ValidateStaff(StaffLoginRequestDTO staffLogin)
        {
            // Check if staff exists with the given email
            var staffDetails = await _context.staff_details
                .FirstOrDefaultAsync(s => s.Email == staffLogin.Email);

            if (staffDetails == null) return false;

            var authUser = await _context.authentication_tables
                .FirstOrDefaultAsync(a => a.email == staffLogin.Email && a.Role_Id == 2); // Role 2 for staff

            if (authUser == null) return false;

            var result = _hasher.VerifyHashedPassword(authUser, authUser.Password, staffLogin.Password);
            return result == PasswordVerificationResult.Success;
        }

        public async Task<StaffLoginResponseDTO> GetStaffDetailsForLogin(string email)
        {
            var staffDetails = await _context.staff_details
                .FirstOrDefaultAsync(s => s.Email == email);

            var authUser = await _context.authentication_tables
                .FirstOrDefaultAsync(a => a.email == email && a.Role_Id == 2); // Role 2 for staff

            if (staffDetails == null || authUser == null)
                throw new Exception("Staff not found.");

            return new StaffLoginResponseDTO
            {
                StaffId = staffDetails.Staff_Id,
                Email = email,
                RoleId = authUser.Role_Id,
                LocationId = staffDetails.LocationId,
                StaffFirstName = staffDetails.StaffFirstName,
                StaffLastName = staffDetails.StaffLastName,
                FullName = $"{staffDetails.StaffFirstName} {staffDetails.StaffLastName}",
                Token = "" // Token will be set in the controller
            };
        }

        public async Task RegisterStaff(StaffRegistrationRequestDTO request)
        {
            var existingUser = await _context.authentication_tables
                .FirstOrDefaultAsync(u => u.email == request.Email);

            if (existingUser != null)
                throw new Exception("Staff already exists with this email.");

            // Create authentication record
            var auth = new AuthenticationTable
            {
                email = request.Email,
                Role_Id = 2 // Role 2 for staff
            };
            // Hash password
            auth.Password = _hasher.HashPassword(auth, request.Password);

            _context.authentication_tables.Add(auth);

            // Create staff details
            var staff = new StaffDetails
            {
                StaffFirstName = request.StaffFirstName,
                StaffLastName = request.StaffLastName,
                Email = request.Email,
                MobileNumber = request.MobileNumber,
                Address = request.Address,
                City = request.City,
                State = request.State,
                ZipCode = request.ZipCode,
                DateOfBirth = request.DateOfBirth,
                Department = request.Department,
                Position = request.Position,
                Salary = request.Salary,
                LocationId = request.LocationId,
                IsActive = true
            };

            _context.staff_details.Add(staff);
            await _context.SaveChangesAsync();
        }
    }
}
