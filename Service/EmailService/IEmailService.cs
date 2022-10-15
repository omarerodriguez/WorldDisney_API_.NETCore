using MundoDeDisney.Core.Dto;

namespace MundoDeDisney.Service.EmailService
{
    public interface IEmailService
    {
        void SendEmail(UserDto request);   
    }
}
