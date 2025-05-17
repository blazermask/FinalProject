namespace FinalProject.Models.Comment
{
	public class CreateCommentDto
	{
		public int ForumPostId { get; set; }
		public string Content { get; set; }
	}
}