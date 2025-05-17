using System.ComponentModel.DataAnnotations;

namespace FinalProject.Data.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<ForumPostEntity> ForumPosts { get; set; }
       
    }
}
