using FinalProject.Models.User;

namespace FinalProject.Services
{
    public interface IUserService
    {
        void Create(CreateUserDto user);

        void Login(LoginUserDto user);

        void Logout();
    }


}
