using FinalProject.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace FinalProject.Services
{
    public class LoggedUserService
    {
        private User user;
        public User User 
        { get => this.user;

            set
            {
                user = value;
                IsLogged= user!= null;
            } 
        }
        public bool IsLogged { get; private set; }
    }
}
