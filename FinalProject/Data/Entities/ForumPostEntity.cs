using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Principal;
namespace FinalProject.Data.Entities
{
    public class ForumPostEntity
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
       public User User { get; set; }

		
		public ICollection<CommentEntity> Comments { get; set; } = new List<CommentEntity>();
        public ICollection<VoteEntity> Votes { get; set; } = new List<VoteEntity>();

    }
}
