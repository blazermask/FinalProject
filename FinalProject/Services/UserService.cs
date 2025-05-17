using FinalProject.Models.User;
using FinalProject.Data.Entities;
using Microsoft.AspNetCore.Identity;
using FinalProject.Data;

namespace FinalProject.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly LoggedUserService loggedUserService;
        private readonly PasswordHasher<User> passwordHasher;

        public UserService(ApplicationDbContext dbContext, LoggedUserService loggedUserService)
        {
            this.dbContext = dbContext;
            this.loggedUserService = loggedUserService;
            this.passwordHasher = new PasswordHasher<User>();
        }

        public void Create(CreateUserDto user)
        {
            User newUser = ToUser(user);
            newUser.Password = passwordHasher.HashPassword(newUser, user.Password);
            dbContext.Users.Add(newUser);
            dbContext.SaveChanges();
        }

        private User ToUser(CreateUserDto user)
        {
            return new User
            {
                Username = user.Username,
                Email = user.Email,
                Password = user.Password, // ще го презапишем в Create()
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }

        public void Login(LoginUserDto User)
        {
            User dbUser = dbContext.Users
                .FirstOrDefault(u => u.Username == User.Username);

            if (dbUser == null)
            {
                throw new ArgumentException("Invalid username or password!");
            }

            var result = passwordHasher.VerifyHashedPassword(dbUser, dbUser.Password, User.Password);
            if (result != PasswordVerificationResult.Success)
            {
                throw new ArgumentException("Invalid username or password!");
            }

            this.loggedUserService.User = dbUser;
        }

        public void Logout()
        {
            this.loggedUserService.User = null;
        }
    }
}