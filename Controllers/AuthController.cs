using Microsoft.AspNetCore.Mvc;
using MundoDeDisney.MundoDeDisney.Core.DTOs;
using MundoDeDisney.MundoDeDisney.Core.Entities;
using MundoDeDisney.MundoDeDisney.Core.Interfaces;

namespace MundoDeDisney.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        protected static User user = new User();
        public AuthController(IAuthService authService, IEmailService emailService, IConfiguration configuration)
        {
            _authService = authService;
            _emailService = emailService;
            _configuration = configuration;
        }
        [HttpPost("Register")]
        public async Task<ActionResult>Register(string username,string password,string email)
        {
            var registerResponse = await _authService.RegisterUser(username,password,email);
            if(!registerResponse.Success) { return BadRequest(registerResponse.Message); }
            
          
            await _emailService.SendEmail(email,username);
            return Ok(registerResponse.Message);
        }
        [HttpPost("Login")]
        public async Task<ActionResult<string>>Login(UserLoginDTO request)
        {
            var user = await _authService.GetUserByPassword(request.Username, request.Password);
           // if (user is null) { return NotFound("The credentials is incorrect"); }
            if (user != null)
            {
                //Create token
                var token = _authService.CreateToken(user);
                return Ok(new { token });
            }
            return NotFound("The credentials is incorrect");
        }
    }
}
