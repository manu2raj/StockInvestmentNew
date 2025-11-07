using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StockInvestmentNew.Models;
using StockInvestmentNew.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StockInvestmentNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenService _jwtTokenService;

        private readonly StockInvestmentContext _context;

        public AuthController(JwtTokenService jwtTokenService, StockInvestmentContext context)
        {
            _jwtTokenService = jwtTokenService;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {

            // This is where you validate the user credentials from your DB.
            // For demonstration, let's assume any user with a non-empty username is valid.
            if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }
            var user = await _context.Users
                                 .FirstOrDefaultAsync(u => u.Username == request.Username && u.PasswordHash == request.Password);

            if (user == null)
            {
                return Unauthorized(new { message = "User Not Found" });  // User not found
            }

            // Generate JWT token
            var token = _jwtTokenService.GenerateToken(request.Username);

            // save user token in database
            var result = await _context.UserTokens.AddAsync(new UserToken
            {
                UserId = user.UserId,
                Token = token,
                Expiry = DateTime.UtcNow.AddHours(1), // assuming 1 hour expiry
                IsRevoked = false
            });

            await _context.SaveChangesAsync();
            return Ok(new { Token = token });
        }

        [HttpPost("register")]
        public IActionResult Register(Register register)
        {
            var exists = _context.StudentsAlls.Any(u => u.Username == register.UserName);
            if (exists) return BadRequest("User already exists");

            var user = new StudentsAll
            {
                Username = register.UserName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(register.Password)
            };

            _context.StudentsAlls.Add(user);
            _context.SaveChanges();
            return Ok("User registered");
        }

       

    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
