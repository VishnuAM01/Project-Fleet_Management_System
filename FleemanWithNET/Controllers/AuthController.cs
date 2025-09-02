using fleeman_with_dot_net.DTO;
using fleeman_with_dot_net.Models;
using fleeman_with_dot_net.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace fleeman_with_dot_net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUser _userService;

        public AuthController(IConfiguration config, IUser userService)
        {
            _config = config;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] MemberRegistrationRequest user)
        {
            try
            {
                await _userService.Register(user);
                return Ok("User registered successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticationTable user)
        {
            if (!await _userService.ValidateUser(user))
                return Unauthorized("Invalid credentials.");

            var userDetails = await _userService.GetUserDetailsForLogin(user.email);

            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, userDetails.Email),
                    new Claim(ClaimTypes.Role, userDetails.RoleId.ToString()),
                    new Claim("MemberId", userDetails.MemberId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            userDetails.Token = tokenHandler.WriteToken(token);

            return Ok(userDetails);
        }

        [HttpPost("staff-login")]
        public async Task<IActionResult> StaffLogin([FromBody] StaffLoginRequestDTO staffLogin)
        {
            if (!await _userService.ValidateStaff(staffLogin))
                return Unauthorized("Invalid staff credentials.");

            var staffDetails = await _userService.GetStaffDetailsForLogin(staffLogin.Email);

            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, staffDetails.Email),
                    new Claim(ClaimTypes.Role, staffDetails.RoleId.ToString()),
                    new Claim("StaffId", staffDetails.StaffId.ToString()),
                    new Claim("LocationId", staffDetails.LocationId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            staffDetails.Token = tokenHandler.WriteToken(token);

            return Ok(staffDetails);
        }

        [HttpPost("staff-register")]
        public async Task<IActionResult> RegisterStaff([FromBody] StaffRegistrationRequestDTO staffRegistration)
        {
            try
            {
                await _userService.RegisterStaff(staffRegistration);
                return Ok("Staff registered successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("verify")]
        public async Task<IActionResult> VerifyToken([FromHeader(Name = "Authorization")] string authorization)
        {
            try
            {
                if (string.IsNullOrEmpty(authorization) || !authorization.StartsWith("Bearer "))
                {
                    return BadRequest("Invalid authorization header. Use 'Bearer {token}' format.");
                }

                var token = authorization.Substring("Bearer ".Length).Trim();
                var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);

                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

                if (validatedToken == null)
                {
                    return Unauthorized("Invalid token.");
                }

                // Extract claims
                var email = principal.FindFirst(ClaimTypes.Email)?.Value;
                var roleId = principal.FindFirst(ClaimTypes.Role)?.Value;
                var memberId = principal.FindFirst("MemberId")?.Value;
                var staffId = principal.FindFirst("StaffId")?.Value;
                var locationId = principal.FindFirst("LocationId")?.Value;

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(roleId))
                {
                    return BadRequest("Token missing required claims.");
                }

                // Check if it's a staff or member token
                if (!string.IsNullOrEmpty(staffId))
                {
                    // Staff token
                    var staffDetails = await _userService.GetStaffDetailsForLogin(email);
                    return Ok(new
                    {
                        isValid = true,
                        staffId = staffDetails.StaffId,
                        email = staffDetails.Email,
                        roleId = staffDetails.RoleId,
                        locationId = staffDetails.LocationId,
                        fullName = staffDetails.FullName,
                        userType = "Staff",
                        expiresAt = validatedToken.ValidTo
                    });
                }
                else if (!string.IsNullOrEmpty(memberId))
                {
                    // Member token
                    var userDetails = await _userService.GetUserDetailsForLogin(email);
                    return Ok(new
                    {
                        isValid = true,
                        memberId = userDetails.MemberId,
                        email = userDetails.Email,
                        roleId = userDetails.RoleId,
                        userType = "Member",
                        expiresAt = validatedToken.ValidTo
                    });
                }
                else
                {
                    return BadRequest("Token missing user identification claims.");
                }
            }
            catch (SecurityTokenExpiredException)
            {
                return Unauthorized("Token has expired.");
            }
            catch (SecurityTokenInvalidSignatureException)
            {
                return Unauthorized("Invalid token signature.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Token validation failed: {ex.Message}");
            }
        }
    }
}
