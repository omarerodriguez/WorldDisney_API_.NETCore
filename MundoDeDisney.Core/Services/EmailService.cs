using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MundoDeDisney.MundoDeDisney.Core.Interfaces;
using MundoDeDisney.MundoDeDisney.Core.DTOs;
using MailKit.Net.Smtp;

namespace MundoDeDisney.MundoDeDisney.Core.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendEmail(string email,string username)
        {
            var mail = new MimeMessage();
            mail.From.Add(MailboxAddress.Parse(_configuration.GetSection("EmailUserName").Value));
            mail.To.Add(MailboxAddress.Parse(email));
            mail.Subject = "DISNEY WORLD";
            mail.Body = new TextPart(TextFormat.Html) { Text = "Hey!! Welcome To Disney World, your account has been registered" };

            using var smtp = new SmtpClient();
            smtp.Connect(_configuration.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(_configuration.GetSection("EmailUserName").Value, _configuration.GetSection("EmailPassword").Value);
            smtp.Send(mail);
            smtp.Disconnect(true);
        }
    }
}
