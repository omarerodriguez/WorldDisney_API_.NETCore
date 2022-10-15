using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using MundoDeDisney.Core.Dto;

namespace MundoDeDisney.Service.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration configuration;

        public EmailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public void SendEmail(UserDto request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(configuration.GetSection("EmailUserName").Value));
            email.To.Add(MailboxAddress.Parse(request.Email));
            email.Subject = "DISNEY WORLD";
            email.Body = new TextPart(TextFormat.Html) { Text = "Hey!! Welcome To Disney World, your account has been registered" };

            using var smtp = new SmtpClient();
            smtp.Connect(configuration.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(configuration.GetSection("EmailUserName").Value, configuration.GetSection("EmailPassword").Value);
            smtp.Send(email);
            smtp.Disconnect(true);

        }
    }
}
