using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models.Forumpost
{
    public class CreateForumPostDto
    {
        [Required(ErrorMessage = "Заглавието е задължително.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Съдържанието е задължително.")]
        public string Content { get; set; }
    }
}
