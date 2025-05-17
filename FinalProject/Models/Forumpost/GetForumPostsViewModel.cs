using FinalProject.Models.Comment;

namespace FinalProject.Models.Forumpost
{
    public class GetForumPostsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
        public bool? UserVote { get; set; }  // true for like, false for dislike, null for no vote
        public List<GetCommentsViewModel> Comments { get; set; } = new List<GetCommentsViewModel>();
    }

  
}
