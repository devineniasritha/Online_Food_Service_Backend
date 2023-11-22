using OnlineFoodService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System.Data;

namespace OnlineFoodService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly onlinefoodservicecontext _context;
        

        public AuthenticationController(IConfiguration config, onlinefoodservicecontext context)
        {
            _configuration = config;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _context.UserDetails
            .Where(c => c.UserName == model.UserName)
            .FirstOrDefaultAsync();

            //var register = BCrypt.Net.BCrypt.HashPassword(model.Password);
            if (user != null && BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            {
                var token = GenerateJwtToken(user);
                return Ok(new { Token = token, Role = user.UserType});
            }

            return BadRequest("Invalid credentials");
        }

        private string GenerateJwtToken(UserDetail user)
        {
            var claims = new[]
            {
               new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
               new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
               new Claim("UserId", user.UserId.ToString()),
               new Claim("UserName", user.UserName),
               new Claim("Email", user.EmailId)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(10),
                signingCredentials: signIn);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
