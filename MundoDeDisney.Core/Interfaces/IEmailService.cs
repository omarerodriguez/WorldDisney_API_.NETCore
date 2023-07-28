using MundoDeDisney.MundoDeDisney.Core.DTOs;

namespace MundoDeDisney.MundoDeDisney.Core.Interfaces
{
    public interface IEmailService
    {
        Task SendEmail(string email,string username);
    }
}
