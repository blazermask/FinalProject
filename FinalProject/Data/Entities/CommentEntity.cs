namespace FinalProject.Data.Entities
{
	public class CommentEntity
	{
		public int Id { get; set; }

		public string Content { get; set; }

		public DateTime CreatedOn { get; set; }

		public int ForumPostId { get; set; }
		public ForumPostEntity ForumPost { get; set; }

		public int UserId { get; set; }
		public User User { get; set; }
	}
}
