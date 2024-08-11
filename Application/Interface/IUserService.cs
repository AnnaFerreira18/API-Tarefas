using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Interface
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> AuthenticateUserAsync(string username, string password);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task RegisterUserAsync(User user, string password);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int id);
    }
}
