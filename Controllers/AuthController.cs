using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MundoDeDisney.Core.Dto;
using MundoDeDisney.Core.Entities;
using MundoDeDisney.Service.EmailService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace MundoDeDisney.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public static User user = new User();
        private readonly IConfiguration configuration;
        private readonly IEmailService emailService;

        public AuthController(IConfiguration configuration, IEmailService emailService)
        {
            this.configuration = configuration;
            this.emailService = emailService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<User>> Register(UserDto request)
        {
            CreatePasswordHash(request.Password, out byte[] passworHash, out byte[] passwordSalt);

            user.UserName = request.UserName;
            user.Role = request.Role;
            user.Email = request.Email;
            user.PasswordHash = passworHash;
            user.PasswordSalt = passwordSalt;

            emailService.SendEmail(request);

            return Ok(user);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            if (user.UserName != request.UserName)
            {
                return BadRequest("User not found");
            }
            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Wrong password");
            }
            string token = CreateToken(user);
            return Ok(token);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {

               // new Claim(ClaimTypes.Name, user.UserName),
                 new Claim(ClaimTypes.Role, user.Role)

            }; 
          


            var key = new SymmetricSecurityKey(System.Text.Encoding.ASCII
                .GetBytes(configuration.GetSection("Jwt:Key").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmca = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmca.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}

