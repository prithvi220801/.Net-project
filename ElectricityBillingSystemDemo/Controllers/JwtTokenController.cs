using ElectricityBillingSystemDemo.Data;
using ElectricityBillingSystemDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;
using System.Text;
using ElectricityBillingSystemDemo.Repositories;
using ElectricityBillingSystemDemo.Interfaces;

namespace ElectricityBillingSystemDemo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class JwtTokenController : ControllerBase
    {
        public IConfiguration _configuration;
        public readonly ElectricityDbContext _context;
        
        public JwtTokenController(IConfiguration configuration, ElectricityDbContext context)
        {
            _configuration = configuration;
            _context = context;
           
        }

        [HttpPost("signup")]
        public IActionResult SignUp(UserInputModel userInput)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_context.Users.Any(d => d.Email == userInput.Email))
            {
                return Conflict("Email already exists.");
            }

            User user = new User
            {
                UserName= userInput.UserName,
                Email = userInput.Email,
                Password = userInput.Password,
                Role = userInput.Role

            };
            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("Signup successful.");
        }

        [HttpPost("login")]
        public IActionResult Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = _context.Users.FirstOrDefault(d => d.UserName == loginModel.UserName && d.Password == loginModel.Password);
            if (users != null)
            {
                var token = GenerateJwtToken(users);
                var role = users.Role;
                var userId=users.UserId;
                return Ok(new { message = $"Welcome, {users.UserName}!", token,role,userId });
            }
            else
            {
                return Unauthorized("Invalid email or password.");
            }

        }


        [HttpPost]
        
        private string GenerateJwtToken(User users)
        {
            
                var jwt = _configuration.GetSection("Jwt").Get<Jwt>();
                
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub,jwt.Subject),
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                        new Claim(ClaimTypes.Role, users.Role),
                        new Claim("UserName",users.UserName.ToString()),
                    };

                    var key=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
                    var signIn=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        jwt.Issuer,
                        jwt.Audience,
                        claims,
                        expires: DateTime.Now.AddMinutes(20),
                        signingCredentials: signIn
                        );
                return new JwtSecurityTokenHandler().WriteToken(token);
                    
                
        }

       

    

[HttpPut("ChangePassword/{id}")]
        public async Task<ActionResult> ChangePassword(int id,string newPassword)
        {
            var user = await _context.Users.FindAsync(id);
            if(user!=null)
            {
                user.Password = newPassword;
                await _context.SaveChangesAsync();
                return Ok(new { message = "Password changed Successfully" });
            }
            else
            {
                return NotFound(new { message = "User not found!" });
            }
        }

        

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCustomerById(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserProfile(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new { message = "User profile updated successfully." });
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }

        public class LoginModel
        {
            //[Display(Name="Email")]
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        public class UserInputModel
        {
            public string UserName { get; set; }
            public string Password { get; set; }
            public string Email { get; set; }
            public string Role { get; set; }
        }


    }
}
