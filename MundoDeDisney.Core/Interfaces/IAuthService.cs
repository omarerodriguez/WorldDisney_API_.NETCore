﻿using MundoDeDisney.MundoDeDisney.Core.Entities;

namespace MundoDeDisney.MundoDeDisney.Core.Interfaces
{
    public interface IAuthService
    {
        public Task<User> GetUserByPassword(string user, string password);
        public Task<User> GetUserById(int id);
        public string CreateToken(User user);
        public Task<(bool Success, string Message)> RegisterUser(string username, string password, string email);
    }
}
