using FinalProject.Models.Comment;
using FinalProject.Models.Forumpost;

namespace FinalProject.Services.ForumPost
{
    public interface IForumPostService
    {
        List<GetAllForumPostsViewModel> GetAll();

        GetForumPostsViewModel Get(int id);
        void Create(CreateForumPostDto createForumPostDto);
        void Update(int id,UpdateForumPostDto updateForumPostDto); 

        void Delete(int id);
		void AddComment(CreateCommentDto commentDto);
		List<GetCommentsViewModel> GetCommentsForPost(int forumPostId);
        void Vote(CreateVoteDto voteDto);
    }
}
