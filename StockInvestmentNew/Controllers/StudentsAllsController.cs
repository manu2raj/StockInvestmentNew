using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using StockInvestmentNew.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StockInvestmentNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsAllsController : ControllerBase
    {
        private readonly StockInvestmentContext _context;
        private readonly IConfiguration _config;

        public StudentsAllsController(StockInvestmentContext context, IConfiguration config)
        {
            _context = context;
            _config = config;

        }


        [HttpPost("login")]
        public IActionResult Login(Login login)
        {
            var user = _context.StudentsAlls.FirstOrDefault(u => u.Username == login.UserName);
            if (user == null || !BCrypt.Net.BCrypt.Verify(login.Password, user.PasswordHash))
                return Unauthorized();

            var token = GenerateJwtToken(user);
            return Ok(new { token });
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

        private string GenerateJwtToken(StudentsAll user)
        {
            var claims = new[] {
            new Claim(ClaimTypes.Name, user.Username)
        };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JwtSettings:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], claims,
                expires: DateTime.Now.AddHours(1), signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        //// GET: api/StudentsAlls
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<StudentsAll>>> GetStudentsAlls()
        //{
        //    return await _context.StudentsAlls.ToListAsync();
        //}

        //// GET: api/StudentsAlls/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<StudentsAll>> GetStudentsAll(int id)
        //{
        //    var studentsAll = await _context.StudentsAlls.FindAsync(id);

        //    if (studentsAll == null)
        //    {
        //        return NotFound();
        //    }

        //    return studentsAll;
        //}

        //// PUT: api/StudentsAlls/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutStudentsAll(int id, StudentsAll studentsAll)
        //{
        //    if (id != studentsAll.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(studentsAll).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!StudentsAllExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/StudentsAlls
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<StudentsAll>> PostStudentsAll(StudentsAll studentsAll)
        //{
        //    _context.StudentsAlls.Add(studentsAll);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetStudentsAll", new { id = studentsAll.Id }, studentsAll);
        //}

        //// DELETE: api/StudentsAlls/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteStudentsAll(int id)
        //{
        //    var studentsAll = await _context.StudentsAlls.FindAsync(id);
        //    if (studentsAll == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.StudentsAlls.Remove(studentsAll);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool StudentsAllExists(int id)
        //{
        //    return _context.StudentsAlls.Any(e => e.Id == id);
        //}
    }

    public class Login
    {
        public string UserName { get; set; }
        public string Password { get; set; }

    }

    public class Register : Login
    { }
}
