using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MundoDeDisney.MundoDeDisney.Core.Entities;
using MundoDeDisney.MundoDeDisney.Core.Interfaces;
using MundoDeDisney.MundoDeDisney.Infrastructure.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace MundoDeDisney.MundoDeDisney.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u=>u.Id == id);
            return user;
        }
        public async Task<User> GetUserByPassword(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null||!BCrypt.Net.BCrypt.Verify(password,user.Password  )) { return null; }
            return user;
        }
        public string CreateToken(User user)
        {
            //header
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            //Claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,user.Username)
               // new Claim(ClaimTypes.Role,security.Role.ToString())
            };

            //Payload
            var payload = new JwtPayload
            (
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                DateTime.Now,
                DateTime.UtcNow.AddDays(1)
            );

            var token = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        public async Task<(bool Success, string Message)> RegisterUser(string username, string password, string email)
        {
           var isValidUser = await IsUserValid(username);
            if(!isValidUser.Success) { return (isValidUser.Success, isValidUser.Message);}

            var isValidEmail = await IsEmailValid(email);
            if(!isValidEmail.Success) { return (isValidEmail.Success, isValidEmail.Message);}

            password = BCrypt.Net.BCrypt.HashPassword(password);
            var userEntity = new User
            {
                Username = username,
                Password = password,
                Email = email
            };

             _context.Users.Add(userEntity);
            var response = await _context.SaveChangesAsync()>0;
            return (response, "Successful resgistration");
        }
        public async Task<(bool Success,string Message)>IsUserValid(string userName)
        {
            if (string.IsNullOrEmpty(userName)) { return (false, "The username cannot be empty"); }
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username.ToLower() == userName.ToLower());
            if(user is not null) { return (false, "There is already a registered user with that name");}
            Regex regex = new("^[A-Za-z0-9ÑñÁáÉéÍíÓóÚúÜü\\s]{6,16}$");
            if(!regex.IsMatch(userName)) { return (false, "Username can contain only letters and numbers between 5-50 characters");}
            return (true, "Successful registration!");
        }
        public async Task<(bool Success, string Message)> IsEmailValid(string email)
        {
            //validaciones de mail
            Regex regex = new("^[_a-z0-9A-Z]+(\\.[_a-z0-9A-Z]+)*@[a-zA-Z0-9-]+(\\.[a-z0-9-]+)*(\\.[a-zA-Z]{2,15})$");
            if (!regex.IsMatch(email)) return (false, "It is not a valid Email.");

            if (string.IsNullOrEmpty(email))
                return (false, "The email cannot be empty.");

            var mail = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());

            if (mail is not null)
                return (false, "There is already a registered user with that email.");

            return (true, "Successful registration");

        }
    }
}
