using Application.Interface;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            try
            {
                return await _userRepository.GetUserByIdAsync(id);
            }
            catch (Exception ex)
            {
                // Log exception
                throw; // Re-throw or handle as needed
            }
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            try
            {
                return await _userRepository.GetUserByUsernameAsync(username);
            }
            catch (Exception ex)
            {
                // Log exception
                throw; // Re-throw or handle as needed
            }
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            try
            {
                return await _userRepository.GetAllUsersAsync();
            }
            catch (Exception ex)
            {
                // Log exception
                throw; // Re-throw or handle as needed
            }
        }

        public async Task RegisterUserAsync(User user, string password)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrEmpty(password)) throw new ArgumentException("Password cannot be null or empty", nameof(password));

            user.Senha = HashPassword(password); // Criptografar a senha antes de salvar
            try
            {
                await _userRepository.AddUserAsync(user);
            }
            catch (Exception ex)
            {
                // Log exception
                throw; // Re-throw or handle as needed
            }
        }

        public async Task UpdateUserAsync(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            try
            {
                await _userRepository.UpdateUserAsync(user);
            }
            catch (Exception ex)
            {
                // Log exception
                throw; // Re-throw or handle as needed
            }
        }

        public async Task DeleteUserAsync(int id)
        {
            try
            {
                await _userRepository.DeleteUserAsync(id);
            }
            catch (Exception ex)
            {
                // Log exception
                throw; // Re-throw or handle as needed
            }
        }

        private string HashPassword(string password)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, 16, 10000, HashAlgorithmName.SHA256))
            {
                var hash = pbkdf2.GetBytes(20);
                var salt = pbkdf2.Salt;
                var hashBytes = new byte[36];
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 20);
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
