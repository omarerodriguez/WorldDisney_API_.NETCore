namespace MundoDeDisney.MundoDeDisney.Core.DTOs
{
    public class UserDTO
    {
        public string Username { get; set; } = string.Empty;

        public string Password { get; set; }=string.Empty;
        public string Email { get; set; }

    }
    public class UserLoginDTO
    {
        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
