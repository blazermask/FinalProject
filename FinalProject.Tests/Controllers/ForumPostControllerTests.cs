using FinalProject.Controllers;
using FinalProject.Models.Forumpost;
using FinalProject.Services.ForumPost;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace FinalProject.Tests.Controllers
{
    public class ForumPostControllerTests
    {
        private readonly Mock<IForumPostService> _mockForumPostService;
        private readonly ForumPostController _controller;

        public ForumPostControllerTests()
        {
            _mockForumPostService = new Mock<IForumPostService>();
            _controller = new ForumPostController(_mockForumPostService.Object);
        }

        [Fact]
        public void GetAll_ShouldReturnViewWithPosts()
        {
            // Arrange
            var posts = new List<GetAllForumPostsViewModel>
            {
                new GetAllForumPostsViewModel
                {
                    Id = 1,
                    Title = "Test Post",
                    AuthorFirstName = "Test",
                    AuthorLastName = "User"
                }
            };
            _mockForumPostService.Setup(s => s.GetAll()).Returns(posts);

            // Act
            var result = _controller.GetAll() as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = Assert.IsType<List<GetAllForumPostsViewModel>>(result.Model);
            Assert.Single(model);
        }

        [Fact]
        public void Create_Get_ShouldReturnView()
        {
            // Act
            var result = _controller.Create() as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Create_Post_ShouldRedirectToGetAll()
        {
            // Arrange
            var createDto = new CreateForumPostDto
            {
                Title = "Test Post",
                Content = "Test Content"
            };

            // Act
            var result = _controller.Create(createDto) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("GetAll", result.ActionName);
            _mockForumPostService.Verify(s => s.Create(createDto), Times.Once);
        }

        [Fact]
        public void Details_ShouldReturnViewWithPost()
        {
            // Arrange
            var post = new GetForumPostsViewModel
            {
                Id = 1,
                Title = "Test Post",
                Content = "Test Content"
            };
            _mockForumPostService.Setup(s => s.Get(1)).Returns(post);

            // Act
            var result = _controller.Details(1) as ViewResult;

            // Assert
            Assert.NotNull(result);
            var model = Assert.IsType<GetForumPostsViewModel>(result.Model);
            Assert.Equal("Test Post", model.Title);
        }
    }
}
