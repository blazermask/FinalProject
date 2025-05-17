using FinalProject.Data;
using FinalProject.Models.Forumpost;
using FinalProject.Data.Entities;
using FinalProject.Models.Comment;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Services.ForumPost
{
    public class ForumPostService : IForumPostService
    {
        private readonly ApplicationDbContext _context;
        private readonly LoggedUserService loggedUserService;

        public ForumPostService(ApplicationDbContext context, LoggedUserService loggedUserService)
        {
            _context = context;
            this.loggedUserService = loggedUserService;
        }

        public GetForumPostsViewModel Get(int id)
        {
            var post = _context.ForumPosts
                .Include(fp => fp.Comments)
                .ThenInclude(c => c.User)
                .Include(fp => fp.Votes)
                .Where(fp => fp.Id == id)
                .Select(fp => new GetForumPostsViewModel
                {
                    Id = fp.Id,
                    Title = fp.Title,
                    Content = fp.Content,
                    LikesCount = fp.Votes.Count(v => v.IsLike),
                    DislikesCount = fp.Votes.Count(v => !v.IsLike),
                    UserVote = fp.Votes
                        .Where(v => v.UserId == loggedUserService.User.Id)
                        .Select(v => (bool?)v.IsLike)
                        .FirstOrDefault(),
                    Comments = fp.Comments
                        .OrderByDescending(c => c.CreatedOn)
                        .Select(c => new GetCommentsViewModel
                        {
                            Content = c.Content,
                            AuthorName = c.User.FirstName + " " + c.User.LastName,
                            CreatedOn = c.CreatedOn
                        })
                        .ToList()
                })
                .FirstOrDefault();

            if (post == null)
            {
                throw new Exception("Post not found.");
            }

            return post;
        }

        public void Create(CreateForumPostDto createForumPostDto)
        {
            var forumPost = new ForumPostEntity
            {
                Title = createForumPostDto.Title,
                Content = createForumPostDto.Content,
                UserId = loggedUserService.User.Id
            };

            _context.ForumPosts.Add(forumPost);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var forumPost = GetForumPostById(id);
            _context.ForumPosts.Remove(forumPost);
            _context.SaveChanges();
        }

        public List<GetAllForumPostsViewModel> GetAll() => _context.ForumPosts
            .Select(x => new GetAllForumPostsViewModel
            {
                Id = x.Id,
                Title = x.Title,
                AuthorFirstName = x.User.FirstName,
                AuthorLastName = x.User.LastName,
            }).ToList();

        public void Update(int id, UpdateForumPostDto updateForumPostDto)
        {
            var forumPost = GetForumPostById(id);
            forumPost.Title = updateForumPostDto.Title;
            forumPost.Content = updateForumPostDto.Content;

            _context.ForumPosts.Update(forumPost);
            _context.SaveChanges();
        }

        public ForumPostEntity GetForumPostById(int id)
        {
            return _context.ForumPosts.FirstOrDefault(f => f.Id == id)
                ?? throw new Exception("Forum post not found!");
        }

        public void AddComment(CreateCommentDto commentDto)
        {
            var comment = new CommentEntity
            {
                Content = commentDto.Content,
                CreatedOn = DateTime.Now,
                ForumPostId = commentDto.ForumPostId,
                UserId = loggedUserService.User.Id
            };

            _context.Comments.Add(comment);
            _context.SaveChanges();
        }

        public List<GetCommentsViewModel> GetCommentsForPost(int forumPostId)
        {
            return _context.Comments
                .Where(c => c.ForumPostId == forumPostId)
                .OrderByDescending(c => c.CreatedOn)
                .Select(c => new GetCommentsViewModel
                {
                    Content = c.Content,
                    AuthorName = c.User.FirstName + " " + c.User.LastName,
                    CreatedOn = c.CreatedOn
                })
                .ToList();
        }

        public void Vote(CreateVoteDto voteDto)
        {
            var existingVote = _context.Votes
                .FirstOrDefault(v => v.ForumPostId == voteDto.ForumPostId &&
                                   v.UserId == loggedUserService.User.Id);

            if (existingVote != null)
            {
                if (existingVote.IsLike == voteDto.IsLike)
                {
                    _context.Votes.Remove(existingVote);
                }
                else
                {
                    existingVote.IsLike = voteDto.IsLike;
                }
            }
            else
            {
                _context.Votes.Add(new VoteEntity
                {
                    ForumPostId = voteDto.ForumPostId,
                    UserId = loggedUserService.User.Id,
                    IsLike = voteDto.IsLike
                });
            }

            _context.SaveChanges();
        }
    }
}
