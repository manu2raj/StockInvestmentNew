using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockInvestmentNew.Models;
using StockInvestmentNew.Services;

namespace StockInvestmentNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationLogin : ControllerBase
    {
        private readonly StockInvestmentContext _context;
        public RegistrationLogin(StockInvestmentContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequest request)
        {
            // Check if user already exists
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == request.Username || u.Email == request.Email);

            if (existingUser != null)
            {
                return Conflict(new { message = "Username or Email already exists" });
            }

            // Create new user
            var newUser = new User
            {
                Username = request.Username,
                PasswordHash = request.PasswordHash, // In real applications, hash the password properly
                Email = request.Email,
                Role = request.IsAdmin ? "Admin" : "Investor" 
            };

            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            return Ok(new { message = "User registered successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _context.Users
                                 .FirstOrDefaultAsync(u => u.Username == request.Username && u.PasswordHash == request.Password);

            if (user == null)
            {
                return Unauthorized(new { message = "User Not Found" });  // User not found
            }

           
            return Ok("Login is successful");
        }
    }

    public class RegistrationRequest
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; } 
    }
}
