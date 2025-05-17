using FinalProject.Data;
using FinalProject.Data.Entities;
using FinalProject.Models.Forumpost;
using FinalProject.Models.Comment;
using FinalProject.Services;
using FinalProject.Services.ForumPost;
using FinalProject.Tests.TestHelpers;
using Xunit;

namespace FinalProject.Tests.Services
{
    public class ForumPostServiceTests : IDisposable
    {
        private readonly TestApplicationDbContext _context;
        private readonly LoggedUserService _loggedUserService;
        private readonly ForumPostService _service;

        public ForumPostServiceTests()
        {
            _context = TestDbContextFactory.Create();
            _loggedUserService = new LoggedUserService();
            _service = new ForumPostService(_context, _loggedUserService);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
        /*
        [Fact]
        public void Create_ShouldAddNewForumPost()
        {
            // Arrange
            var user = new User
            {
                Username = "testuser",
                Email = "test@test.com",
                FirstName = "Test",
                LastName = "User"
            };
            _context.Users.Add(user);
            _context.SaveChanges();

            _loggedUserService.User = user;

            var createDto = new CreateForumPostDto
            {
                Title = "Test Post",
                Content = "Test Content"
            };

            // Act
            _service.Create(createDto);

            // Assert
            var post = Assert.Single(_context.ForumPosts);
            Assert.Equal("Test Post", post.Title);
            Assert.Equal("Test Content", post.Content);
            Assert.Equal(user.Id, post.UserId);
        } 

        [Fact]
        public void GetAll_ShouldReturnAllPosts()
        {
            // Arrange
            var user = new User
            {
                Username = "testuser",
                Email = "test@test.com",
                FirstName = "Test",
                LastName = "User"
            };
            _context.Users.Add(user);

            var posts = new List<ForumPostEntity>
            {
                new ForumPostEntity { Title = "Post 1", Content = "Content 1", UserId = user.Id },
                new ForumPostEntity { Title = "Post 2", Content = "Content 2", UserId = user.Id }
            };
            _context.ForumPosts.AddRange(posts);
            _context.SaveChanges();

            // Act
            var result = _service.GetAll();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, p => p.Title == "Post 1");
            Assert.Contains(result, p => p.Title == "Post 2");
        }
        
        [Fact]
        public void Vote_ShouldAddNewVote()
        {
            // Arrange
            var user = new User
            {
                Username = "testuser",
                Email = "test@test.com",
                FirstName = "Test",
                LastName = "User"
            };
            _context.Users.Add(user);

            var post = new ForumPostEntity
            {
                Title = "Test Post",
                Content = "Test Content",
                UserId = user.Id
            };
            _context.ForumPosts.Add(post);
            _context.SaveChanges();

            _loggedUserService.User = user;

            var voteDto = new CreateVoteDto
            {
                ForumPostId = post.Id,
                IsLike = true
            };

            // Act
            _service.Vote(voteDto);

            // Assert
            var vote = Assert.Single(_context.Votes);
            Assert.True(vote.IsLike);
            Assert.Equal(user.Id, vote.UserId);
            Assert.Equal(post.Id, vote.ForumPostId);
        } *

        [Fact]
        public void AddComment_ShouldCreateNewComment()
        {
            // Arrange
            var user = new User
            {
                Username = "testuser",
                Email = "test@test.com",
                FirstName = "Test",
                LastName = "User"
            };
            _context.Users.Add(user);

            var post = new ForumPostEntity
            {
                Title = "Test Post",
                Content = "Test Content",
                UserId = user.Id
            };
            _context.ForumPosts.Add(post);
            _context.SaveChanges();

            _loggedUserService.User = user;

            var commentDto = new CreateCommentDto
            {
                ForumPostId = post.Id,
                Content = "Test Comment"
            };

            // Act
            _service.AddComment(commentDto);

            // Assert
            var comment = Assert.Single(_context.Comments);
            Assert.Equal("Test Comment", comment.Content);
            Assert.Equal(user.Id, comment.UserId);
            Assert.Equal(post.Id, comment.ForumPostId);
        }*/
    }
}
