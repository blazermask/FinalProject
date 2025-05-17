using FinalProject.Data.Entities;
using Microsoft.AspNetCore.Identity;
using BCrypt.Net;

namespace FinalProject.Services
{
    public class PasswordHash : IPasswordHasher<User>
    {
        public string HashPassword(User user, string password)
        {
            // Хеширане на паролата с BCrypt
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public PasswordVerificationResult VerifyHashedPassword(User user, string hashedPassword, string providedPassword)
        {
            // Проверка дали въведената парола съвпада със запазения хеш
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);

            if (isPasswordValid)
                return PasswordVerificationResult.Success;
            else
                return PasswordVerificationResult.Failed;
        }
    }
}
