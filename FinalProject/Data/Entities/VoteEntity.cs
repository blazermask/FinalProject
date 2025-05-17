
using FinalProject.Data.Entities;

public class VoteEntity
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public int ForumPostId { get; set; }
    public ForumPostEntity ForumPost { get; set; }
    public bool IsLike { get; set; }  // true for like, false for dislike
}
