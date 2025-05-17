using FinalProject.Data.Entities;
using FinalProject.Models.Comment;
using FinalProject.Models.Forumpost;
using FinalProject.Services.ForumPost;
using Microsoft.AspNetCore.Authorization;  // добави това
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers
{
    public class ForumPostController : Controller
    {
        private readonly IForumPostService forumPostService;
        public ForumPostController(IForumPostService forumPostService)
        {
            this.forumPostService = forumPostService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<GetAllForumPostsViewModel> allForumPosts = forumPostService.GetAll();
            return View(allForumPosts);
        }

        
        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public IActionResult Create(CreateForumPostDto forumPost)
        {
            if (!ModelState.IsValid)
            {
                return View("Create",forumPost);
            }

            forumPostService.Create(forumPost);
            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            GetForumPostsViewModel model = forumPostService.Get(id);
            return View(model);
        }

        [HttpPost]  // за редакция трябва POST, а не GET
        [Authorize]  // може да се ограничи само за авторите на поста (по-сложна логика)
        public IActionResult Edit(int id, UpdateForumPostDto forumPost)
        {
            if (!ModelState.IsValid)
            {
                var model = forumPostService.Get(id);
                return View(model);
            }

            forumPostService.Update(id, forumPost);
            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            GetForumPostsViewModel model = forumPostService.Get(id);
            return View(model);
        }

        [HttpPost]
        
        public IActionResult DeleteForumPost(int id)
        {
            forumPostService.Delete(id);
            return RedirectToAction("GetAll");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            GetForumPostsViewModel model = forumPostService.Get(id);
            return View(model);
        }

        [HttpPost]
         
        public IActionResult AddComment(CreateCommentDto commentDto)
        {
            if (!ModelState.IsValid)
            {
                // Ако искаш можеш да върнеш грешка или обратно към детайли на поста
                return RedirectToAction("Details", new { id = commentDto.ForumPostId });
            }

            forumPostService.AddComment(commentDto);
            return RedirectToAction("Details", new { id = commentDto.ForumPostId });
        }
        [HttpPost]
        public IActionResult Vote(CreateVoteDto voteDto)
        {
            forumPostService.Vote(voteDto);
            return RedirectToAction("Details", new { id = voteDto.ForumPostId });
        }
    }
}