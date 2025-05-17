using FinalProject.Data;
using FinalProject.Data.Entities;
using FinalProject.Models.User;
using FinalProject.Services;
using FinalProject.Tests.TestHelpers;
using Xunit;

namespace FinalProject.Tests.Services
{
    public class UserServiceTests : IDisposable
    {
        private readonly TestApplicationDbContext _context;
        private readonly LoggedUserService _loggedUserService;
        private readonly UserService _service;

        public UserServiceTests()
        {
            _context = TestDbContextFactory.Create();
            _loggedUserService = new LoggedUserService();
            _service = new UserService(_context, _loggedUserService);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public void Create_ShouldCreateNewUser()
        {
            // Arrange
            var createDto = new CreateUserDto
            {
                Username = "testuser",
                Password = "testpass",
                Email = "test@test.com",
                FirstName = "Test",
                LastName = "User"
            };

            // Act
            _service.Create(createDto);

            // Assert
            var user = Assert.Single(_context.Users);
            Assert.Equal("testuser", user.Username);
            Assert.Equal("test@test.com", user.Email);
            Assert.Equal("Test", user.FirstName);
            Assert.Equal("User", user.LastName);
            Assert.NotEqual("testpass", user.Password); // Password should be hashed
        }

        [Fact]
        public void Login_WithValidCredentials_ShouldSetLoggedUser()
        {
            // Arrange
            var createDto = new CreateUserDto
            {
                Username = "testuser",
                Password = "testpass",
                Email = "test@test.com",
                FirstName = "Test",
                LastName = "User"
            };
            _service.Create(createDto);

            var loginDto = new LoginUserDto
            {
                Username = "testuser",
                Password = "testpass"
            };

            // Act
            _service.Login(loginDto);

            // Assert
            Assert.NotNull(_loggedUserService.User);
            Assert.Equal("testuser", _loggedUserService.User.Username);
            Assert.True(_loggedUserService.IsLogged);
        }

        [Fact]
        public void Login_WithInvalidCredentials_ShouldThrowException()
        {
            // Arrange
            var loginDto = new LoginUserDto
            {
                Username = "nonexistent",
                Password = "wrongpass"
            };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _service.Login(loginDto));
        }

        [Fact]
        public void Logout_ShouldClearLoggedUser()
        {
            // Arrange
            _loggedUserService.User = new User { Username = "testuser" };

            // Act
            _service.Logout();

            // Assert
            Assert.Null(_loggedUserService.User);
            Assert.False(_loggedUserService.IsLogged);
        }
    }
}
